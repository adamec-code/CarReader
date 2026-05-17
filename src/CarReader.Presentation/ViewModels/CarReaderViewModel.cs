using System.Collections.ObjectModel;
using System.Reflection;
using CarReader.Application.Common;
using CarReader.Application.Models;
using CarReader.Application.Repositories;
using CarReader.Application.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace CarReader.Presentation.ViewModels
{
    public partial class CarReaderViewModel : ObservableObject
    {
        public string Version => $"Verze: {Assembly.GetExecutingAssembly().GetName().Version}";

        private readonly ICarService service;

        [ObservableProperty]
        private ObservableCollection<CarDto> cars = [];

        [ObservableProperty]
        private string infoMessage = string.Empty;

        public CarReaderViewModel(ICarService service)
        {
            this.service = service;
        }

        [RelayCommand]
        private void UploadData()
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "XML soubory (*.xml)|*.xml|All files (*.*)|*.*";

            if (dialog.ShowDialog() != true)
            {
                InfoMessage = "Nebyl vybrán žádný soubor.";
                return;
            }

            DataSource<CarDto> dataSource = service.LoadCars(dialog.FileName);

            if (dataSource.IsOk)
            {
                Cars = new ObservableCollection<CarDto>(dataSource.Data);
                InfoMessage = "Soubor úspěšně načten.";
            }
            else
            {
                InfoMessage = dataSource.ErrorMessage;
            }
        }
    }
}
