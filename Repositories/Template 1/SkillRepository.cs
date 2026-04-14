using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using FINAL.Models;

namespace FINAL.Repositories
{
    public class SkillRepository
    {
        private readonly string _connectionString;

        public SkillRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            var skills = new List<Skill>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetAllSkills", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    skills.Add(new Skill
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        SkillName = reader["SkillName"].ToString()
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

            return skills;
        }

        public Skill GetSkillById(int resumeId)
        {
            Skill skill = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetSkills", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    skill = new Skill
                    {
                        Id = (int)reader["Id"],
                        ResumeId = (int)reader["ResumeId"],
                        SkillName = reader["SkillName"].ToString()
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

            return skill;
        }

        public void CreateSkill(Skill skill)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("CreateSkill", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ResumeId", skill.ResumeId);
                command.Parameters.AddWithValue("@SkillName", skill.SkillName);

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

        public void UpdateSkill(Skill skill)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateSkill", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", skill.Id);
                command.Parameters.AddWithValue("@ResumeId", skill.ResumeId);
                command.Parameters.AddWithValue("@SkillName", skill.SkillName);

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

        public void DeleteSkill(int id)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteSkill", connection)
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
