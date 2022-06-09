using CarsClientServices.Exceptions;
using CarsClientServices.Services.Abstract;
using System;
using System.Net.Http;

namespace CarsClientServices.Services
{
    public class HandlingResponseService : IHandlingResponseService
    {
        public void CheckResnonse(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var statusCode = httpResponseMessage.StatusCode;
                throw new HttpClientException($"{(int)statusCode} {statusCode}");
            }
        }
    }
}
