using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Database;
using MySql.Data.MySqlClient;
using FreshBox.Models;
using System.Windows;

namespace FreshBox.Repository
{
    public class CategoryRepository
    {
        private readonly MysqlDatabaseManager _dbManager;
        public CategoryRepository()
        {
            _dbManager = MysqlDatabaseManager.GetInstance();
        }

        public List<Category> GetAllCategories()
        {
            MessageBox.Show("GetAllCategories() called", "Category Repository", MessageBoxButton.OK, MessageBoxImage.Information);

            MySqlConnection conn = new();
            var categories = new List<Category>();
            string query = "SELECT id, category_name FROM category order by id;";

            try
            {
                conn = _dbManager.GetConnection();
                var command = new MySqlCommand(query, conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(
                        new Category(reader.GetInt32("id"), reader.GetString("category_name"))
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<Error>\r\n- location: CategoryRepository.cs -> GetAllCategories()\r\n- message: {ex.Message}");
            }
            finally
            {
                _dbManager.CloseConnection(conn);
            }
            return categories;
        }
    }
}
