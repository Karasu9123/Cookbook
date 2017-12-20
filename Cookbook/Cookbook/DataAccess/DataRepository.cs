using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Cookbook.DataModel;

namespace Cookbook.DataAccess
{
    public class DataRepository : IDataRepository
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

                    #region Units
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Units (
                                            Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Title TEXT NOT NULL
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region Ingredients
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Ingredients (
                                            Id                     INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            IngredientCategoryId   INTEGER NOT NULL,
                                            Title                  TEXT NOT NULL,
                                            Kilocalories           INTEGER NOT NULL,
                                            Picture                BLOB,
                                            FOREIGN KEY(IngredientCategoryId) REFERENCES IngredientCategory(Id)
                                            );";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region Recipes
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Recipes (
                                            Id               INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            RecipeCategoryId INTEGER NOT NULL,
                                            Title            TEXT NOT NULL,
                                            Description      TEXT NOT NULL,
                                            Time             INTEGER NOT NULL,
                                            Picture          BLOB,
                                            Instructions     TEXT NOT NULL,
                                            FOREIGN KEY(RecipeCategoryId) REFERENCES RecipeCategory(Id)
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
                                            UnitId       INTEGER NOT NULL,
                                            FOREIGN KEY(IngredientId) REFERENCES Ingredients(Id),
                                            FOREIGN KEY(RecipeId) REFERENCES Recipes(Id)
                                            FOREIGN KEY(UnitId) REFERENCES Units(Id)
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
        //not tested
        public void AddUnit(string title)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"INSERT INTO Units (Title)
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
        public void AddRecipe(int recipeCategoryId, string title, string description, string instructions, int time, byte[] picture)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"INSERT INTO Recipes (RecipeCategoryId, Title, Description, Time, Picture, Instructions)
                                        VALUES (@recipeCategoryId, @title, @description, @time, @picture, @instructions)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeCategoryId", recipeCategoryId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@picture", picture);
                    cmd.Parameters.AddWithValue("@instructions", instructions);
                    
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        public void AddRecipeWithExistingIngredients(int recipeCategoryId, string title, string description, string instructions,
            int time, byte[] picture, List<Ingredient> ingredients)
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

                    cmd.CommandText = @"INSERT INTO Recipes (RecipeCategoryId, Title, Description, Time, Picture, Instructions)
                                            VALUES (@recipeCategoryId, @title, @description, @time, @picture, @instructions)
                                            ;";
                    cmd.Parameters.AddWithValue("@recipeCategoryId", recipeCategoryId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@picture", picture);
                    cmd.Parameters.AddWithValue("@instructions", instructions);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"SELECT last_insert_rowid();";
                    int lastRecipeId = (int)cmd.ExecuteScalar();

                    cmd.CommandText = @"INSERT INTO IngredientRecipe (RecipeId, IngredientId, Quantity, UnitId)
                                        VALUES (@recipeId, @ingredientId, @quantity, @unitId)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", lastRecipeId);
                    foreach (var ingredient in ingredients)
                    {
                        cmd.Parameters.AddWithValue("@ingredientId", ingredient.Id);
                        cmd.Parameters.AddWithValue("@quantity", ingredient.Quantity);
                        cmd.Parameters.AddWithValue("@unitId", ingredient.Unit.Id);
                        cmd.ExecuteNonQuery();
                    }

                }
                connection.Close();
            }
        }
        //not tested
        public void AddIngredientToRecipe(int recipeId, int ingredientId, int quantity, int unitId)
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

                    cmd.CommandText = @"INSERT INTO IngredientRecipe (RecipeId, IngredientId, Quantity, UnitId)
                                        VALUES (@recipeId, @ingredientId, @quantity, @unitId)
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@unitId", unitId);
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
        public void UpdateUnit(Unit unit)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"Update Units
                                        SET Title = @title
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@title", unit.Title);
                    cmd.Parameters.AddWithValue("@id", unit.Id);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        /// <summary>
        /// Update CategoryId, Title, Kilocalories, Picture
        /// </summary>
        /// <param name="ingredient">Existing ingredient</param>
        public void UpdateIngredient(Ingredient ingredient)
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

                    cmd.CommandText = @"Update Ingredients
                                        SET IngredientCategoryId = @categoryId, Title = @title, 
                                            Kilocalories = @calories, Picture = @picture
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", ingredient.Category.Id);
                    cmd.Parameters.AddWithValue("@title", ingredient.Title);
                    cmd.Parameters.AddWithValue("@calories", ingredient.Calories);
                    cmd.Parameters.AddWithValue("@picture", ingredient.Picture);
                    cmd.Parameters.AddWithValue("@id", ingredient.Id);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        /// <summary>
        /// Update CategoryId, Title, Description, Time, Picture, Instructions
        /// </summary>
        /// <param name="recipe">Existing recipe</param>
        public void UpdateRecipe(Recipe recipe)
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

                    cmd.CommandText = @"Update Recipes
                                        SET RecipeCategoryId = @categoryId, Title = @title, Description = @description,
                                            Time = @time, Picture = @picture, Instructions = @instructions
                                        WHERE Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", recipe.Category.Id);
                    cmd.Parameters.AddWithValue("@title", recipe.Title);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@time", recipe.Time);
                    cmd.Parameters.AddWithValue("@picture", recipe.Picture);
                    cmd.Parameters.AddWithValue("@instructions", recipe.Instruction);
                    cmd.Parameters.AddWithValue("@id", recipe.Id);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        //not tested
        /// <summary>
        /// Update Quantity and UnitId
        /// </summary>
        /// <param name="recipeId">Existing recipe</param>
        /// <param name="ingredient">Ingredient of recipe</param>
        public void UpdateIngredientRecipe(int recipeId, Ingredient ingredient)
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

                    cmd.CommandText = @"Update IngredientRecipe
                                        SET Quantity = @quantity, UnitId = @unitId
                                        WHERE IngredientId = @ingredientId AND RecipeId = @recipeId
                                        ;";
                    cmd.Parameters.AddWithValue("@quantity", ingredient.Quantity);
                    cmd.Parameters.AddWithValue("@unitId", ingredient.Unit.Id);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredient.Id);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        #endregion

        #region Delete
        //not tested !!!
        public void DeleteIngredientCategory(int id)
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

                    //+2 new connection every iteration
                    GetIngredientsOfCategory(id).ForEach(ingredient => DeleteIngredient(ingredient.Id));

                    cmd.CommandText = @"DELETE FROM IngredientCategory 
                                        WHERE Id = @categoryId
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", id);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested !!!
        public void DeleteRecipeCategory(int id)
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

                    //+2 new connection every iteration
                    GetRecipesOfCategory(id).ForEach(recipe => DeleteRecipe(recipe.Id));

                    cmd.CommandText = @"DELETE FROM RecipeCategory 
                                        WHERE Id = @categoryId
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", id);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested !!!
        public void DeleteUnit(int unitId)
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

                    DeleteLinksFromIngredientRecipe(unitId, "UnitId", connection);

                    cmd.CommandText = @"DELETE FROM Units 
                                        WHERE Id = @unitId
                                        ;";
                    cmd.Parameters.AddWithValue("@unitId", unitId);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested !!!
        public void DeleteIngredient(int ingredientId)
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

                    DeleteLinksFromIngredientRecipe(ingredientId, "IngredientId", connection);

                    cmd.CommandText = @"DELETE FROM Ingredients 
                                        WHERE Id = @ingredientId
                                        ;";
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested !!!
        public void DeleteRecipe(int recipeId)
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

                    DeleteLinksFromIngredientRecipe(recipeId, "RecipeId", connection);

                    cmd.CommandText = @"DELETE FROM Recipes 
                                        WHERE Id = @recipeId
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        //not tested
        /// <param name="id">Id of Ingredient or Recipe</param>
        /// <param name="rowName">"IngredientId" or "RecipeId"</param>
        /// <param name="openConnection"></param>
        private void DeleteLinksFromIngredientRecipe(int id, string rowName, SQLiteConnection openConnection)
        {

            using (SQLiteCommand cmd = new SQLiteCommand(openConnection))
            {
                cmd.CommandText = @"DELETE FROM IngredientRecipe 
                                    WHERE " + rowName + @" = @id
                                    ;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
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
        #endregion

        #region Get
        public List<Category> GetAllRecipeCategory()
        {
            return GetAllCategory("RecipeCategory");
        }
        public List<Category> GetAllIngredientCategory()
        {
            return GetAllCategory("IngredientCategory");
        }
        private List<Category> GetAllCategory(string table)
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
            Category result = null;
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
                        if (reader.Read())
                        {
                            result = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title"))
                            };
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public List<Unit> GetAllUnit(int id)
        {
            List<Unit> result = new List<Unit>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Id, Title FROM Units;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var unit = new Unit
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title"))
                            };
                            result.Add(unit);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public Ingredient GetIngredient(int id)
        {
            Ingredient result = null;
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
                        if (reader.Read())
                        {
                            Category ingredientCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };

                            result = new Ingredient
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("IngredientsId")),
                                Category = ingredientCategory,
                                Title = reader.GetString(reader.GetOrdinal("IngredientTitle")),
                                Calories = reader.GetInt32(reader.GetOrdinal("Kilocalories")),
                                Picture = reader["Picture"] as byte[]
                            };
                        }

                    }
                }
                connection.Close();
            }
            return result;
        }
        //not tested
        public List<Ingredient> GetIngredients(string title)
        {
            List<Ingredient> ingredients = GetAllIngredient();
            List<Ingredient> result = ingredients.Where(i => i.Title.ToUpper().Contains(title.ToUpper())).ToList();
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
                                Category = ingredientCategory,
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
                        string temp;
                        while (reader.Read())
                        {
                            temp = reader.GetString(reader.GetOrdinal("CategoryTitle"));
                            Category ingredientCategory = new Category
                            {
                                Id = categoryId,
                                Title = temp
                            };

                            var ingredient = new Ingredient
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("IngredientsId")),
                                Category = ingredientCategory,
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
        public Recipe GetRecipe(int id)
        {
            Recipe result = null;
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Recipes.Id AS RecipesId, RecipeCategory.Id AS CategoryId, 
                                               RecipeCategory.Title AS CategoryTitle, Recipes.Title AS RecipesTitle,
                                               Recipes.Description, Recipes.Time, Recipes.Picture, Recipes.Instructions  
                                        FROM Recipes INNER JOIN RecipeCategory ON Recipes.RecipeCategoryId = RecipeCategory.Id
                                        WHERE Recipes.Id = @id
                                        ;";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category recipeCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };

                            result = new Recipe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("RecipesId")),
                                Category = recipeCategory,
                                Title = reader.GetString(reader.GetOrdinal("RecipesTitle")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Picture = reader["Picture"] as byte[],
                                Instruction = reader.GetString(reader.GetOrdinal("Instructions"))
                            };
                        }

                    }
                    if(result != null)
                        result.Ingredients = GetIngredientsOfRecipe(id);
                }
                connection.Close();
            }
            return result;
        }
        //not tested
        private List<Ingredient> GetIngredientsOfRecipe(int recipeId)
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
                                               Ingredients.Kilocalories, Ingredients.Picture,
                                               IngredientRecipe.Quantity, Units.Id AS UnitId, Units.Title AS UnitTitle
                                        FROM (((Ingredients 
                                        INNER JOIN IngredientCategory ON Ingredients.IngredientCategoryId = IngredientCategory.Id)
                                        INNER JOIN IngredientRecipe ON Ingredients.Id = IngredientRecipe.IngredientId)
                                        INNER JOIN Units ON IngredientRecipe.UnitId = Units.Id)
                                        WHERE IngredientRecipe.RecipeId = @recipeId
                                        ;";
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category ingredientCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };
                            Unit unit = new Unit
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UnitId")),
                                Title = reader.GetString(reader.GetOrdinal("UnitTitle"))
                            };

                            var ingredient = new Ingredient
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("IngredientsId")),
                                Category = ingredientCategory,
                                Title = reader.GetString(reader.GetOrdinal("IngredientTitle")),
                                Calories = reader.GetInt32(reader.GetOrdinal("Kilocalories")),
                                Picture = reader["Picture"] as byte[],
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                Unit = unit
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
        public List<Recipe> GetRecipes(string title)
        {
            List<Recipe> result = GetAllRecipe();
            title = title.ToUpper();        
            return result.Where(recipe => recipe.Title.ToUpper().Contains(title)).ToList();
        }
        //not tested
        public List<Recipe> GetRecipesWithIngredient(string ingredientTitle)
        {
            List<Recipe> result = GetAllRecipe();
            ingredientTitle = ingredientTitle.ToUpper();
            return result.Where(recipe => !recipe.Ingredients
                         .All(ingr => !ingr.Title.ToUpper().Contains(ingredientTitle))).ToList();
        }
        //not tested
        public List<Recipe> GetRecipes(int time)
        {
            List<Recipe> result = new List<Recipe>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Recipes.Id AS RecipesId, RecipeCategory.Id AS CategoryId, 
                                               RecipeCategory.Title AS CategoryTitle, Recipes.Title AS RecipesTitle,
                                               Recipes.Description, Recipes.Time, Recipes.Picture, Recipes.Instructions  
                                        FROM Recipes INNER JOIN RecipeCategory ON Recipes.RecipeCategoryId = RecipeCategory.Id
                                        WHERE Recipes.Time <= @time
                                        ;";
                    cmd.Parameters.AddWithValue("@time", time);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category recipeCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };

                            Recipe temp = new Recipe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("RecipesId")),
                                Category = recipeCategory,
                                Title = reader.GetString(reader.GetOrdinal("RecipesTitle")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Picture = reader["Picture"] as byte[],
                                Instruction = reader.GetString(reader.GetOrdinal("Instructions"))
                            };
                            temp.Ingredients = GetIngredientsOfRecipe(temp.Id);
                            result.Add(temp);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        //not tested
        public List<Recipe> GetRecipesOfCategory(int categoryId)
        {
            List<Recipe> result = new List<Recipe>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Recipes.Id AS RecipesId, RecipeCategory.Id AS CategoryId, 
                                               RecipeCategory.Title AS CategoryTitle, Recipes.Title AS RecipesTitle,
                                               Recipes.Description, Recipes.Time, Recipes.Picture, Recipes.Instructions  
                                        FROM Recipes INNER JOIN RecipeCategory ON Recipes.RecipeCategoryId = RecipeCategory.Id
                                        WHERE RecipeCategory.Id = @categoryId
                                        ;";
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category recipeCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };

                            Recipe temp = new Recipe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("RecipesId")),
                                Category = recipeCategory,
                                Title = reader.GetString(reader.GetOrdinal("RecipesTitle")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Picture = reader["Picture"] as byte[],
                                Instruction = reader.GetString(reader.GetOrdinal("Instructions"))
                            };
                            temp.Ingredients = GetIngredientsOfRecipe(temp.Id);
                            result.Add(temp);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        //not tested
        public List<Recipe> GetAllRecipe()
        {
            List<Recipe> result = new List<Recipe>();
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"SELECT Recipes.Id AS RecipesId, RecipeCategory.Id AS CategoryId, 
                                               RecipeCategory.Title AS CategoryTitle, Recipes.Title AS RecipesTitle,
                                               Recipes.Description, Recipes.Time, Recipes.Picture, Recipes.Instructions  
                                        FROM Recipes INNER JOIN RecipeCategory ON Recipes.RecipeCategoryId = RecipeCategory.Id
                                        ;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category recipeCategory = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Title = reader.GetString(reader.GetOrdinal("CategoryTitle"))
                            };

                            Recipe temp = new Recipe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("RecipesId")),
                                Category = recipeCategory,
                                Title = reader.GetString(reader.GetOrdinal("RecipesTitle")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Picture = reader["Picture"] as byte[],
                                Instruction = reader.GetString(reader.GetOrdinal("Instructions"))
                            };
                            temp.Ingredients = GetIngredientsOfRecipe(temp.Id);
                            result.Add(temp);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        public List<Recipe> GetRecipesFromIngredients(List<Ingredient> ingredients)
        {
            List<Recipe> result = GetAllRecipe();
            //рецепты у которых список ингредиентов является подмножеством списка имеющихся ингредиентов
            return result.Where(recipe => recipe.Ingredients.All(recipeIngr => ingredients.Contains(recipeIngr))).ToList();
        }
        #endregion
    
    }
}