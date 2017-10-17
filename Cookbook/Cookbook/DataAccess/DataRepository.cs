using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Cookbook.DataModel;

namespace Cookbook.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;
        public DataRepository(string dataBaseName)
        {
            _connectionString = "Data Source = " + dataBaseName;
        }

        public void CreateDataBase(string dbName) //H
        {
            SQLiteConnection.CreateFile(dbName);
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"PRAGMA foreign_keys = ON;";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    #region IngredientCategory
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS IngredientCategory (
                                            Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Title TEXT NOT NULL
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region Ingredients
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Ingredients (
                                            Id           INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            CategoryId   INTEGER,
                                            Title        TEXT NOT NULL,
                                            Kilocalories INTEGER,
                                            Picture      BLOB,
                                            FOREIGN KEY(CategoryId) REFERENCES IngredientCategory(Id)
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region RecipeCategory
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS RecipeCategory (
                                            Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Title TEXT NOT NULL
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region Recipes
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Recipes (
                                            Id               INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            RecipeCategoryId INTEGER,
                                            Title            TEXT NOT NULL,
                                            Instructions     TEXT NOT NULL,
                                            Time             INTEGER,
                                            FOREIGN KEY(RecipeCategoryId) REFERENCES RecipeCategory(Id)
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region RecipesPicture
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS RecipesPictures (
                                            Id              INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            NumberOfPicture INTEGER NOT NULL,
                                            RecipeId        INTEGER NOT NULL,
                                            Picture         BLOB NOT NULL,
                                            FOREIGN KEY(RecipeId) REFERENCES Recipes(Id)
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region IngredientRecipe
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS IngredientRecipe (
                                            Id           INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            IngredientId INTEGER NOT NULL,
                                            RecipeId     INTEGER NOT NULL,
                                            Quantity     INTEGER NOT NULL,
                                            Units        TEXT NOT NULL,
                                            FOREIGN KEY(IngredientId) REFERENCES Ingredients(Id),
                                            FOREIGN KEY(RecipeId) REFERENCES Recipes(Id)
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion
                }
                connection.Close();
            }
        }

        public void AddIngredientCategory(string title) //H
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"INSERT INTO IngredientCategory (Title)
                                            VALUES (@title)
                                            ";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }

        public void AddRecipeCategory(string title) //H
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"INSERT INTO RecipeCategory (Title)
                                            VALUES (@title)
                                            ";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }

        public List<Category> GetAllIngredientCategory() //H
        {
            List<Category> result = new List<Category>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Id, Title FROM IngredientCategory";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title"))
                            };
                            result.Add(category);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public List<Category> GetAllRecipeCategory() //H
        {
            List<Category> result = new List<Category>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Id, Title FROM RecipeCategory";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title"))
                            };
                            result.Add(category);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
    }
}
