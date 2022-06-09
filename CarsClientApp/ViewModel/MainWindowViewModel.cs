using CarsClientApp.Infrastructure;
using CarsClientApp.Services.Abstract;
using CarsClientServices.DTOs;
using CarsClientServices.Services.Abstract;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarsClientApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IApiService _apiService;
        private readonly IHandlingExceptionService _handlingExceptionService;
        private readonly DelegateInitOrRefresh _initOrUpdate;
        
        public ObservableCollection<CarDto> Cars { get; private set; }

        private CarDto _selectedCar;
        public CarDto SelectedCar
        {
            get => _selectedCar;
            set 
            { 
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private CarDto _newCar;
        public CarDto NewCar
        {
            get => _newCar;            
            set
            {
                _newCar = value;
                OnPropertyChanged(nameof(NewCar));
            }
        }

        public ICommand RemoveCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SelectImageCommand { get; private set; }

        public MainWindowViewModel(IApiService apiService, IHandlingExceptionService handlingExceptionService)
        {
            _newCar=new CarDto();
            _apiService = apiService;
            _handlingExceptionService = handlingExceptionService;

            RemoveCommand = new DelegateCommand(RemoveCarAsync, CanRemoveCar);
            AddCommand = new DelegateCommand(AddCarAsync, CanAddCar);
            SaveCommand = new DelegateCommand(SaveCarAsync, CanSaveCar);
            SelectImageCommand = new DelegateCommand(SelectImageAsync);

            _initOrUpdate = new DelegateInitOrRefresh(InitOrRefreshItemsSourceAsync);
            _initOrUpdate.Invoke();
        }

        private Task SelectImageAsync(object arg)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg)|*.jpg",
                Multiselect = false
            };
            if (dialog.ShowDialog() != true) 
                return Task.CompletedTask;

            _newCar.FileName = dialog.SafeFileName;
            _newCar.Path = dialog.FileName;
            return Task.CompletedTask;
        }

        private async Task SaveCarAsync(object obj)
        {
            try
            {
                await _apiService.UpdateCarAsync(SelectedCar);
            }
            catch (Exception ex)
            {
                _handlingExceptionService.HandlingException(ex);
            }
            _initOrUpdate?.Invoke();            
        }

        private async Task AddCarAsync(object obj)
        {
            try
            {
                await _apiService.AddCarAsync(NewCar);
                NewCar = new CarDto();
            }
            catch(Exception ex)
            {
                _handlingExceptionService.HandlingException(ex);
            }
            _initOrUpdate?.Invoke();
        }

        private async Task RemoveCarAsync(object obj)
        {
            var idList = ((IList)obj).Cast<CarDto>().Select(x=>x.Id);
            if (idList.Any())
            {
                try
                {
                    await _apiService.RemoveCarAsync(idList);
                }
                catch (Exception ex)
                {
                    _handlingExceptionService.HandlingException(ex);
                }
                _initOrUpdate?.Invoke();                
            }
        }

        private bool CanSaveCar(object arg) => 
            SelectedCar != null && SelectedCar.Company != null && 
            SelectedCar.Company != string.Empty && SelectedCar.Model != string.Empty;
             
        private bool CanAddCar(object arg) =>
            NewCar.Company != null && NewCar.Company != string.Empty && 
            NewCar.Model != null && NewCar.Model != string.Empty && 
            NewCar.FileName != null && NewCar.FileName != string.Empty;                

        private bool CanRemoveCar(object arg) => SelectedCar != null;

        
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task InitOrRefreshItemsSourceAsync()
        {
            try
            {
                var result = await _apiService.GetCarsAsync();
                Cars = new ObservableCollection<CarDto>(result);
                MainWindow.ProductsList.ItemsSource = Cars;
            }
            catch (Exception ex)
            {
                _handlingExceptionService.HandlingException(ex);
            }
        }

    }
}
