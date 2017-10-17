using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class Ingredient //H
    {
        public int Id { get; set; }
        public Category IngredientCategory { get; set; }
        public string Title { get; set; }
        public int Calories { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }//Единицы измерения
        public byte[] Image { get; set; }
    }
}
