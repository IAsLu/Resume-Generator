using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FINAL.Models;

namespace FINAL.Repositories
{
    public class EducationRepository
    {
        private readonly string _connectionString;

        public EducationRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<Education> GetAllEducations()
        {
            var educations = new List<Education>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllEducations", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    educations.Add(new Education
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        Degree = reader["Degree"].ToString(),
                        Institution = reader["Institution"].ToString(),
                        GraduationYear = reader["GraduationYear"].ToString()
                    });
                }
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return educations;
        }

        public Education GetEducationById(int resumeId)
        {
            Education education = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetEducation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    education = new Education
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        Degree = reader["Degree"].ToString(),
                        Institution = reader["Institution"].ToString(),
                        GraduationYear = reader["GraduationYear"].ToString()
                    };
                }
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return education;
        }

        public void CreateEducation(Education education)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("CreateEducation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ResumeId", education.ResumeId);
                command.Parameters.AddWithValue("@Degree", education.Degree);
                command.Parameters.AddWithValue("@Institution", education.Institution);
                command.Parameters.AddWithValue("@GraduationYear", education.GraduationYear);

                connection.Open();
                command.ExecuteNonQuery(); // Execute the insert command
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

        public void UpdateEducation(Education education)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateEducation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", education.Id);
                command.Parameters.AddWithValue("@ResumeId", education.ResumeId);
                command.Parameters.AddWithValue("@Degree", education.Degree);
                command.Parameters.AddWithValue("@Institution", education.Institution);
                command.Parameters.AddWithValue("@GraduationYear", education.GraduationYear);

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

        public void DeleteEducation(int id)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteEducation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
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
