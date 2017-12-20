using Cookbook.DataModel;
using System.Collections.Generic;

namespace Cookbook.DataAccess
{
    public interface IDataRepository
    {
        void CreateDataBase();

        #region Add
        void AddIngredientCategory(string title);
        void AddRecipeCategory(string title);
        void AddUnit(string title);
        void AddIngredient(int ingredientCategoryId, string title, int kilocalories, byte[] picture);
        void AddRecipe(int recipeCategoryId, string title, string description, string instructions, int time, byte[] picture);
        void AddRecipeWithExistingIngredients(int recipeCategoryId, string title, string description, string instructions,
            int time, byte[] picture, List<Ingredient> ingredients);
        void AddIngredientToRecipe(int recipeId, int ingredientId, int quantity, int unitId);
        #endregion

        #region Update
        void UpdateIngredientCategory(int id, string title);
        void UpdateRecipeCategory(int id, string title);
        void UpdateUnit(Unit unit);
        void UpdateIngredient(Ingredient ingredient);
        void UpdateRecipe(Recipe recipe);
        void UpdateIngredientRecipe(int recipeId, Ingredient ingredient);
        #endregion

        #region Delete
        void DeleteIngredientCategory(int id);
        void DeleteRecipeCategory(int id);
        void DeleteUnit(int id);
        void DeleteIngredient(int id);
        void DeleteRecipe(int id);
        void DeleteIngredientFromRecipe(int recipeId, int ingredientId);
        #endregion

        #region Get
        List<Category> GetAllRecipeCategory();
        List<Category> GetAllIngredientCategory();
        Category GetRecipeCategory(int id);
        Category GetIngredientCategory(int id);
        List<Unit> GetAllUnit(int id);
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
        /// Returns recipes which contains ingredient 
        /// </summary>
        /// <param name="ingredientTitle"></param>
        /// <returns></returns>
        List<Recipe> GetRecipesWithIngredient(string ingredientTitle);
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