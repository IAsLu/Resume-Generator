using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories.Template_2
{
    public class ProjectsRepository
    {
        private readonly string _connectionString;

        public ProjectsRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Insert(Projects project)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertProject2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", project.ResumeId);
                command.Parameters.AddWithValue("@ProjectTitle", project.ProjectTitle);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@Role", project.Role);
                command.Parameters.AddWithValue("@Outcome", project.Outcome);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Update(Projects project)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateProject2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                command.Parameters.AddWithValue("@ProjectTitle", project.ProjectTitle);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@Role", project.Role);
                command.Parameters.AddWithValue("@Outcome", project.Outcome);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public void Delete(int projectId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteProject2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ProjectId", projectId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        public Projects GetByResumeId(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            Projects project = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetProjectsByResumeId2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    project = new Projects
                    {
                        ProjectId = (int)reader["ProjectId"],
                        ResumeId = (int)reader["ResumeId"],
                        ProjectTitle = (string)reader["ProjectTitle"],
                        Description = (string)reader["Description"],
                        Role = (string)reader["Role"],
                        Outcome = (string)reader["Outcome"]
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return project;
        }
    }
}
