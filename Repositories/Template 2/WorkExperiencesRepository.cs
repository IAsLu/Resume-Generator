using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories.Template_2
{
    public class WorkExperiencesRepository
    {
        private readonly string _connectionString;

        public WorkExperiencesRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Insert(WorkExperiences workExperiences)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertWorkExperience2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", workExperiences.ResumeId);
                command.Parameters.AddWithValue("@JobTitle", workExperiences.JobTitle ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CompanyName", workExperiences.CompanyName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@City", workExperiences.City ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@State", workExperiences.State ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(workExperiences.StartDate) ? (object)DBNull.Value : workExperiences.StartDate);
                command.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(workExperiences.EndDate) ? (object)DBNull.Value : workExperiences.EndDate);
                command.Parameters.AddWithValue("@Responsibilities", workExperiences.Responsibilities ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Update(WorkExperiences workExperiences)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateWorkExperience2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ExperienceId", workExperiences.ExperienceId);
                command.Parameters.AddWithValue("@JobTitle", workExperiences.JobTitle ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CompanyName", workExperiences.CompanyName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@City", workExperiences.City ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@State", workExperiences.State ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(workExperiences.StartDate) ? (object)DBNull.Value : workExperiences.StartDate);
                command.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(workExperiences.EndDate) ? (object)DBNull.Value : workExperiences.EndDate);
                command.Parameters.AddWithValue("@Responsibilities", workExperiences.Responsibilities ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Delete(int experienceId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteWorkExperience2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ExperienceId", experienceId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public WorkExperiences GetById(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            WorkExperiences workExperience = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetWorkExperiencesByResumeId2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    workExperience = new WorkExperiences
                    {
                        ExperienceId = reader.GetInt32(reader.GetOrdinal("ExperienceId")),
                        ResumeId = reader.GetInt32(reader.GetOrdinal("ResumeId")),
                        JobTitle = reader["JobTitle"] as string,
                        CompanyName = reader["CompanyName"] as string,
                        City = reader["City"] as string,
                        State = reader["State"] as string,
                        StartDate = reader["StartDate"] as string,
                        EndDate = reader["EndDate"] as string,
                        Responsibilities = reader["Responsibilities"] as string
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return workExperience;
        }

        public IEnumerable<WorkExperiences> GetByResumeId(int resumeId)
        {
            var workExperiences = new List<WorkExperiences>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetWorkExperiencesByResumeId2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    workExperiences.Add(new WorkExperiences
                    {
                        ExperienceId = reader.GetInt32(reader.GetOrdinal("ExperienceId")),
                        ResumeId = reader.GetInt32(reader.GetOrdinal("ResumeId")),
                        JobTitle = reader["JobTitle"] as string,
                        CompanyName = reader["CompanyName"] as string,
                        City = reader["City"] as string,
                        State = reader["State"] as string,
                        StartDate = reader["StartDate"] as string,
                        EndDate = reader["EndDate"] as string,
                        Responsibilities = reader["Responsibilities"] as string
                    });
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return workExperiences;
        }
    }
}
