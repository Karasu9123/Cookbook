using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class RecipeImage //H
    {
        public int Id { get; set; }
        public int NumberOfImage { get; set; }
        public byte[] Image { get; set; }
    }
}
