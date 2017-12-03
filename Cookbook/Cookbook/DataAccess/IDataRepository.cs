using Cookbook.DataModel;
using System.Collections.Generic;

namespace Cookbook.DataAccess
{
    public interface IDataRepository
    {
        void CreateDataBase();

        #region Add
        void AddIngredientCategory(string title);//+
        void AddRecipeCategory(string title);//+
        void AddIngredient(int ingredientCategoryId, string title, int kilocalories, byte[] picture);//+
        void AddRecipe(int recipeCategoryId, string title, string description, string instructions, int time, byte[] picture);//+ not tested
        void AddRecipeWithExistingIngredients(int recipeCategoryId, string title, string description, string instructions,
            int time, byte[] picture, List<Ingredient> ingredients);//+ not tested
        void AddIngredientToRecipe(int recipeId, int ingredientId, int quantity, string units);//+ not tested
        #endregion

        #region Update
        void UpdateIngredientCategory(int id, string title);
        void UpdateRecipeCategory(int id, string title);
        /// <summary>
        /// Update row categoryId in Ingredients table
        /// </summary>
        void UpdateCategoryOnIngredient(int ingredientId, int ingredientCategoryId);
        void UpdateIngredientTitle(int ingredientId, string title);
        void UpdateIngredientKilocalories(int ingredientId, int kilocalories);
        void UpdateIngredientPicture(int ingredientId, byte[] picture);
        /// <summary>
        /// Update row categoryId in Recipes table
        /// </summary>
        void UpdateCategoryOnRecipe(int recipeId, int recipeCategoryId);
        void UpdateRecipeTitle(int recipeId, string title);
        void UpdateRecipeDescription(int recipeId, string description);
        void UpdateRecipeInstructions(int recipeId, string instructions);
        void UpdateRecipeTime(int recipeId, int time);
        void UpdateRecipePicture(int recipeId, byte[] picture);
        void UpdateIngredientQuantityOnRecipe(int recipeId, int ingredientId, int quantity);
        void UpdateIngredientUnitsOnRecipe(int recipeId, int ingredientId, string units);
        
        #endregion

        #region Delete
        void DeleteIngredientCategory(int id);
        void DeleteRecipeCategory(int id);
        void DeleteIngredient(int id);
        void DeleteRecipe(int id);
        void DeleteIngredientFromRecipe(int recipeId, int ingredientId);
        #endregion

        #region Get
        List<Category> GetAllRecipeCategory();
        List<Category> GetAllIngredientCategory();
        Category GetRecipeCategory(int id);
        Category GetIngredientCategory(int id);
        Ingredient GetIngredient(int id);
        /// <summary>
        /// Returns ingredients that have an argument in the title
        /// </summary>
        /// <param name="title">Part of title</param>
        /// <returns></returns>
        List<Ingredient> GetIngredients(string title);
        List<Ingredient> GetIngredientsOfCategory(int categoryId);
        List<Ingredient> GetAllIngredient();
        Recipe GetRecipe(int id);
        /// <summary>
        /// Returns recipes that have an argument in the title
        /// </summary>
        /// <param name="title">Part of title</param>
        /// <returns></returns>
        List<Recipe> GetRecipes(string title);
        /// <summary>
        /// Returns recipes in which the time is less than the argument
        /// /// </summary>
        /// <param name="time">Max cooking time</param>
        /// <returns></returns>
        List<Recipe> GetRecipes(int time);
        List<Recipe> GetRecipesOfCategory(int categoryId);
        List<Recipe> GetAllRecipe();
        List<Recipe> GetRecipesFromIngredients(List<Ingredient> ingredients);
        #endregion
    }
}