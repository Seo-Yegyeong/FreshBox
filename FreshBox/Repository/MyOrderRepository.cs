using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FreshBox.Models;
using MySql.Data.MySqlClient;
using FreshBox.Database;
using System.Windows;

namespace FreshBox.Repository
{
    class MyOrderRepository
    {
        private readonly MysqlDatabaseManager _dbManager;

        public MyOrderRepository()
        {
            // MysqlDatabaseManager 싱글톤 인스턴스 얻기 (DB 연결 관리 담당)
            _dbManager = MysqlDatabaseManager.GetInstance();
        }

        public List<MyOrder> GetAllOrders()
        {
            MySqlConnection conn = new();
            var myOrders = new List<MyOrder>();
            string query = "SELECT * FROM my_order";

            try
            {
                conn = _dbManager.GetConnection();
                using var command = new MySqlCommand(query, conn);
                using var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    myOrders.Add(new MyOrder
                    {
                        Id = reader.GetInt32("id"),
                        Order_date = reader.GetDateTime("order_date"),
                        ProductId = reader.GetInt32("product_id"),
                        Quantity = reader.GetInt32("quantity")
                    });
                }
            }
            catch (Exception ex)
            {
                // 예외 처리 로직 (예: 로그 기록)
                Console.WriteLine($"<Error>\r\n- location: MyOrderRepository.cs -> GetAllOrders()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
            return myOrders;
        }

        public void InsertMyOrder(MyOrder order)
        {
            MySqlConnection conn = new();
            string query = "INSERT INTO my_order (order_date, product_id, quantity) VALUES (@order_date, @product_id, @quantity)";
            try
            {
                conn = _dbManager.GetConnection();
                using var command = new MySqlCommand(query, conn);
                // AddWithValue()와 @파라미터를 사용하여 SQL 인젝션 공격 방지
                command.Parameters.AddWithValue("@order_date", order.Order_date);
                command.Parameters.AddWithValue("@product_id", order.ProductId);
                command.Parameters.AddWithValue("@quantity", order.Quantity);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // 예외 처리 로직 (예: 로그 기록)
                Console.WriteLine($"<Error>\r\n- location: MyOrderRepository.cs -> AddMyOrder()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
        }

        public void UpdateMyOrder(MyOrder order)
        {
            MySqlConnection conn = new();
            string query = "UPDATE my_order SET order_date = @order_date, product_id = @product_id, quantity = @quantity WHERE id = @id";
            try
            {
                conn = _dbManager.GetConnection();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@order_date", order.Order_date);
                command.Parameters.AddWithValue("@product_id", order.ProductId);
                command.Parameters.AddWithValue("@quantity", order.Quantity);
                command.Parameters.AddWithValue("@id", order.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: MyOrderRepository.cs -> UpdateMyOrder()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
        }

        public void DeleteMyOrder(int orderId)
        {
            MySqlConnection conn = new();
            string query = "DELETE FROM my_order WHERE id = @id";
            try
            {
                conn = _dbManager.GetConnection();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", orderId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: MyOrderRepository.cs -> DeleteMyOrder()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
        }

        public int GetProductIdByName(string productName)
        {
            MySqlConnection conn = new();
            string query = "SELECT id FROM product WHERE product_name = @name";
            try
            {
                conn = _dbManager.GetConnection();
                using var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@name", productName.Trim());
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: MyOrderRepository.cs -> GetProductIdFromName()\r\n- message: {ex.Message}");
                return -1; // 오류 발생 시 -1 반환
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
        }
    }
}
