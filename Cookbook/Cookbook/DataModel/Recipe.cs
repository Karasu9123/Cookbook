using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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

        public static Image DefaultImage { get; set; }

        public Image GetImage()
        {
            return Picture == null ? DefaultImage : (Bitmap)((new ImageConverter()).ConvertFrom(Picture));
        }

        public string ImageToBase64()
        {
            if (Picture != null)
                return Convert.ToBase64String(Picture);

            var ms = new MemoryStream();
            DefaultImage.Save(ms, ImageFormat.Bmp);
            return Convert.ToBase64String(ms.GetBuffer());
        }
    }
}
