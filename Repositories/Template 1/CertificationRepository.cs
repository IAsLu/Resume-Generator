using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using FINAL.Models;
using System;

namespace FINAL.Repositories
{
    public class CertificationRepository
    {
        private readonly string _connectionString;

        public CertificationRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<Certification> GetAllCertifications()
        {
            var certifications = new List<Certification>();
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllCertifications", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        certifications.Add(new Certification
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                            ResumeId = reader["ResumeId"] != DBNull.Value ? Convert.ToInt32(reader["ResumeId"]) : 0,
                            CertificationName = reader["CertificationName"].ToString(),
                            IssuedBy = reader["IssuedBy"].ToString(),
                            Year = reader["Year"].ToString()
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

            return certifications;
        }

        public Certification GetCertificationById(int resumeId)
        {
            Certification certification = null;
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetCertifications", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        certification = new Certification
                        {
                            Id = (int)reader["Id"],
                            ResumeId = (int)reader["ResumeId"],
                            CertificationName = reader["CertificationName"].ToString(),
                            IssuedBy = reader["IssuedBy"].ToString(),
                            Year = reader["Year"].ToString(),
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

            return certification;
        }

        public void CreateCertification(Certification certification)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("CreateCertification", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ResumeId", certification.ResumeId);
                command.Parameters.AddWithValue("@CertificationName", certification.CertificationName);
                command.Parameters.AddWithValue("@IssuedBy", certification.IssuedBy);
                command.Parameters.AddWithValue("@Year", certification.Year);

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

        public void UpdateCertification(Certification certification)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateCertification", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", certification.Id);
                command.Parameters.AddWithValue("@ResumeId", certification.ResumeId);
                command.Parameters.AddWithValue("@CertificationName", certification.CertificationName);
                command.Parameters.AddWithValue("@IssuedBy", certification.IssuedBy);
                command.Parameters.AddWithValue("@Year", certification.Year);

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

        public void DeleteCertification(int id)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteCertification", connection);
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
