using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Cookbook.DataModel
{
    public class Recipe
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Time in minutes
        /// </summary>
        public int Time { get; set; }
        public byte[] Picture { get; set; }
        public string Instruction { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Image GetImage()
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(Picture));
        }
    }
}
