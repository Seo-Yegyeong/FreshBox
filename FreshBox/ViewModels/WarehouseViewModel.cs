using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FreshBox.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FreshBox.ViewModels
{
    public class WarehouseViewModel : ViewModelBase
    {
        private Warehouse _warehouse = new();

        public Warehouse Warehouse
        {
            get => _warehouse;
            set
            {
                _warehouse = value;
                OnPropertyChanged();
            }
        }

        public string WarehouseName
        {
            get => _warehouse.WarehouseName;
            set
            {
                if (_warehouse.WarehouseName != value)
                {
                    _warehouse.WarehouseName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Location
        {
            get => _warehouse.Location;
            set
            {
                if (_warehouse.Location != value)
                {
                    _warehouse.Location = value;
                    OnPropertyChanged();
                }
            }
        }

        //public string TempControl
        //{
        //    get => _warehouse.TempControl;
        //    set
        //    {
        //        if (_warehouse.TempControl != value)
        //        {
        //            _warehouse.TempControl = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public int Capacity
        //{
        //    get => _warehouse.Capacity;
        //    set
        //    {
        //        if (_warehouse.Capacity != value)
        //        {
        //            _warehouse.Capacity = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
    }
}
