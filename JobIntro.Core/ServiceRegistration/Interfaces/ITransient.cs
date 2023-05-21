namespace JobIntro.Core.ServiceRegistration.Interfaces;

/// <summary>
/// Interface for the registration of a transient service
/// </summary>
public interface ITransient : IJobIntroService
{
    
}

/// <summary>
/// Interface for the registration of a transient service
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITransient<T> : IJobIntroService
{
    
}