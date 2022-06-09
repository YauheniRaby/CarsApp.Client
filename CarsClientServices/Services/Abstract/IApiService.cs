using CarsClientServices.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsClientServices.Services.Abstract
{
    public interface IApiService
    {
        Task<IEnumerable<CarDto>> GetCarsAsync();

        Task RemoveCarAsync(IEnumerable<Guid> idList);

        Task AddCarAsync(CarDto car);

        Task UpdateCarAsync(CarDto car);
    }
}
