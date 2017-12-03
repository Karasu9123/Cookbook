using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cookbook.DataModel;
using Cookbook.DataAccess;

namespace Cookbook.GUI
{
    public partial class Details : Form
    {
        List<int> recipesId = new List<int>();
        IDataRepository db;

        private class ButtonRecipe : Button
        {
            public int RecipeId { get; private set; }

            public ButtonRecipe(int recipeId) : base()
            {
                RecipeId = recipeId;
            }
        }



        public Details(IDataRepository db)
        {
            InitializeComponent();
            this.db = db;
        }

        public void Add(int recipeId)
        {
            var recipe = db.GetRecipe(recipeId);

            if (!recipesId.Contains(recipeId))
            {
                var buttonRecipe = new ButtonRecipe(recipeId);
                buttonRecipe.Location = new Point();
                buttonRecipe.Size = new Size();
                buttonRecipe.FlatStyle = FlatStyle.Flat;
                buttonRecipe.Text = recipe.Title;

                var buttonExit = new ButtonRecipe(recipeId);
                buttonExit.Location = new Point();
                buttonExit.Size = new Size();
                buttonExit.FlatStyle = FlatStyle.Flat;
                buttonExit.Text = "X";

                panelRecipes.Controls.Add(buttonRecipe);
                panelRecipes.Controls.Add(buttonExit);
            }

            Display(recipe);
        }

        public void Delete(Recipe recipe)
        {

        }

        private void ButtonRecipe_Click(object sender, EventArgs e)
        {
            var recipe = db.GetRecipe((sender as ButtonRecipe).RecipeId);
            Display(recipe);
        }

        private void Display(Recipe recipe)
        {

        }

    }
}
