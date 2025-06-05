using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Models;
using FreshBox.Database;
using MySql.Data.MySqlClient;
using FreshBox.Enums;
using System.Windows;
using System.Diagnostics;
using MySqlX.XDevAPI.Common;

namespace FreshBox.Repository
{
    public class ProductRepository
    {
        private readonly MysqlDatabaseManager _dbManager;

        public ProductRepository()
        {
            _dbManager = MysqlDatabaseManager.GetInstance();
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            string query = "SELECT * FROM product";
            try
            {
                using var conn = _dbManager.GetConnection();
                using var command = new MySqlCommand(query, conn);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32("id"),
                        ProductName = reader.GetString("product_name"),
                        CategoryId = reader.GetInt32("category_id"),
                        Barcode = reader.GetString("barcode"),
                        Stock = reader.GetInt32("stock"),
                        StorageTemp = (StorageTemp)reader.GetInt32("storage_temp"),
                        WarehouseId = reader.GetInt32("warehouse_id")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: ProductRepository.cs -> GetAllProducts()\r\n- message: {ex.Message}");
            }
            return products;
        }

        public int InsertProduct(Product product)
        {
            MySqlConnection conn = new();
            string query = "INSERT INTO product (product_name, category_id, barcode, stock, storage_temp, warehouse_id) " +
                           "VALUES (@ProductName, @CategoryId, @Barcode, 0, @StorageTemp, @WarehouseId)";
            int isSuceeded = 0;
            try
            {
                conn = _dbManager.GetConnection();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@StorageTemp", (int)product.StorageTemp);
                command.Parameters.AddWithValue("@WarehouseId", product.WarehouseId);
                isSuceeded = command.ExecuteNonQuery(); // returns 1 if successful, 0 if not
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: ProductRepository.cs -> InsertProduct()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
            return isSuceeded;
        }

        public int UpdateProduct(Product product)
        {
            MySqlConnection conn = new();
            string query = "UPDATE product SET product_name = @ProductName, category_id = @CategoryId, " +
                           "barcode = @Barcode, stock = @Stock, storage_temp = @StorageTemp, warehouse_id = @WarehouseId " +
                           "WHERE id = @Id";
            int isSuceeded = 0;
            try
            {
                conn = _dbManager.GetConnection();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@StorageTemp", (int)product.StorageTemp);
                command.Parameters.AddWithValue("@WarehouseId", product.WarehouseId);
                isSuceeded = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: ProductRepository.cs -> UpdateProduct()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }

            return isSuceeded;
        }

        public int DeleteProduct(Product product)
        {
            MySqlConnection conn = new();
            string query = "DELETE FROM product WHERE id = @Id";
            int isSuceeded = 0;

            try
            {
                conn = _dbManager.GetConnection();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", product.Id);
                isSuceeded = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: ProductRepository.cs -> DeleteProduct()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }

            return isSuceeded;
        }

        public Product? GetProductIDbyName(string productName)
        {
            MySqlConnection conn = new();
            string query = "SELECT * FROM id WHERE product_name = @ProductName";
            try
            {
                conn = _dbManager.GetConnection();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@ProductName", productName);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Product
                    {
                        Id = reader.GetInt32("id"),
                        ProductName = reader.GetString("product_name"),
                        CategoryId = reader.GetInt32("category_id"),
                        Barcode = reader.GetString("barcode"),
                        Stock = reader.GetInt32("stock"),
                        StorageTemp = (StorageTemp)reader.GetInt32("storage_temp"),
                        WarehouseId = reader.GetInt32("warehouse_id")
                    };
                }
                else
                {
                    MessageBox.Show("해당 제품이 존재하지 않습니다. 제품을 등록하세요.", "제품 조회 실패", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: ProductRepository.cs -> GetProductIDbyName()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
            return null;
        }

        public int FindByUsername(string productName)
        {
            MessageBox.Show("FindByUsername() !!");
            MySqlConnection conn = new();
            string query = "SELECT EXISTS (SELECT 1 FROM product WHERE product_name = @ProductName)";
            int result;
            try
            {
                conn = _dbManager.GetConnection();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@ProductName", productName);

                result = Convert.ToInt32(command.ExecuteScalar()); //returns 1 if exists, 0 if not
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"<Error>\r\n- location: ProductRepository.cs -> FindByUsername()\r\n- message: {ex.Message}");
                result = -1;
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    _dbManager.CloseConnection(conn);
                }
            }
            return result;
        }
    }
}
