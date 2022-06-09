using CarsClientApp.Configuration;
using CarsClientServices.DTOs;
using CarsClientServices.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarsClientSevices.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHandlingResponseService _handlingResponseService;
        private readonly IAppConfiguration _appConfiguration;
        
        public ApiService(HttpClient httpClient, IAppConfiguration appConfiguration,  IHandlingResponseService handlingResponseService)
        {
            _httpClient = httpClient;
            _handlingResponseService = handlingResponseService;
            _appConfiguration = appConfiguration;
        }
        
        public Task<IEnumerable<CarDto>> GetCarsAsync()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<CarDto>>(string.Empty);
        }

        public async Task RemoveCarAsync(IEnumerable<Guid> idList)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = _httpClient.BaseAddress,
                Content = new StringContent(JsonSerializer.Serialize(idList), Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);            
            _handlingResponseService.CheckResnonse(response); 
        }

        public async Task AddCarAsync(CarDto car)
        {
            car.FileName = await UploadImage(car.FileName, car.Path);
            var jsonCar = JsonSerializer.Serialize(car);
            var data = new StringContent(jsonCar, Encoding.UTF8, "application/json");
            _handlingResponseService.CheckResnonse(await _httpClient.PostAsync(string.Empty, data));            
        }

        public async Task UpdateCarAsync(CarDto car)
        {
            var jsonCar = JsonSerializer.Serialize(car);
            var data = new StringContent(jsonCar, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(string.Empty, data);
            _handlingResponseService.CheckResnonse(response);
        }

        private async Task<string> UploadImage(string fileName, string path)
        {
            string uri = $"{_appConfiguration.BaseAdress}{_appConfiguration.ImageController}";
            
            var fileByteArray = File.ReadAllBytes(path);

            var content = new MultipartFormDataContent("Upload")
            {
                { new StreamContent(new MemoryStream(fileByteArray)), Path.GetFileNameWithoutExtension(path), fileName }
            };
            var response = await _httpClient.PostAsync(uri, content);

            _handlingResponseService.CheckResnonse(response);

            return await response.Content.ReadAsStringAsync(); 
        }
    }
}
