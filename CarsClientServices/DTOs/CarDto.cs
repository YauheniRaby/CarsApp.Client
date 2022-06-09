using System;
using System.ComponentModel;

namespace CarsClientServices.DTOs
{
    public class CarDto : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public string Company { get; set; }

        public string Model { get; set; }

        public string Path { get; set; }

        private string fileName;

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
