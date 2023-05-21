using System.Reflection;
using JobIntro.Core.ServiceRegistration.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobIntro.Core.ServiceRegistration;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all items for the calling assembly
    /// </summary>
    public static IServiceCollection AddServiceForCallingAssembly(this IServiceCollection services)
    {
        var callingAssembly = Assembly.GetCallingAssembly();

        return AddService(services, callingAssembly);
    }

    private static void RegisterWithTypes(
        IServiceCollection services, 
        Type serviceType, 
        Type implementationType,
        ServiceLifetime lifetime)
    {
        var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);

        services.Add(descriptor);
    }

    private static void RegisterWithImplementationFactory(
        IServiceCollection services,
        Type implementationType,
        ServiceLifetime lifetime)
    {
        var classInstance = Activator.CreateInstance(implementationType);

        var invokedMethod = implementationType
            .GetMethod(nameof(IHasImplementationFactory.GetFactory))
            ?.Invoke(classInstance, null);
        
        if (invokedMethod is not Func<IServiceProvider, object> factory)
        {
            throw new NullReferenceException("The factory for services must not be null");
        }
        
        var descriptor = new ServiceDescriptor(implementationType, factory, lifetime);

        services.Add(descriptor);
    }

    private static (Type serviceType, Type implementationType) GetTypes(Type serviceToRegister)
    {
        var genericInterface = serviceToRegister
            .GetInterfaces()
            .FirstOrDefault(type => type.IsGenericType && typeof(IJobIntroService).IsAssignableFrom(type));

        return (genericInterface is not null
            ? genericInterface.GetGenericArguments()[0]
            : serviceToRegister, serviceToRegister);
    }

    private static ServiceLifetime GetLifetime(Type serviceToRegister)
    {
        var lifetime = ServiceLifetime.Transient;

        if (typeof(IScoped).IsAssignableFrom(serviceToRegister))
            lifetime = ServiceLifetime.Scoped;
        else if (typeof(ISingleton).IsAssignableFrom(serviceToRegister)) 
            lifetime = ServiceLifetime.Singleton;

        return lifetime;
    }

    private static IEnumerable<Assembly> FilterAssemblies(params Assembly[] assemblies)
    {
        var currentAssembly = Assembly.GetAssembly(typeof(IJobIntroService));

        return assemblies.Where(x => x.FullName != currentAssembly?.FullName);
    }
    
    private static IServiceCollection AddService(this IServiceCollection services, params Assembly[] assemblies)
    {
        var compatibleAssemblies = FilterAssemblies(assemblies);

        var servicesToRegister = compatibleAssemblies
            .SelectMany(x => x.GetTypes())
            .Where(t => typeof(IJobIntroService).IsAssignableFrom(t))
            .ToList();

        foreach (var serviceToRegister in servicesToRegister)
        {
            var (serviceType, implementationType) = GetTypes(serviceToRegister);
            var lifetime = GetLifetime(serviceToRegister);

            if (typeof(IHasImplementationFactory).IsAssignableFrom(serviceToRegister))
                RegisterWithImplementationFactory(services, implementationType, lifetime);
            else
                RegisterWithTypes(services, serviceType, implementationType, lifetime);
        }

        return services;
    }
}