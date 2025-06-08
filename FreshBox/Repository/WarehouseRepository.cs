//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FreshBox.Models;
//using FreshBox.Database;
//using MySql.Data.MySqlClient;

//namespace FreshBox.Repository
//{
//    public class WarehouseRepository
//    {
//        MysqlDatabaseManager _dbManager;

//        public WarehouseRepository()
//        {
//            _dbManager = MysqlDatabaseManager.GetInstance();
//        }

//        public List<Warehouse> GetAllWarehouses()
//        {
//            MySqlConnection conn = new();
//            var warehouses = new List<Warehouse>();
//            string query = "SELECT id, location, temp_control FROM warehouse";

//            try
//            {
//                conn = _dbManager.GetConnection();
//                var command = new MySqlCommand(query, conn);
//                var reader = command.ExecuteReader();
//                while (reader.Read())
//                {
//                    warehouses.Add(new Warehouse(
//                        reader.GetInt32("id"),
//                        reader.GetString("location"),
//                        reader.GetString("temp_control")
//                    ));
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"<Error>\r\n- location: WarehouseRepository.cs -> GetAllWarehouses()\r\n- message: {ex.Message}");
//            }
//            finally
//            {
//                _dbManager.CloseConnection(conn);
//            }
//            return warehouses;
//        }
//    }
//}
