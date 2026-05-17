using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Linq;
using CarReader.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace CarReader.ViewModels
{
    public partial class CarReaderViewModel : ObservableObject
    {
        public string Version => $"Verze: {Assembly.GetExecutingAssembly().GetName().Version}";

        [ObservableProperty]
        private ObservableCollection<Car> cars = [];

        [ObservableProperty]
        private string infoMessage = string.Empty;

        public CarReaderViewModel() { }

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

            try
            {
                var doc = XDocument.Load(dialog.FileName);

                var data = doc.Root!.Elements("car")
                    .Select(x => new Car
                    {
                        Name = x.Element("name")?.Value ?? "",
                        SellDate = DateTime.Parse(x.Element("sellDate")?.Value ?? DateTime.MinValue.ToString()),
                        Price = double.Parse(x.Element("price")?.Value ?? "0"),
                        Vat = double.Parse(x.Element("vat")?.Value ?? "0"),
                    })
                    .ToArray();

                Cars = new ObservableCollection<Car>(data);

                if (data.Count() > 0)
                {
                    InfoMessage = "Soubor úspěšně načten.";
                }
                else
                {
                    InfoMessage = "Soubor neobsahuje žádná data.";
                }
            }
            catch (Exception ex)
            {
                InfoMessage = $"Chyba při načítání XML: {ex.Message}";
                Cars.Clear();
            }
        }
    }
}
