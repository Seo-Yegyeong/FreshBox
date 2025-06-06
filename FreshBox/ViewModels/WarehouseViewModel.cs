using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Models;
using FreshBox.Services;
using FreshBox.Enums;

namespace FreshBox.ViewModels
{
    public partial class WarehouseViewModel : ObservableObject
    {
        private readonly WarehouseService warehouseService = new WarehouseService();

        public ObservableCollection<Warehouse> Warehouses { get; set; } = new();

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string location = string.Empty;

        [ObservableProperty]
        private StorageTemp tempControl;

        [ObservableProperty]
        private StorageTemp selectedTempControl;

        private IEnumerable<StorageTemp> storageTempTypes => Enum.GetValues(typeof(StorageTemp)).Cast<StorageTemp>();

        public WarehouseViewModel()
        {
            LoadWarehouses();
        }

        [RelayCommand]
        private void LoadWarehouses()
        {
            Warehouses = warehouseService.LoadWarehousesService();
        }
    }
}
