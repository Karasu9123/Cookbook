using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class RecipeImage //H
    {
        public int Id { get; set; }
        public int NumberOfPicture { get; set; }
        public byte[] Picture { get; set; }

        public Image GetImage()
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(Picture));
        }
    }
}
