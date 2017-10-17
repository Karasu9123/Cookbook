using Cookbook.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cookbook.DataAccess
{
    interface IDataRepository
    {
        void CreateDataBase(string dbName);

        void AddIngredientCategory(string title);
        void AddRecipeCategory(string title);

        List<Category> GetAllRecipeCategory();
        List<Category> GetAllIngredientCategory();
    }
}
