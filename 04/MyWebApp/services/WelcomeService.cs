using MyWebApp.interfaces;

namespace MyWebApp.Services;

public class WelcomeServices : IWelcomeServices
{
    DateTime _serviceCreated;
    Guid _serviceid;

    public WelcomeServices()
    {
        _serviceCreated = DateTime.Now;
        _serviceid = Guid.NewGuid();
    }

    public string GetWelcomeMsg()
    {
        return $"welcome to contoso! the current time is {_serviceCreated}. this service instance is {_serviceid}";
    }

}