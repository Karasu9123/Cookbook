using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class Ingredient //H
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public int Calories { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }//Единицы измерения
        /// <summary>
        /// Picture in Base64 string
        /// </summary>
        public byte[] Picture { get; set; }

        public Image GetImage()
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(Picture));
        }
    }
}
