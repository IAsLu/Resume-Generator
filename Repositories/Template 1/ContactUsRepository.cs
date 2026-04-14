using FINAL.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories
{
    public class ContactUsRepository
    {
        private readonly string _connectionString;

        public ContactUsRepository()
        {
            // Retrieve connection string from web.config
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void SaveContactUsMessage(ContactUsViewModel model)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertContactUsMessage", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Subject", model.Subject ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Message", model.Message ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
