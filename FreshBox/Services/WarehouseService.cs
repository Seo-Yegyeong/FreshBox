using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Repository;
using FreshBox.Models;

namespace FreshBox.Services
{
    public class WarehouseService
    {
        private WarehouseRepository WarehouseRepo { get; set; } = new WarehouseRepository();
        public WarehouseService() {
            
        }

        public ObservableCollection<Warehouse> LoadWarehousesService()
        {
            return new ObservableCollection<Warehouse>(WarehouseRepo.GetAllWarehouses());
        }

    }
}
