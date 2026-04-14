using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FINAL.Models;

namespace FINAL.Repositories
{
    public class UserRegistrationRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void InsertUser(UserRegistration user)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand("spInsertUserRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@State", user.State ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@City", user.City ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
        }

        public UserRegistration GetUser(int userId)
        {
            UserRegistration user = null;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand("spGetUserRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserRegistration
                    {
                        UserId = (int)reader["UserId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Gender = reader["Gender"].ToString(),
                        PhoneNumber = reader["PhoneNumber"]?.ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"]?.ToString(),
                        State = reader["State"]?.ToString(),
                        City = reader["City"]?.ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        ConfirmPassword = reader["ConfirmPassword"].ToString()
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }

            return user;
        }

        public UserRegistration GetUserByEmail(string email)
        {
            UserRegistration user = null;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand("spGetUserRegistrationByEmail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserRegistration
                    {
                        UserId = (int)reader["UserId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Gender = reader["Gender"].ToString(),
                        PhoneNumber = reader["PhoneNumber"]?.ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"]?.ToString(),
                        State = reader["State"]?.ToString(),
                        City = reader["City"]?.ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        ConfirmPassword = reader["ConfirmPassword"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }
            finally
            {
                reader?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }

            return user;
        }

        public void UpdateUser(UserRegistration user)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand("spUpdateUserRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@State", user.State ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@City", user.City ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
        }

        public void DeleteUser(int userId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand("spDeleteUserRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
        }

        public List<UserRegistration> GetAllUsers()
        {
            var users = new List<UserRegistration>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand("spGetAllUserRegistrations", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var user = new UserRegistration
                    {
                        UserId = (int)reader["UserId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Gender = reader["Gender"].ToString(),
                        PhoneNumber = reader["PhoneNumber"]?.ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"]?.ToString(),
                        State = reader["State"]?.ToString(),
                        City = reader["City"]?.ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        ConfirmPassword = reader["ConfirmPassword"].ToString()
                    };
                    users.Add(user);
                }
            }
            finally
            {
                reader?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }

            return users;
        }
    }
}
