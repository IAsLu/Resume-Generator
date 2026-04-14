using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System;
using FINAL.Models;

namespace FINAL.Repositories
{
    public class ProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            var projects = new List<Project>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllProjects", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        ProjectName = reader["ProjectName"].ToString(),
                        ProjectDescription = reader["ProjectDescription"].ToString()
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

            return projects;
        }

        public Project GetProjectById(int resumeId)
        {
            Project project = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetProjects", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    project = new Project
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        ProjectName = reader["ProjectName"].ToString(),
                        ProjectDescription = reader["ProjectDescription"].ToString(),
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

            return project;
        }

        public void CreateProject(Project project)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("CreateProject", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ResumeId", project.ResumeId);
                command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);

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

        public void UpdateProject(Project project)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateProject", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", project.Id);
                command.Parameters.AddWithValue("@ResumeId", project.ResumeId);
                command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);

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

        public void DeleteProject(int id)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteProject", connection)
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
