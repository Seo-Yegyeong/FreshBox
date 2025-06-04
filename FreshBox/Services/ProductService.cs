using FreshBox.Models;
using FreshBox.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*
 service 계층은 비즈니스 로직을 처리하고,
 repository 계층은 데이터베이스와의 상호작용을 담당합니다.

 repository 계층은 데이터베이스와의 직접적인 CRUD 작업을 수행하고,
 service 계층은 repository를 통해 데이터를 가져와 저장하거나 유효성 검사 등의 비즈니스 로직을 처리합니다.
 */

namespace FreshBox.Services
{
    public class ProductService
    {
        ProductRepository ProductRepo = new ProductRepository();

        //public ObservableCollection<Product> Products { get; set; } = new();
        // 유효성 검사 모음


        // Read the list of all products
        public ObservableCollection<Product> LoadProductsService()
        {
            return new ObservableCollection<Product>(ProductRepo.GetAllProducts());
        }

        internal bool IsProductNameDuplicated(string productName)
        {
            try
            {
                int result = ProductRepo.FindByUsername(productName);

                if (result == 0) // 중복 없음
                {
                    MessageBox.Show(productName + "은(는) 사용 가능한 상품명입니다.", "상품명 중복 검사", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (result == 1) // DB에 중복된 username 있음
                {
                    MessageBox.Show(productName + "은(는) 이미 존재하는 상품명입니다.", "상품명 중복 검사", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return true;
                }
                else
                {
                    throw new Exception("ERROR : DB 예외발생"); //호출부로 예외 던짐
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw; // 호출부로 예외 던짐
            }
        }
    }
}
