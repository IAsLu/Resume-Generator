using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using FINAL.Models;

namespace FINAL.Repositories
{
    public class WorkExperienceRepository
    {
        private readonly string _connectionString;

        public WorkExperienceRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<WorkExperience> GetAllWorkExperiences()
        {
            var workExperience = new List<WorkExperience>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllWorkExperiences", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    workExperience.Add(new WorkExperience
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        JobTitle = reader["JobTitle"].ToString(),
                        Company = reader["Company"].ToString(),
                        Location = reader["Location"].ToString(),
                        Years = reader["Years"].ToString()
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

            return workExperience;
        }

        public WorkExperience GetWorkExperienceByResumeId(int resumeId)
        {
            WorkExperience workExperience = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetWorkExperience", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    workExperience = new WorkExperience
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        JobTitle = reader["JobTitle"].ToString(),
                        Company = reader["Company"].ToString(),
                        Location = reader["Location"].ToString(),
                        Years = reader["Years"].ToString()
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

            return workExperience;
        }

        public void CreateWorkExperience(WorkExperience workExperience)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("CreateWorkExperience", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ResumeId", workExperience.ResumeId);
                command.Parameters.AddWithValue("@JobTitle", workExperience.JobTitle);
                command.Parameters.AddWithValue("@Company", workExperience.Company);
                command.Parameters.AddWithValue("@Location", workExperience.Location);
                command.Parameters.AddWithValue("@Years", workExperience.Years);

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

        public void UpdateWorkExperience(WorkExperience workExperience)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateWorkExperience", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", workExperience.Id);
                command.Parameters.AddWithValue("@ResumeId", workExperience.ResumeId);
                command.Parameters.AddWithValue("@JobTitle", workExperience.JobTitle);
                command.Parameters.AddWithValue("@Company", workExperience.Company);
                command.Parameters.AddWithValue("@Location", workExperience.Location);
                command.Parameters.AddWithValue("@Years", workExperience.Years);

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

        public void DeleteWorkExperience(int id)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteWorkExperience", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
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
