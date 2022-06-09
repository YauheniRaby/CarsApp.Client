using System.Net.Http;

namespace CarsClientServices.Services.Abstract
{
    public interface IHandlingResponseService
    {
        void CheckResnonse(HttpResponseMessage httpResponseMessage);
    }
}
