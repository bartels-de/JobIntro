namespace JobIntro.Core.ServiceRegistration.Interfaces;

/// <summary>
/// Interface for the registration of a scoped service
/// </summary>
public interface IScoped : IJobIntroService
{
    
}

/// <summary>
/// Interface for the registration of a scoped service
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IScoped<T> : IJobIntroService
{
    
}