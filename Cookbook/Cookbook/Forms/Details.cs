using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Cookbook.DataModel;
using Cookbook.DataAccess;

namespace Cookbook.GUI
{
    public partial class Details : Form
    {
        private List<Record> records = new List<Record>();
        private IDataRepository db;
        private int currentId;

        private class Record
        {
            public ButtonRecipe Recipe { get; set; }
            public Button Exit { get; set; }
        }

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
            
            tableLayoutPanel.ColumnStyles[1].Width = 30;
            tableLayoutPanel.ColumnStyles[0].Width = tableLayoutPanel.Width - tableLayoutPanel.ColumnStyles[1].Width;
        }


        public void Add(int recipeId)
        {
            var recipe = db.GetRecipe(recipeId);

            if (!records.Any(i => i.Recipe.RecipeId == recipeId))
            {
                var buttonRecipe = new ButtonRecipe(recipeId);
                var buttonExit = new Button();

                buttonRecipe.FlatStyle = FlatStyle.Popup;
                buttonRecipe.Dock = DockStyle.Fill;
                buttonRecipe.AutoEllipsis = true;
                buttonRecipe.Text = recipe.Title;
                buttonRecipe.Click += ButtonRecipe_Click;

                buttonExit.BackColor = Color.Tomato;
                buttonExit.FlatStyle = FlatStyle.Popup;
                buttonExit.Dock = DockStyle.Fill;
                buttonExit.Text = "X";
                buttonExit.Click += ButtonExit_Click;

                tableLayoutPanel.Controls.Add(buttonRecipe);
                tableLayoutPanel.Controls.Add(buttonExit);

                records.Add(new Record() { Recipe = buttonRecipe, Exit = buttonExit });
            }

            currentId = recipeId;
            Display(recipe);
        }


        private void ButtonExit_Click(object sender, EventArgs e)
        {
            var record = records.Single(i => i.Exit == (sender as Button));
            ClearRecord(record);
            records.Remove(record);
        }


        private void ClearRecord(Record record)
        {
            if (!records.Contains(record))
                return;

            if (record.Recipe.RecipeId == currentId)
                webBrowser.DocumentText = "";

            record.Recipe.Dispose();
            record.Exit.Dispose();
        }


        private void ButtonRecipe_Click(object sender, EventArgs e)
        {
            if (currentId == (sender as ButtonRecipe).RecipeId)
                return;

            currentId = (sender as ButtonRecipe).RecipeId;
            var recipe = db.GetRecipe(currentId);

            Display(recipe);
        }


        private void Display(Recipe recipe)
        {
            string ingredients = "<ul>";
            foreach (var ingredient in recipe.Ingredients)
                ingredients += "<li>" + ingredient.Title + " — <b>" + ingredient.Quantity + " " + ingredient.Unit.Title + "</b>" + "</li>";
            ingredients += "</ul>";

            webBrowser.DocumentText = @"<!DOCTYPE html>
                                        <html>
                                          <head>
                                            <meta charset=""UTF - 8"">
                                            <style>
                                              .photocard {
                                                display: block;
                                                width: 500px;
                                                margin: 20px auto;
                                                border-radius: 10px;
                                                box-shadow: 0 0 5px #666;
                                              }
                                            </style> 
                                          </head>

                                            <body>
                                                <h1 align=""center"">" + recipe.Title + @"</h1>

                                                <div> <strong> Категория: </strong> "
                                                    + recipe.Category.Title + @".</div>

                                                <div><p>
                                                    <img class = photocard src = ""data:image/bmp;base64," + recipe.ImageToBase64() 
                                                    + @" ""</p></div>

                                                <p>"
                                                    + recipe.Description + @".</p>

                                                <h3> Ингредиенты: </h3>"
                                                + ingredients +

                                                @"<h3 align=""center""> Инструкция приготовления: </h3>
                                                <div>" + recipe.Instruction + @"</div>
                                            </body>
     

                                        </html> ";
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            records.ForEach(i => ClearRecord(i));
            records.Clear();
            webBrowser.DocumentText = "";
        }
        
    }
}
