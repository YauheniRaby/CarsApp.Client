namespace CarsClientApp.Configuration
{
    public interface IAppConfiguration
    {
        string BaseAdress { get; set; }

        string CarController { get; set; }

        string ImageController { get; set; }
    }
}
