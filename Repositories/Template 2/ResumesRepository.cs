using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories.Template_2
{
    public class ResumesRepository
    {
        private readonly string _connectionString;

        // Constructor to initialize the connection string
        public ResumesRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public ResumesRepository()
        {
        }

        // Method to insert a new resume and return the generated ResumeId
        public int Insert(Resumes resume)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int resumeId = 0;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertResume2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add input parameters
                command.Parameters.AddWithValue("@Name", resume.Name);
                command.Parameters.AddWithValue("@Email", resume.Email);
                command.Parameters.AddWithValue("@Phone", resume.Phone);
                command.Parameters.AddWithValue("@Address", resume.Address);
                command.Parameters.AddWithValue("@ProfessionalSummary", resume.ProfessionalSummary);

                // Add an output parameter for ResumeId
                var outputIdParam = new SqlParameter("@ResumeId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                connection.Open();
                command.ExecuteNonQuery();

                // Retrieve the ResumeId from the output parameter
                resumeId = (int)outputIdParam.Value;
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }

            return resumeId;
        }

        // Method to update an existing resume
        public int Update(Resumes resume)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int rowsAffected = 0;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateResume2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resume.ResumeId);
                command.Parameters.AddWithValue("@Name", resume.Name);
                command.Parameters.AddWithValue("@Email", resume.Email);
                command.Parameters.AddWithValue("@Phone", resume.Phone);
                command.Parameters.AddWithValue("@Address", resume.Address);
                command.Parameters.AddWithValue("@ProfessionalSummary", resume.ProfessionalSummary);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }

            return rowsAffected;
        }

        // Method to delete a resume
        public void Delete(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteResume2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        // Method to get a resume by its ID
        public Resumes GetById(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            Resumes resume = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetResumeById2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    resume = new Resumes
                    {
                        ResumeId = (int)reader["ResumeId"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        ProfessionalSummary = reader["ProfessionalSummary"].ToString()
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return resume;
        }

        // Method to get all resumes
        public IEnumerable<Resumes> GetAll()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            var resumes = new List<Resumes>();

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllResumes2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resumes.Add(new Resumes
                    {
                        ResumeId = (int)reader["ResumeId"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        ProfessionalSummary = reader["ProfessionalSummary"].ToString()
                    });
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return resumes;
        }
    }
}
