using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories.Template_2
{
    public class EducationsRepository
    {
        private readonly string _connectionString;

        public EducationsRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Insert(Educations education)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertEducation2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", education.ResumeId);
                command.Parameters.AddWithValue("@Degree", education.Degree);
                command.Parameters.AddWithValue("@Major", education.Major);
                command.Parameters.AddWithValue("@SchoolName", education.SchoolName);
                command.Parameters.AddWithValue("@City", education.City);
                command.Parameters.AddWithValue("@State", education.State);
                command.Parameters.AddWithValue("@GraduationYear", education.GraduationYear);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Update(Educations education)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateEducation2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@EducationId", education.EducationId);
                command.Parameters.AddWithValue("@Degree", education.Degree);
                command.Parameters.AddWithValue("@Major", education.Major);
                command.Parameters.AddWithValue("@SchoolName", education.SchoolName);
                command.Parameters.AddWithValue("@City", education.City);
                command.Parameters.AddWithValue("@State", education.State);
                command.Parameters.AddWithValue("@GraduationYear", education.GraduationYear);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Delete(int educationId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteEducation2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@EducationId", educationId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public Educations GetByResumeId(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetEducationsByResumeId2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Educations
                    {
                        EducationId = (int)reader["EducationId"],
                        ResumeId = (int)reader["ResumeId"],
                        Degree = (string)reader["Degree"],
                        Major = (string)reader["Major"],
                        SchoolName = (string)reader["SchoolName"],
                        City = (string)reader["City"],
                        State = (string)reader["State"],
                        GraduationYear = (int)reader["GraduationYear"]
                    };
                }
                return null;
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }
        }
    }
}
