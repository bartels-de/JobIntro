namespace JobIntro.Core.ServiceRegistration.Interfaces;

/// <summary>
/// Interface for the registration of a singleton service
/// </summary>
public interface ISingleton : IJobIntroService
{

}

/// <summary>
/// Interface for the registration of a singleton service
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISingleton<T> : IJobIntroService
{

}