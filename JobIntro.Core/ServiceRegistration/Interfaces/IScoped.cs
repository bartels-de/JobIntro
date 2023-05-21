namespace JobIntro.Core.ServiceRegistration.Interfaces;

public interface IScoped : IJobIntroService
{
    
}

public interface IScoped<T> : IJobIntroService
{
    
}