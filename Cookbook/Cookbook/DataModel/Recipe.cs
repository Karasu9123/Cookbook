﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataModel
{
    public class Recipe //H
    {
        public int Id { get; set; }
        public RecipeCategory Category { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public int Time { get; set; }//Time in minutes
        public List<Ingredient> Ingredients { get; set; }

    }
}