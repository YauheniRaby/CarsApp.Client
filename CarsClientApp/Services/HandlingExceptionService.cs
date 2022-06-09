using CarsClientApp.Services.Abstract;
using CarsClientServices.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;

namespace CarsClientApp.Services
{
    public class HandlingExceptionService : IHandlingExceptionService
    {
        private readonly ILogger _logger;
        public HandlingExceptionService(ILogger logger)
        {
            _logger = logger;
        }

        public void HandlingException(Exception exception)
        {
            if (exception.GetType() == typeof(HttpClientException))
                MessageBox.Show(exception.Message, $"Error", MessageBoxButton.OK, MessageBoxImage.Error);

            _logger.LogError(exception, exception.Message);
        }
    }
}
