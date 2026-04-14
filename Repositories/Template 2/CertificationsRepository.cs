using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories.Template_2
{
    public class CertificationsRepository
    {
        private readonly string _connectionString;

        public CertificationsRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Insert(Certifications certification)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertCertification2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", certification.ResumeId);
                command.Parameters.AddWithValue("@CertificationName", certification.CertificationName);
                command.Parameters.AddWithValue("@IssuingOrganization", certification.IssuingOrganization);
                command.Parameters.AddWithValue("@Years", certification.Years);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Update(Certifications certification)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateCertification2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CertificationId", certification.CertificationId);
                command.Parameters.AddWithValue("@CertificationName", certification.CertificationName);
                command.Parameters.AddWithValue("@IssuingOrganization", certification.IssuingOrganization);
                command.Parameters.AddWithValue("@Years", certification.Years);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Delete(int certificationId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteCertification2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CertificationId", certificationId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public Certifications GetById(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetCertificationsByResumeId2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Certifications
                    {
                        CertificationId = (int)reader["CertificationId"],
                        ResumeId = (int)reader["ResumeId"],
                        CertificationName = (string)reader["CertificationName"],
                        IssuingOrganization = (string)reader["IssuingOrganization"],
                        Years = (int)reader["Years"]
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return null; // Return null if not found
        }

        public IEnumerable<Certifications> GetAllCertifications()
        {
            var certifications = new List<Certifications>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllCertifications2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    certifications.Add(new Certifications
                    {
                        CertificationId = (int)reader["CertificationId"],
                        ResumeId = (int)reader["ResumeId"],
                        CertificationName = (string)reader["CertificationName"],
                        IssuingOrganization = (string)reader["IssuingOrganization"],
                        Years = (int)reader["Years"]
                    });
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return certifications;
        }
    }
}
