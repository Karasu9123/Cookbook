using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class Recipe //H
    {
        public int Id { get; set; }
        public Category RecipeCategory { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        /// <summary>
        /// Time in minutes
        /// </summary>
        public int Time { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<RecipeImage> Pictures { get; set; }

    }
}
