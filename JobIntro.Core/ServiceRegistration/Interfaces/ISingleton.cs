namespace JobIntro.Core.ServiceRegistration.Interfaces;

public interface ISingleton : IJobIntroService
{

}

public interface ISingleton<T> : IJobIntroService
{

}