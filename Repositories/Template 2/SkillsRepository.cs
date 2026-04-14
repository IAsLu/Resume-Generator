using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FINAL.Repositories.Template_2
{
    public class SkillsRepository
    {
        private readonly string _connectionString;

        public SkillsRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Method to insert a new skill
        public void Insert(Skills skill)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("InsertSkill2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", skill.ResumeId);
                command.Parameters.AddWithValue("@SkillName", skill.SkillName);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        // Method to update an existing skill
        public void Update(Skills skill)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("UpdateSkill2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SkillId", skill.SkillId);
                command.Parameters.AddWithValue("@SkillName", skill.SkillName);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        // Method to delete a skill
        public void Delete(int skillId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("DeleteSkill2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SkillId", skillId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command?.Dispose();
                connection?.Dispose();
            }
        }

        // Method to get skills by resume ID
        public Skills GetById(int resumeId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            Skills skill = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetSkillsByResumeId2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResumeId", resumeId);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    skill = new Skills
                    {
                        SkillId = (int)reader["SkillId"],
                        ResumeId = (int)reader["ResumeId"],
                        SkillName = (string)reader["SkillName"]
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return skill;
        }

        // Method to retrieve all skills
        public IEnumerable<Skills> GetAllSkills()
        {
            var skills = new List<Skills>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("GetSkills2", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    skills.Add(new Skills
                    {
                        SkillId = (int)reader["SkillId"],
                        ResumeId = (int)reader["ResumeId"],
                        SkillName = (string)reader["SkillName"]
                    });
                }
            }
            finally
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();
            }

            return skills;
        }
    }
}
