using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Cookbook.DataModel;

namespace Cookbook.DataAccess
{
    public class DataRepository : IDataRepository //H
    {
        private readonly string _connectionString;
        private readonly string _dataBaseName;
        public DataRepository(string dataBaseName)
        {
            _connectionString = "Data Source = " + dataBaseName;
            _dataBaseName = dataBaseName;
        }

        public void CreateDataBase()
        {
            SQLiteConnection.CreateFile(_dataBaseName);
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

                    #region RecipeCategory
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS RecipeCategory (
                                            Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Title TEXT NOT NULL
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region Ingredients
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Ingredients (
                                            Id                     INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            IngredientCategoryId   INTEGER,
                                            Title                  TEXT NOT NULL,
                                            Kilocalories           INTEGER,
                                            Picture                BLOB,
                                            FOREIGN KEY(IngredientCategoryId) REFERENCES IngredientCategory(Id)
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

        #region Add
        public void AddIngredientCategory(string title)
        {
            AddCategory(title, "IngredientCategory");
        }
        public void AddRecipeCategory(string title)
        {
            AddCategory(title, "RecipeCategory");
        }
        private void AddCategory(string title, string table)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"INSERT INTO " + table + @"(Title)
                                        VALUES (@title)
                                        ;";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        public void AddIngredient(int ingredientCategoryId, string title, int kilocalories, byte[] picture)
        {
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

                    cmd.CommandText = @"INSERT INTO Ingredients (IngredientCategoryId, Title, Kilocalories, Picture)
                                        VALUES (@ingredientCategoryId, @title, @kilocalories, @picture)
                                        ;";
                    cmd.Parameters.AddWithValue("@ingredientCategoryId", ingredientCategoryId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@kilocalories", kilocalories);
                    cmd.Parameters.AddWithValue("@picture", picture);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void AddRecipe(int recipeCategoryId, string title, string instructions, int time)
        {
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

                    cmd.CommandText = @"INSERT INTO Recipes (RecipeCategoryId, Title, Instructions, Time)
                                        VALUES (@recipeCategoryId, @title, @instructions, @time)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeCategoryId", recipeCategoryId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@instructions", instructions);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void AddRecipeWithExistingIngredients(int recipeCategoryId, string title, string instructions, int time, List<Ingredient> ingredients)
        {
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

                    cmd.CommandText = @"INSERT INTO Recipes (RecipeCategoryId, Title, Instructions, Time)
                                            VALUES (@recipeCategoryId, @title, @instructions, @time)
                                            ;";
                    cmd.Parameters.AddWithValue("@recipeCategoryId", recipeCategoryId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@instructions", instructions);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"SELECT last_insert_rowid();";
                    int lastRecipeId = (int)cmd.ExecuteScalar();

                    cmd.CommandText = @"INSERT INTO IngredientRecipe (RecipeId, IngredientId, Quantity, Units)
                                        VALUES (@recipeId, @ingredientId, @quantity, @units)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", lastRecipeId);
                    foreach (var ingredient in ingredients)
                    {
                        cmd.Parameters.AddWithValue("@ingredientId", ingredient.Id);
                        cmd.Parameters.AddWithValue("@quantity", ingredient.Quantity);
                        cmd.Parameters.AddWithValue("@units", ingredient.Unit);
                        cmd.ExecuteNonQuery();
                    }

                }
                connection.Close();
            }
        }
        //not tested
        public void AddIngredientToRecipe(int recipeId, int ingredientId, int quantity, string units)
        {
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

                    cmd.CommandText = @"INSERT INTO IngredientRecipe (RecipeId, IngredientId, Quantity, Units)
                                        VALUES (@recipeId, @ingredientId, @quantity, @units)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@units", units);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested
        public void AddRecipePicture(int recipeId, int numberOfPicture, byte[] picture)
        {
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

                    cmd.CommandText = @"INSERT INTO RecipesPictures (RecipeId, NumberOfPicture, Picture)
                                        VALUES (@recipeId, @numberOfPicture, @picture)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@numberOfPicture", numberOfPicture);
                    cmd.Parameters.AddWithValue("@picture", picture);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region Update
        public void UpdateIngredientCategory(int id, string title)
        {
            UpdateCategory(id, title, "IngredientCategory");
        }
        public void UpdateRecipeCategory(int id, string title)
        {
            UpdateCategory(id, title, "RecipeCategory");
        }
        //not tested
        private void UpdateCategory(int id, string title, string table)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update " + table + @"
                                        SET Title = @title
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateCategoryOnIngredient(int ingredientId, int ingredientCategoryId)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Ingredients
                                        SET IngredientCategoryId = @categoryId
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", ingredientCategoryId);
                    cmd.Parameters.AddWithValue("@id", ingredientId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateIngredientTitle(int ingredientId, string title)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Ingredients
                                        SET Title = @title
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@id", ingredientId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateIngredientKilocalories(int ingredientId, int kilocalories)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Ingredients
                                        SET Kilocalories = @kilocalories
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@kilocalories", kilocalories);
                    cmd.Parameters.AddWithValue("@id", ingredientId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateIngredientPicture(int ingredientId, byte[] picture)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Ingredients
                                        SET Picture = @picture
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@picture", picture);
                    cmd.Parameters.AddWithValue("@id", ingredientId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateCategoryOnRecipe(int recipeId, int recipeCategoryId)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Recipes
                                        SET RecipeCategoryId = @categoryId
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", recipeCategoryId);
                    cmd.Parameters.AddWithValue("@id", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateRecipeTitle(int recipeId, string title)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Recipes
                                        SET Title = @title
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@id", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateRecipeInstructions(int recipeId, string instructions)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Recipes
                                        SET Instructions = @instructions
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@instructions", instructions);
                    cmd.Parameters.AddWithValue("@id", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateRecipeTime(int recipeId, int time)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Recipes
                                        SET Time = @time
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@id", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateIngredientQuantityOnRecipe(int recipeId, int ingredientId, int quantity)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update IngredientRecipe
                                        SET Quantity = @quantity 
                                        WHERE IngredientId = @ingredientId AND RecipeId = @recipeId
                                        ;";
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateIngredientUnitsOnRecipe(int recipeId, int ingredientId, string units)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update IngredientRecipe
                                        SET Units = @units 
                                        WHERE IngredientId = @ingredientId AND RecipeId = @recipeId
                                        ;";
                    cmd.Parameters.AddWithValue("@units", units);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void UpdateRecipePicture(int recipeId, int numberOfPicture, byte[] picture)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update RecipesPictures
                                        SET Picture = @picture 
                                        WHERE RecipeId = @recipeId AND NumberOfPicture = @numberOfPicture
                                        ;";
                    cmd.Parameters.AddWithValue("@picture", picture);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@numberOfPicture", numberOfPicture);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        #endregion

        #region Delete
        //not tested
        public void DeleteIngredientFromRecipe(int recipeId, int ingredientId)
        {
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

                    cmd.CommandText = @"DELETE FROM IngredientRecipe 
                                        WHERE RecipeId = @recipeId AND IngredientId = @ingredientId
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested
        public void DeleteRecipePicture(int recipeId, int numberOfPicture)
        {
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

                    cmd.CommandText = @"DELETE FROM RecipesPictures 
                                        WHERE RecipeId = @recipeId AND NumberOfPicture = @numberOfPicture
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@numberOfPicture", numberOfPicture);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region Get
        public List<Category> GetAllIngredientCategory()
        {
            return GetAllCategory("IngredientCategory");
        }
        public List<Category> GetAllRecipeCategory()
        {
            return GetAllCategory("RecipeCategory");
        }
        public List<Category> GetAllCategory(string table)
        {
            List<Category> result = new List<Category>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Id, Title FROM " + table + ";";
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
        public Category GetRecipeCategory(int id)
        {
            return GetCategory(id, "RecipeCategory");
        }
        public Category GetIngredientCategory(int id)
        {
            return GetCategory(id, "IngredientCategory");
        }
        private Category GetCategory(int id, string table)
        {
            Category result;
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Id, Title 
                                        FROM " + table + @" 
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        result = new Category
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title"))
                        };

                    }
                }
                connection.Close();
            }
            return result;
        }
        public Ingredient GetIngredient(int id)
        {
            Ingredient result;
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Ingredients.Id AS IngredientsId, IngredientCategory.Id AS CategoryId, 
                                               IngredientCategory.Title AS CategoryTitle, Ingredients.Title AS IngredientTitle,
                                               Ingredients.Kilocalories, Ingredients.Picture 
                                        FROM Ingredients INNER JOIN IngredientCategory ON Ingredients.IngredientCategoryId = IngredientCategory.Id
                                        WHERE Ingredients.Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        Category ingredientCategory = new Category
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                        };
                        
                        result = new Ingredient
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("IngredientsId")),
                            IngredientCategory = ingredientCategory,
                            Title = reader.GetString(reader.GetOrdinal("IngredientTitle")),
                            Calories = reader.GetInt32(reader.GetOrdinal("Kilocalories")),
                            Picture = reader["Picture"] as byte[]
                        };
                        
                    }
                }
                connection.Close();
            }
            return result;
        }

        //public List<Ingredient> GetIngredients(string title)

        //not tested
        public List<Ingredient> GetIngredientsOfCategory(int categoryId)
        {
            List<Ingredient> result = new List<Ingredient>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Ingredients.Id AS IngredientsId, IngredientCategory.Title AS CategoryTitle, 
                                               Ingredients.Title AS IngredientTitle,
                                               Ingredients.Kilocalories, Ingredients.Picture 
                                        FROM Ingredients INNER JOIN IngredientCategory ON Ingredients.IngredientCategoryId = IngredientCategory.Id
                                        WHERE Ingredients.IngredientCategoryId = @categoryId
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    using (var reader = cmd.ExecuteReader())
                    {

                        Category ingredientCategory = new Category
                        {
                            Id = categoryId,
                            Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                        };

                        while (reader.Read())
                        {
                            var ingredient = new Ingredient
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("IngredientsId")),
                                IngredientCategory = ingredientCategory,
                                Title = reader.GetString(reader.GetOrdinal("IngredientTitle")),
                                Calories = reader.GetInt32(reader.GetOrdinal("Kilocalories")),
                                Picture = reader["Picture"] as byte[]
                            };
                            result.Add(ingredient);
                        }

                    }
                }
                connection.Close();
            }
            return result;
        }
        //not tested
        public List<Ingredient> GetAllIngredient()
        {
            List<Ingredient> result = new List<Ingredient>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Ingredients.Id AS IngredientsId, IngredientCategory.Id AS CategoryId, 
                                               IngredientCategory.Title AS CategoryTitle, Ingredients.Title AS IngredientTitle,
                                               Ingredients.Kilocalories, Ingredients.Picture 
                                        FROM Ingredients INNER JOIN IngredientCategory ON Ingredients.IngredientCategoryId = IngredientCategory.Id
                                        ;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            Category ingredientCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };

                            var ingredient = new Ingredient
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("IngredientsId")),
                                IngredientCategory = ingredientCategory,
                                Title = reader.GetString(reader.GetOrdinal("IngredientTitle")),
                                Calories = reader.GetInt32(reader.GetOrdinal("Kilocalories")),
                                Picture = reader["Picture"] as byte[]
                            };
                            result.Add(ingredient);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        #endregion

        #region Not implemented
        public void DeleteIngredientCategory(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteRecipeCategory(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteIngredient(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }
        
        

        public List<Ingredient> GetIngredients(string title)
        {
            throw new NotImplementedException();
        }
        public Recipe GetRecipe(int id)
        {
            throw new NotImplementedException();
        }
        public List<Recipe> GetRecipes(string title)
        {
            throw new NotImplementedException();
        }
        public List<Recipe> GetRecipes(int time)
        {
            throw new NotImplementedException();
        }
        public List<Recipe> GetRecipesOfCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
        public List<Recipe> GetAllRecipe()
        {
            throw new NotImplementedException();
        }
        public List<Recipe> RecipesFromIngredients(List<Ingredient> ingredients)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
