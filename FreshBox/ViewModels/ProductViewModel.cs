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
    public class ProductViewModel : ViewModelBase
    {
        private Product _product = new();

        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get => _product.ProductName;
            set
            {
                if (_product.ProductName != value)
                {
                    _product.ProductName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CategoryId
        {
            get => _product.CategoryId;
            set
            {
                if (_product.CategoryId != value)
                {
                    _product.CategoryId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Barcode
        {
            get => _product.Barcode;
            set
            {
                if (_product.Barcode != value)
                {
                    _product.Barcode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Unit
        {
            get => _product.Unit;
            set
            {
                if (_product.Unit != value)
                {
                    _product.Unit = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Price
        {
            get => _product.Price;
            set
            {
                if (_product.Price != value)
                {
                    _product.Price = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ExpireDate
        {
            get => _product.ExpireDate;
            set
            {
                if (_product.ExpireDate != value)
                {
                    _product.ExpireDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Stock
        {
            get => _product.Stock;
            set
            {
                if (_product.Stock != value)
                {
                    _product.Stock = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Origin
        {
            get => _product.Origin;
            set
            {
                if (_product.Origin != value)
                {
                    _product.Origin = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Allergens
        {
            get => _product.Allergens;
            set
            {
                if (_product.Allergens != value)
                {
                    _product.Allergens = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HaccpCertified
        {
            get => _product.HaccpCertified;
            set
            {
                if (_product.HaccpCertified != value)
                {
                    _product.HaccpCertified = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? HaccpCertDate
        {
            get => _product.HaccpCertDate;
            set
            {
                if (_product.HaccpCertDate != value)
                {
                    _product.HaccpCertDate = value;
                    OnPropertyChanged();
                }
            }
        }

        //public string StorageTemp
        //{
        //    get => _product.StorageTemp;
        //    set
        //    {
        //        if (_product.StorageTemp != value)
        //        {
        //            _product.StorageTemp = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public int WarehouseId
        //{
        //    get => _product.WarehouseId;
        //    set
        //    {
        //        if (_product.WarehouseId != value)
        //        {
        //            _product.WarehouseId = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public bool IsDefective
        //{
        //    get => _product.IsDefective;
        //    set
        //    {
        //        if (_product.IsDefective != value)
        //        {
        //            _product.IsDefective = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
    }
}