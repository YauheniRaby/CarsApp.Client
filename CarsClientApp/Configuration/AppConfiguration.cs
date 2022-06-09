using System.Configuration;

namespace CarsClientApp.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration()
        {
            BaseAdress = ConfigurationManager.AppSettings[nameof(BaseAdress)] ?? default;

            CarController = ConfigurationManager.AppSettings[nameof(CarController)] ?? default;

            ImageController = ConfigurationManager.AppSettings[nameof(ImageController)] ?? default;

            PathLog = ConfigurationManager.AppSettings[nameof(PathLog)] ?? default;
        }

        public string BaseAdress { get; set; }

        public string CarController { get; set; }

        public string ImageController { get; set; }

        public string PathLog { get; set; }
    }
}
