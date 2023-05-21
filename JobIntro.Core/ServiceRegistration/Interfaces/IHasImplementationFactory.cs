namespace JobIntro.Core.ServiceRegistration.Interfaces;

/// <summary>
/// You can use this interface if you have a different registration strategy, for example,
/// if you want to register a concrete instance of an object.
///
/// Note: Provide a parameterless constructor for your class
/// </summary>
public interface IHasImplementationFactory
{
    Func<IServiceProvider, object> GetFactory();
}