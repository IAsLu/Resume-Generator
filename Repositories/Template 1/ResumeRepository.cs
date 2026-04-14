using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System;
using FINAL.Models;

namespace FINAL.Repositories
{
    public class ResumeRepository
    {
        private readonly string _connectionString;

        public ResumeRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<Resume> GetAllResumes()
        {
            var resumes = new List<Resume>();
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllResumes", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resumes.Add(new Resume
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            LinkedIn = reader["LinkedIn"].ToString(),
                            GitHub = reader["GitHub"].ToString(),
                            ProfessionalSummary = reader["ProfessionalSummary"].ToString()
                           
                        });
                    }
                }
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

            return resumes;
        }

        public Resume GetResumeById(int id)
        {
            Resume resume = null;
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetResume", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resume = new Resume
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            LinkedIn = reader["LinkedIn"].ToString(),
                            GitHub = reader["GitHub"].ToString(),
                            ProfessionalSummary = reader["ProfessionalSummary"].ToString()
                   
                        };
                    }
                }
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

            return resume;
        }

        public int CreateResume(Resume resume)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("CreateResume", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", resume.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", resume.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Phone", resume.Phone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@LinkedIn", resume.LinkedIn ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@GitHub", resume.GitHub ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ProfessionalSummary", resume.ProfessionalSummary ?? (object)DBNull.Value);


                connection.Open();
                var result = command.ExecuteScalar();

                // Ensure result is not null
                if (result == null || result == DBNull.Value)
                {
                    throw new Exception("Failed to retrieve new ID.");
                }

                int newId = Convert.ToInt32(result);
                return newId;
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

        public void UpdateResume(Resume resume)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateResume", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", resume.Id);
                command.Parameters.AddWithValue("@Name", resume.Name);
                command.Parameters.AddWithValue("@Email", resume.Email);
                command.Parameters.AddWithValue("@Phone", resume.Phone);
                command.Parameters.AddWithValue("@LinkedIn", resume.LinkedIn);
                command.Parameters.AddWithValue("@GitHub", resume.GitHub);
                command.Parameters.AddWithValue("@ProfessionalSummary", resume.ProfessionalSummary);
               

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

        public void DeleteResume(int id)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteResume", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

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
