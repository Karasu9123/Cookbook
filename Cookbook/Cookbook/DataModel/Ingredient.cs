using System;
using System.Drawing;

namespace Cookbook.DataModel
{
    public class Ingredient : IEquatable<Ingredient>
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public int Calories { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }//Единицы измерения
        public byte[] Picture { get; set; }

        public Image GetImage()
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(Picture));
        }

        public bool Equals(Ingredient otherIngredient)
        {
            if (otherIngredient == null)
                return false;
            return Id == otherIngredient.Id;
        }

        public override bool Equals(object other)
        {
            if (!(other is Ingredient))
                return false;
            return Equals(other as Ingredient);
        }
    }
}
