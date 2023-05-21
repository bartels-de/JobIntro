namespace JobIntro.Core.ServiceRegistration.Interfaces;

public interface ITransient : IJobIntroService
{
    
}

public interface ITransient<T> : IJobIntroService
{
    
}