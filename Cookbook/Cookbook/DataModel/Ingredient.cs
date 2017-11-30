using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class Ingredient
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public int Calories { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }//Единицы измерения
        public byte[] Picture { get; set; }

        public Image GetImage()
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(Picture));
        }
    }
}
