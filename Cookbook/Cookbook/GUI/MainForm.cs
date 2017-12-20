using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Cookbook.DataModel;
using Cookbook.DataAccess;
using System.IO;
using System.Drawing;

namespace Cookbook.GUI
{
    public partial class MainForm : Form
    {
        //string dbName = "Cookbook.db3";
        string testDB = "TestDB.db3";
        string dbPath = Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)) + @"\";
        IDataRepository db;
        Details details = null;

        public MainForm()
        {
            InitializeComponent();
            //db = new DataRepository(dbPath + dbName);
            db = new DataRepository(dbPath + testDB);

            #region Test data
            db.CreateDataBase();
            db.AddRecipeCategory("Жареные блюда");
            db.AddRecipeCategory("Вегетарианские блюда");
            db.AddRecipeCategory("Мясные блюда");
            db.AddRecipeCategory("Фастфуд");

            db.AddIngredientCategory("Алкоголь");
            db.AddIngredientCategory("Фастфуд");
            db.AddIngredientCategory("Овощи");
            db.AddIngredientCategory("Мясо");
            db.AddIngredientCategory("Молочные продукты");

            Random r = new Random();
            
            db.AddIngredient(1, "Вино", 83, null);
            db.AddIngredient(2, "Мивина", 350, null);
            db.AddIngredient(3, "Огурцы", 15, null);
            db.AddIngredient(3, "Петрушка", 23, null);
            db.AddIngredient(4, "Курица", r.Next(135, 210), null);
            db.AddIngredient(5, "Сыр", r.Next(268, 380), null);
            db.AddRecipe(4, "Мивина с петрушкой", "Просто мивина", "Залей мивину кипятком. Порежь петрушку.", 10, null);
            db.AddUnit("Штук");
            db.AddUnit("Веточек");
            db.AddIngredientToRecipe(1, 2, 1, 1);
            db.AddIngredientToRecipe(1, 4, 2, 2);

            Recipe recipe = db.GetRecipe(1);
            var ingredients = new List<Ingredient>() { db.GetIngredient(1), db.GetIngredient(2), db.GetIngredient(3),
                                                       db.GetIngredient(4), db.GetIngredient(5), db.GetIngredient(6)};
            var recipes = db.GetRecipesFromIngredients(ingredients);
            #endregion

            listCategoriesOfIngredient.DataSource = db.GetAllIngredientCategory();
            SetRecipeCategories(db.GetAllRecipeCategory());

            fridge.DisplayMember = "Title";
            listIngredients.DisplayMember = "Title";
            listCategoriesOfIngredient.DisplayMember = "Title";

            Recipe.DefaultImage = new Bitmap(Properties.Resources.DefaultImage);
        }


        /// <summary>
        /// Set list of recipe categories on the tableCategoriesPanel.
        /// </summary>
        /// <param name="categories"></param>
        private void SetRecipeCategories(List<Category> categories)
        {
            foreach (var category in categories)
            {
                var label = new LinkLabel();

                label.AutoEllipsis = true;
                label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                label.Text = category.Title;
                label.Links.Add(0, label.Text.Length, category.Id);
                label.LinkClicked += LinkRecipeCategory_Click;

                tableCategoriesPanel.Controls.Add(label);
            }
        }



        /// <summary>
        /// Draw list of recipes on the tabPageFind.
        /// </summary>
        /// <param name="recipes"></param>
        private void DrawRecipes(List<Recipe> recipes)
        {
            tabPageFind.Controls.Clear();

            if (recipes == null || recipes.Count == 0)
            {
                ///Draw "Not Found".
                var labelNotFound = new Label();
                labelNotFound.Text = "Recipes are not found";
                labelNotFound.Location = new Point(5, 5);
                labelNotFound.AutoSize = true;
                tabPageFind.Controls.Add(labelNotFound);
            }
            else
            {
                ///Draw elements.
                int i = 0;
                int recordSize = 210;

                foreach (var recipe in recipes)
                {
                    var img = new PictureBox();
                    var title = new Label();
                    var description = new Label();
                    var ingredients = new Label();
                    var details = new LinkLabel();

                    img.Location = new Point(15, 15 + i * recordSize);
                    img.Size = new Size(185, 185);
                    img.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    img.SizeMode = PictureBoxSizeMode.Zoom;
                    img.BorderStyle = BorderStyle.None;
                    img.TabStop = false;
                    img.Image = recipe.GetImage();

                    title.Location = new Point(225, 15 + i * recordSize);
                    title.Size = new Size(tabPageFind.Width - 300, 25);
                    title.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    title.AutoEllipsis = true;
                    title.RightToLeft = RightToLeft.No;
                    title.Font = new Font("Segoe Print", 12F, FontStyle.Regular, GraphicsUnit.Point, (byte)(204));
                    title.Text = recipe.Title;

                    description.Location = new Point(210, 45 + i * recordSize);
                    description.Size = new Size(tabPageFind.Width - 230, 85);
                    description.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    description.AutoEllipsis = true;
                    description.RightToLeft = RightToLeft.No;
                    description.Font = new Font("Times New Roman", 11.25F, FontStyle.Regular, GraphicsUnit.Point, (byte)(204));
                    description.Text = recipe.Description;

                    ingredients.Location = new Point(210, 140 + i * recordSize);
                    ingredients.Size = new Size(tabPageFind.Width - 230, 60);
                    ingredients.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    ingredients.AutoEllipsis = true;
                    description.RightToLeft = RightToLeft.No;
                    ingredients.Font = new Font("Sitka Small", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)(204));
                    ingredients.ForeColor = Color.DarkSlateBlue;
                    foreach (var ingredient in recipe.Ingredients)
                        ingredients.Text += ingredient.Title + "        ";

                    details.Location = new Point(tabPageFind.Width - 60, 15 + i * recordSize);
                    details.Size = new Size(40, 13);
                    details.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    details.AutoSize = true;
                    details.Text = "Details";
                    details.Links.Add(0, details.Text.Length, recipe.Id);
                    details.LinkClicked += DetailsLink_Clicked;


                    tabPageFind.Controls.Add(img);
                    tabPageFind.Controls.Add(title);
                    tabPageFind.Controls.Add(description);
                    tabPageFind.Controls.Add(ingredients);
                    tabPageFind.Controls.Add(details);

                    ++i; //НЕ ТРОГАТЬ!!!
                }
            }

            tabControl.SelectedTab = tabPageFind;
        }



        /// <summary>
        /// Open Details form and add recipe to it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {    
            if (e.Button == MouseButtons.Left)
            {
                if (details == null || details.IsDisposed)
                    details = new Details(db);

                details.Add((int)e.Link.LinkData);
                details.Show();
            }
        }



        /// <summary>
        /// Read all recipes of the clicked category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkRecipeCategory_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var recipes = db.GetRecipesOfCategory((int)e.Link.LinkData);
                DrawRecipes(recipes);
            }
        }



        /// <summary>
        /// Add all ingredients from selectedCategory to listIngredients. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listIngredientCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCategoriesOfIngredient.SelectedItem == null)
                return;

            listIngredients.Items.Clear();
            var ingredients = db.GetIngredientsOfCategory((listCategoriesOfIngredient.SelectedItem as Category).Id);

            foreach (var ingredient in ingredients)
            {
                bool inFridge = fridge.Items.Contains(ingredient);
                listIngredients.Items.Add(ingredient, inFridge);
            }

            listIngredients.Refresh();
        }



        /// <summary>
        /// Add/Remove selectedIngredient to/from fridge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listIngredients_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var selectedIngredient = listIngredients.Items[e.Index] as Ingredient;

            if (e.NewValue == CheckState.Checked)
            {
                if (!fridge.Items.Contains(selectedIngredient))
                    fridge.Items.Add(selectedIngredient);

                return;
            }


            if (e.NewValue == CheckState.Unchecked)
            {
                int index = fridge.Items.IndexOf(selectedIngredient);
                if (index != -1)
                    fridge.Items.RemoveAt(index);
            }
        }



        /// <summary>
        /// Remove clicked item from fridge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFridge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = fridge.IndexFromPoint(e.Location);
            if (index == -1)
                return;

            var selectedIngredient = fridge.Items[index] as Ingredient;

            //Uncheck in listIngredient.
            int checkedIndex = listIngredients.Items.IndexOf(selectedIngredient);
            if (checkedIndex != -1)
                listIngredients.SetItemCheckState(checkedIndex, CheckState.Unchecked);

            //Delete from fridge.
            fridge.Items.Remove(selectedIngredient);
            fridge.Refresh();
        }



        /// <summary>
        /// Clear listIngredient and fridge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            fridge.Items.Clear();
            listIngredients.Items.Clear();
        }



        /// <summary>
        /// Search recipes with selected parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFind_Click(object sender, EventArgs e)
        {
            List<Recipe> recipes = null;

            if (fridge.Items.Count != 0)
            {
                var ingredients = new List<Ingredient>();

                foreach (Ingredient item in fridge.Items)
                    ingredients.Add(item);

                recipes = db.GetRecipesFromIngredients(ingredients);
            }

            DrawRecipes(recipes);
        }



        /// <summary>
        /// Search recipes with textBox.Text in title.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            var recipes = textBoxSearch.Text != "" ? db.GetRecipes(textBoxSearch.Text) : null;
            DrawRecipes(recipes);
        }

    }
}