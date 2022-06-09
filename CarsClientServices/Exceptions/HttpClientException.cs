using System;

namespace CarsClientServices.Exceptions
{
    public class HttpClientException : Exception
    {
        public HttpClientException(string message) : base(message) { }
    }
}
