using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cookbook.DataModel;
using Cookbook.DataAccess;
using System.IO;

namespace Cookbook.GUI
{
    public partial class DataManager : Form
    {
        private enum CurrentModels { Recipes, Ingredients, RCategories, ICategories, Units }
        private enum Action { Create, Details, Edit, Delete }
        
        CurrentModels curModels;
        string curFilter;
        Action curAction;
        bool start;

        IDataRepository db;

        public DataManager(IDataRepository dataBase)
        {
            InitializeComponent();
            DrawStart();

            db = dataBase;

            Recipe.DefaultImage = Properties.Resources.DefaultImage;
        }


        #region Start

        private void DrawStart()
        {
            Controls.Clear();
            WindowState = FormWindowState.Normal;
            start = true;

            Button buttonRecipes;
            Button buttonIngredients;
            Button buttonRCategories;
            Button buttonICategories;
            Button buttonUnits;

            buttonRecipes = new Button();
            buttonIngredients = new Button();
            buttonRCategories = new Button();
            buttonICategories = new Button();
            buttonUnits = new Button();
            SuspendLayout();
            // 
            // buttonRecipes
            // 
            buttonRecipes.BackColor = Color.LightCyan;
            buttonRecipes.FlatStyle = FlatStyle.Flat;
            buttonRecipes.Font = new Font("Segoe Print", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonRecipes.ForeColor = Color.Black;
            buttonRecipes.Location = new Point(136, 48);
            buttonRecipes.Name = "buttonRecipes";
            buttonRecipes.Size = new Size(331, 77);
            buttonRecipes.TabIndex = 0;
            buttonRecipes.Text = "Рецепты";
            buttonRecipes.UseVisualStyleBackColor = false;
            buttonRecipes.Click += ButtonRecipes_Click;
            // 
            // buttonIngredients
            // 
            buttonIngredients.BackColor = Color.LightCyan;
            buttonIngredients.FlatStyle = FlatStyle.Flat;
            buttonIngredients.Font = new Font("Segoe Print", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonIngredients.ForeColor = Color.Black;
            buttonIngredients.Location = new Point(136, 140);
            buttonIngredients.Name = "buttonIngredients";
            buttonIngredients.Size = new Size(331, 77);
            buttonIngredients.TabIndex = 1;
            buttonIngredients.Text = "Ингредиенты";
            buttonIngredients.UseVisualStyleBackColor = false;
            buttonIngredients.Click += ButtonIngredients_Click;
            // 
            // buttonRCategories
            // 
            buttonRCategories.BackColor = Color.LightCyan;
            buttonRCategories.FlatStyle = FlatStyle.Flat;
            buttonRCategories.Font = new Font("Segoe Print", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonRCategories.ForeColor = Color.Black;
            buttonRCategories.Location = new Point(136, 233);
            buttonRCategories.Name = "buttonRCategories";
            buttonRCategories.Size = new Size(331, 77);
            buttonRCategories.TabIndex = 2;
            buttonRCategories.Text = "Категории рецептов";
            buttonRCategories.UseVisualStyleBackColor = false;
            buttonRCategories.Click += ButtonRCategories_Click;
            // 
            // buttonICategories
            // 
            buttonICategories.BackColor = Color.LightCyan;
            buttonICategories.FlatStyle = FlatStyle.Flat;
            buttonICategories.Font = new Font("Segoe Print", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonICategories.ForeColor = Color.Black;
            buttonICategories.Location = new Point(136, 327);
            buttonICategories.Name = "buttonICategories";
            buttonICategories.Size = new Size(331, 77);
            buttonICategories.TabIndex = 3;
            buttonICategories.Text = "Категории ингредиентов";
            buttonICategories.UseVisualStyleBackColor = false;
            buttonICategories.Click += ButtonICategories_Click;
            // 
            // buttonUnits
            // 
            buttonUnits.BackColor = Color.LightCyan;
            buttonUnits.FlatStyle = FlatStyle.Flat;
            buttonUnits.Font = new Font("Segoe Print", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonUnits.ForeColor = Color.Black;
            buttonUnits.Location = new Point(136, 419);
            buttonUnits.Name = "buttonUnits";
            buttonUnits.Size = new Size(331, 77);
            buttonUnits.TabIndex = 4;
            buttonUnits.Text = "Еденицы измерения";
            buttonUnits.UseVisualStyleBackColor = false;
            buttonUnits.Click += ButtonUnits_Click;
            // 
            // DataManager
            // 
            MinimumSize = new Size(0, 0);
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackgroundImage = Properties.Resources.StartManager;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(592, 570);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(buttonUnits);
            Controls.Add(buttonICategories);
            Controls.Add(buttonRCategories);
            Controls.Add(buttonIngredients);
            Controls.Add(buttonRecipes);
            ResumeLayout(false);
        }


        private void ButtonRecipes_Click(object sender, EventArgs e)
        {
            curModels = CurrentModels.Recipes;
            ViewMainPage(db.GetAllRecipe(), db.GetAllRecipeCategory());
        }


        private void ButtonIngredients_Click(object sender, EventArgs e)
        {
            curModels = CurrentModels.Ingredients;
            ViewMainPage(db.GetAllIngredient(), db.GetAllIngredientCategory());
        }


        private void ButtonRCategories_Click(object sender, EventArgs e)
        {
            curModels = CurrentModels.RCategories;
            ViewMainPage(db.GetAllRecipeCategory());
        }


        private void ButtonICategories_Click(object sender, EventArgs e)
        {
            curModels = CurrentModels.ICategories;
            ViewMainPage(db.GetAllIngredientCategory());
        }


        private void ButtonUnits_Click(object sender, EventArgs e)
        {
            curModels = CurrentModels.Units;
            ViewMainPage(db.GetAllUnit());
        }

        #endregion
        
        #region Pages

        private void Template()
        {
            var buttonHome = new Button();
            var labelNavigate = new Label();
            var topPanel = new Panel();
            var mainTableLayoutPanel = new TableLayoutPanel();
            //
            // topPanel
            //
            topPanel.BackColor = Color.MintCream;
            topPanel.BorderStyle = BorderStyle.Fixed3D;
            topPanel.Height = 42;
            topPanel.Dock = DockStyle.Top;
            Controls.Add(labelNavigate);
            Controls.Add(buttonHome);
            // 
            // buttonHome
            // 
            buttonHome.BackColor = Color.AliceBlue;
            buttonHome.FlatStyle = FlatStyle.Flat;
            buttonHome.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonHome.Location = new Point(1, 1);
            buttonHome.Size = new Size(170, 40);
            buttonHome.Text = "На главную";
            buttonHome.UseVisualStyleBackColor = false;
            buttonHome.Click += ButtonHome_Click;
            // 
            // labelNavigate
            // 
            labelNavigate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelNavigate.BackColor = Color.WhiteSmoke;
            labelNavigate.BorderStyle = BorderStyle.FixedSingle;
            labelNavigate.FlatStyle = FlatStyle.Flat;
            labelNavigate.Font = new Font("Comic Sans MS", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNavigate.Location = new Point(200, 9);
            labelNavigate.Size = new Size(Width - labelNavigate.Left - 30, 26);
            labelNavigate.Name = "labelNavigate";
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.AutoScroll = true;
            mainTableLayoutPanel.BackColor = Color.LightGoldenrodYellow;
            mainTableLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            // 
            // DataManager
            // 
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            BackgroundImage = null;
            MinimumSize = new Size(470, 270);
            if (start)
            {
                ClientSize = new Size(751, 468);
                start = false;
            }
            Controls.Add(mainTableLayoutPanel);
            Controls.Add(topPanel);
        }

        private void ButtonHome_Click(object sender, EventArgs e)
        {
            DrawStart();
        }

        private void ChangeNavigate(bool withAction = false)
        {
            var labelNavigate = Controls.Find("labelNavigate", true)[0] as Label;

            switch (curModels)
            {
                case CurrentModels.Recipes:
                    labelNavigate.Text = "Рецепты";
                    break;
                case CurrentModels.Ingredients:
                    labelNavigate.Text = "Ингредиенты";
                    break;
                case CurrentModels.RCategories:
                    labelNavigate.Text = "Категории рецептов";
                    break;
                case CurrentModels.ICategories:
                    labelNavigate.Text = "Категории ингредиентов";
                    break;
                case CurrentModels.Units:
                    labelNavigate.Text = "Единицы измерения";
                    break;
            }
            labelNavigate.Text += "/" + curFilter;

            if (withAction)
            {
                labelNavigate.Text += "/";
                switch (curAction)
                {
                    case Action.Create:
                        labelNavigate.Text += "Создать";
                        break;
                    case Action.Details:
                        labelNavigate.Text += "Детали";
                        break;
                    case Action.Edit:
                        labelNavigate.Text += "Изменить";
                        break;
                    case Action.Delete:
                        labelNavigate.Text += "Удалить";
                        break;
                }
            }
        }


        #region MainPage

        private void ViewMainPage(IEnumerable<dynamic> list, List<Category> filters = null, string filter = "Все")
        {
            Controls.Clear();
            Template();

            var mainFlowLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var mainPagePanel = new Panel();
            var tableModels = new TableLayoutPanel();
            var labelCreate = new LinkLabel();
            // 
            // mainPagePanel
            // 
            mainPagePanel.Dock = DockStyle.Fill;
            mainPagePanel.Name = "mainPagePanel";
            mainFlowLayoutPanel.Controls.Add(mainPagePanel);
            // 
            // labelCreate
            // 
            labelCreate.AutoSize = true;
            labelCreate.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelCreate.FlatStyle = FlatStyle.Popup;
            labelCreate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCreate.Location = new Point(23, 13);
            labelCreate.TabStop = true;
            labelCreate.Text = "Создать";
            labelCreate.Links[0].LinkData = new { id = -1, action = Action.Create };
            labelCreate.LinkClicked += LinkModelView_Click;
            mainPagePanel.Controls.Add(labelCreate);
            // 
            // tableModels
            // 
            tableModels.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tableModels.AutoSize = true;
            tableModels.ColumnCount = 4;
            tableModels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableModels.ColumnStyles.Add(new ColumnStyle());
            tableModels.ColumnStyles.Add(new ColumnStyle());
            tableModels.ColumnStyles.Add(new ColumnStyle());
            tableModels.Location = new Point(25, 70);
            tableModels.Width = mainPagePanel.Width - tableModels.Left * 2;
            tableModels.Padding = new Padding(0, 0, 0, 5);
            tableModels.Name = "tableModels";
            mainPagePanel.Controls.Add(tableModels);
            DrawTable(list);
            //
            // comboBoxFilters
            //
            if (filters != null)
            {
                var comboBoxFilters = new ComboBox();

                comboBoxFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                comboBoxFilters.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxFilters.Font = new Font("Times New Roman", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
                comboBoxFilters.FormattingEnabled = true;
                comboBoxFilters.Size = new Size(187, 25);
                comboBoxFilters.Location = new Point(mainPagePanel.Width - comboBoxFilters.Width - 20, 13);
                comboBoxFilters.SelectedIndexChanged += FilterChanged;

                filters.Insert(0, new Category { Id = -1, Title = "Все" });
                comboBoxFilters.DataSource = filters;
                comboBoxFilters.DisplayMember = "Title";

                mainPagePanel.Controls.Add(comboBoxFilters);
            }

            curFilter = filter;
            ChangeNavigate();
        }

        private void DrawTable(IEnumerable<dynamic> list)
        {
            var table = Controls.Find("tableModels", true)[0] as TableLayoutPanel;
            table.Controls.Clear();

            foreach (var item in list)
            {
                var labelTitle = new Label();
                var labelDelete = new LinkLabel();
                var labelDetails = new LinkLabel();
                var labelEdit = new LinkLabel();
                // 
                // labelTitle
                // 
                labelTitle.AutoEllipsis = true;
                labelTitle.AutoSize = true;
                labelTitle.BackColor = Color.Transparent;
                labelTitle.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelTitle.Location = new Point(3, 0);
                labelTitle.Text = item.Title;
                // 
                // labelDetails
                // 
                labelDetails.AutoSize = true;
                labelDetails.BackColor = Color.Transparent;
                labelDetails.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelDetails.TabStop = true;
                labelDetails.Text = "Детали";
                labelDetails.Links[0].LinkData = new { id = item.Id, action = Action.Details };
                labelDetails.LinkClicked += LinkModelView_Click;
                // 
                // labelEdit
                // 
                labelEdit.AutoSize = true;
                labelEdit.BackColor = Color.Transparent;
                labelEdit.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelEdit.TabStop = true;
                labelEdit.Text = "Изменить";
                labelEdit.Links[0].LinkData = new { id = item.Id, action = Action.Edit };
                labelEdit.LinkClicked += LinkModelView_Click;
                // 
                // labelDelete
                // 
                labelDelete.AutoSize = true;
                labelDelete.BackColor = Color.Transparent;
                labelDelete.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelDelete.TabStop = true;
                labelDelete.Text = "Удалить";
                labelDelete.Links[0].LinkData = new { id = item.Id, action = Action.Delete };
                labelDelete.LinkClicked += LinkModelView_Click;
                //
                // Add
                //
                table.Controls.Add(labelTitle);
                table.Controls.Add(labelDetails);
                table.Controls.Add(labelEdit);
                table.Controls.Add(labelDelete);
            }
        }

        private void LinkModelView_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dynamic args = e.Link.LinkData;
            curAction = (Action)args.action;

            switch (curModels)
            {
                case CurrentModels.Recipes:
                    ViewRecipe((int)args.id);
                    break;
                case CurrentModels.Ingredients:
                    ViewIngredient((int)args.id);
                    break;
                case CurrentModels.RCategories:
                case CurrentModels.ICategories:
                    ViewCategory((int)args.id);
                    break;
                case CurrentModels.Units:
                    ViewUnit((int)args.id);
                    break;
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            var selectedItem = (sender as ComboBox).SelectedItem as Category;
            curFilter = selectedItem.Title;
            ChangeNavigate();

            IEnumerable<dynamic> list = null;

            switch (curModels)
            {
                case CurrentModels.Recipes:
                    if (selectedItem.Id == -1)
                        list = db.GetAllRecipe();
                    else
                        list = db.GetRecipesOfCategory(selectedItem.Id);
                    break;
                case CurrentModels.Ingredients:
                    if (selectedItem.Id == -1)
                        list = db.GetAllIngredient();
                    else
                        list = db.GetIngredientsOfCategory(selectedItem.Id);
                    break;
            }

            DrawTable(list);
        }

        #endregion
        
        #region ModelPages
        private void ViewRecipe(int id)
        {
            var recipe = db.GetRecipe(id);
            ViewModelLayout(curAction == Action.Create ? "Создать" : recipe.Title);
            
            var mainTableLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var panel = new Panel();
            var panelIngredients = new Panel();
            var panelInstruction = new Panel();
            var pictureBox = new PictureBox();
            var labelTitle = new Label();
            var textBoxTitle = new TextBox();
            var labelDescription = new Label();
            var textBoxDescription = new TextBox();
            var labelCategory = new Label();
            var comboBoxCategory = new ComboBox();
            var numericTime = new NumericUpDown();
            var labelTime = new Label();
            var labelIngredients = new Label();
            var tableIngredients = new TableLayoutPanel();
            var buttonAddIngredient = new Button();
            var labelInstruction = new Label();
            var browserInstruction = new WebBrowser();
            //
            // panel
            //
            panel.Dock = DockStyle.Fill;
            panel.AutoSize = true;
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(textBoxTitle);
            panel.Controls.Add(labelCategory);
            panel.Controls.Add(comboBoxCategory);
            panel.Controls.Add(labelDescription);
            panel.Controls.Add(textBoxDescription);
            panel.Controls.Add(labelTime);
            panel.Controls.Add(numericTime);
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panel);
            //
            // panelIngredients
            //
            panelIngredients.Dock = DockStyle.Fill;
            panelIngredients.AutoSize = true;
            panelIngredients.SizeChanged += (_sender, _e) => { labelIngredients.Left = (mainTableLayoutPanel.Width - labelIngredients.Width) / 2; };
            panelIngredients.Controls.Add(labelIngredients);
            panelIngredients.Controls.Add(tableIngredients);
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panelIngredients);
            //
            // panelInstruction
            //
            panelInstruction.Dock = DockStyle.Fill;
            panelInstruction.AutoSize = true;
            panelInstruction.SizeChanged += (_sender, _e) => { labelInstruction.Left = (mainTableLayoutPanel.Width - labelInstruction.Width) / 2; };
            panelInstruction.Controls.Add(buttonAddIngredient);
            panelInstruction.Controls.Add(labelInstruction);
            panelInstruction.Controls.Add(browserInstruction);
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panelInstruction);
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top;
            pictureBox.Size = new Size(350, 300);
            pictureBox.Location = new Point((panel.Width - pictureBox.Width) / 2, 20);
            pictureBox.Image = curAction == Action.Create ? Properties.Resources.DefaultImage : recipe.GetImage();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            pictureBox.Name = "pictureBox";
            if (curAction == Action.Edit || curAction == Action.Create)
                pictureBox.Click += ReadPicture;
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTitle.Text = "Название";
            labelTitle.Size = new Size(labelTitle.PreferredWidth, labelTitle.PreferredHeight);
            labelTitle.Location = new Point(panel.Width / 6, pictureBox.Bottom + 30);
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxTitle.Location = new Point(labelTitle.Right + 15, pictureBox.Bottom + 30);
            textBoxTitle.Size = new Size(panel.Width - labelTitle.Left - textBoxTitle.Left, 32);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Text = curAction == Action.Create ? "" : recipe.Title;
            textBoxTitle.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategory.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(textBoxTitle.Left, textBoxTitle.Bottom + 20);
            comboBoxCategory.Width = textBoxTitle.Width;

            #region Filling comboBox.
            if (curAction == Action.Create || curAction == Action.Edit)
            {
                db.GetAllIngredientCategory().ForEach(i => comboBoxCategory.Items.Add(i));
                if (curAction == Action.Edit)
                    comboBoxCategory.SelectedItem = comboBoxCategory.Items.Cast<Category>().Single(i => i.Id == recipe.Category.Id);
            }
            else
            {
                comboBoxCategory.Items.Add(recipe.Category);
                comboBoxCategory.SelectedItem = recipe.Category;
            }
            #endregion

            comboBoxCategory.DisplayMember = "Title";
            comboBoxCategory.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            comboBoxCategory.Name = "comboBoxCategory";
            // 
            // labelCategory
            // 
            labelCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelCategory.Top  = comboBoxCategory.Top;
            labelCategory.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCategory.Text = "Категория";
            labelCategory.Size = new Size(labelCategory.PreferredWidth, labelCategory.PreferredHeight);
            labelCategory.Left = textBoxTitle.Left - (labelCategory.Width + 20);         
            // 
            // textBoxDescription
            // 
            textBoxDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDescription.Multiline = true;
            textBoxDescription.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxDescription.Size = new Size(textBoxTitle.Width, 300);
            textBoxDescription.Location = new Point(textBoxTitle.Left, comboBoxCategory.Bottom + 15);
            textBoxDescription.Text = curAction == Action.Create ? "" : recipe.Description;
            textBoxDescription.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            textBoxDescription.Name = "textBoxDescription";
            // 
            // labelDescription
            // 
            labelDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelDescription.Top  = textBoxDescription.Top;
            labelDescription.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelDescription.Text = "Описание";
            labelDescription.Size = new Size(labelDescription.PreferredWidth, labelDescription.PreferredHeight);
            labelDescription.Left = textBoxDescription.Left - (labelDescription.Width + 20);
            // 
            // numericTime
            // 
            numericTime.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            numericTime.AutoSize = true;
            numericTime.Location = new Point(textBoxTitle.Left, textBoxDescription.Bottom + 20);
            numericTime.Maximum = int.MaxValue;
            numericTime.Value = curAction == Action.Create ? 0 : recipe.Time;
            numericTime.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            numericTime.Name = "numericTime";
            // 
            // labelTime
            // 
            labelTime.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelTime.Top  = numericTime.Top;
            labelTime.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTime.Text = "Готовка(мин)";
            labelTime.Size = new Size(labelTime.PreferredWidth, labelTime.PreferredHeight);
            labelTime.Left = textBoxTitle.Left - (labelTime.Width + 20);
            // 
            // labelIngredients
            // 
            labelIngredients.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelIngredients.Top  = 30;
            labelIngredients.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelIngredients.Text = "Ингредиенты";
            labelIngredients.Size = new Size(labelIngredients.PreferredWidth, labelIngredients.PreferredHeight);
            labelIngredients.Left = (mainTableLayoutPanel.Width - labelIngredients.Width) / 2;
            // 
            // tableIngredients
            // 
            tableIngredients.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tableIngredients.AutoSize = true;
            tableIngredients.ColumnCount = 3;
            tableIngredients.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableIngredients.ColumnStyles.Add(new ColumnStyle());
            tableIngredients.ColumnStyles.Add(new ColumnStyle());
            tableIngredients.Location = new Point(labelTitle.Right + 5, labelIngredients.Bottom + 15);
            tableIngredients.Width = mainTableLayoutPanel.Width - tableIngredients.Left * 2;
            tableIngredients.Padding = new Padding(0, 0, 0, 5);
            tableIngredients.Name = "tableIngredients";
            
            #region Filling the table.
            if (curAction != Action.Create)
            foreach (var ingredient in recipe.Ingredients)
            {
                var labelIngredient = new Label();
                var labelEdit = new LinkLabel();
                var labelDelete = new LinkLabel();
                // 
                // labelIngredient
                // 
                labelIngredient.AutoEllipsis = true;
                labelIngredient.AutoSize = true;
                labelIngredient.BackColor = Color.Transparent;
                labelIngredient.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelIngredient.Location = new Point(3, 0);
                labelIngredient.Text = "•  " + ingredient.Title + " — " + ingredient.Quantity + " " + ingredient.Unit.Title;
                // 
                // labelEdit
                // 
                labelEdit.AutoSize = true;
                labelEdit.BackColor = Color.Transparent;
                labelEdit.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelEdit.TabStop = true;
                labelEdit.Text = "Изменить";
                labelEdit.Links[0].LinkData = new { id = ingredient.Id, action = Action.Edit };
                labelEdit.LinkClicked += EditIngredientInRecipe;
                // 
                // labelDelete
                // 
                labelDelete.AutoSize = true;
                labelDelete.BackColor = Color.Transparent;
                labelDelete.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                labelDelete.TabStop = true;
                labelDelete.Text = "Удалить";
                labelDelete.Links[0].LinkData = new { id = ingredient.Id, action = Action.Delete };
                labelDelete.LinkClicked += DeleteIngredientFromRecipe;
                //
                // Add
                //
                tableIngredients.Controls.Add(labelIngredient);
                tableIngredients.Controls.Add(labelEdit);
                tableIngredients.Controls.Add(labelDelete);
            }
            #endregion
            //
            // buttonAddIngredient
            //
            buttonAddIngredient.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            buttonAddIngredient.FlatStyle = FlatStyle.Flat;
            buttonAddIngredient.Text = "Добавить ингредиент";
            buttonAddIngredient.AutoSize = true;
            buttonAddIngredient.Location = new Point(tableIngredients.Left + 10, 5);
            buttonAddIngredient.Click += AddIngredientToRecipe;
            // 
            // labelInstruction
            // 
            labelInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelInstruction.Top  = buttonAddIngredient.Bottom + 30;
            labelInstruction.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelInstruction.Text = "Инструкция";
            labelInstruction.Size = new Size(labelInstruction.PreferredWidth, labelInstruction.PreferredHeight);
            labelInstruction.Left = (mainTableLayoutPanel.Width - labelInstruction.Width) / 2;
            //
            // browserInstruction
            //
            browserInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            browserInstruction.Size = new Size(panelInstruction.Width - 150, 600);
            browserInstruction.Location = new Point(70, labelInstruction.Bottom + 30);


            if (curAction != Action.Details)
                DrawLinkDataAction(id);
            curFilter = id == -1 ? "Новый" : recipe.Title;
            ChangeNavigate(true);
        }

        #region Delete/Add recipe's ingredient

        private void DeleteIngredientFromRecipe(object sender, LinkLabelLinkClickedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditIngredientInRecipe(object sender, LinkLabelLinkClickedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddIngredientToRecipe(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion


        private void ViewIngredient(int id)
        {
            var ingredient = db.GetIngredient(id);
            ViewModelLayout(curAction == Action.Create ? "Создать" : ingredient.Title);

            var mainTableLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var panel = new Panel();
            var pictureBox = new PictureBox();
            var labelTitle = new Label();
            var textBoxTitle = new TextBox();
            var labelCategory = new Label();
            var comboBoxCategory = new ComboBox();
            var numericCalories = new NumericUpDown();
            var labelCalories = new Label();
            //
            // panel
            //
            panel.Dock = DockStyle.Fill;
            panel.AutoSize = true;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panel);
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top;
            pictureBox.Size = new Size(350, 300);
            pictureBox.Location = new Point((panel.Width - pictureBox.Width) / 2, 20);
            pictureBox.Image = curAction == Action.Create ? Properties.Resources.DefaultImage : ingredient.GetImage();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            pictureBox.Name = "pictureBox";
            if (curAction == Action.Edit || curAction == Action.Create)
                pictureBox.Click += ReadPicture;
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTitle.Text = "Название";
            labelTitle.Size = new Size(labelTitle.PreferredWidth, labelTitle.PreferredHeight);
            labelTitle.Location = new Point(panel.Width / 6, pictureBox.Bottom + 30);
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxTitle.Location = new Point(labelTitle.Right + 15, pictureBox.Bottom + 30);
            textBoxTitle.Size = new Size(panel.Width - labelTitle.Left - textBoxTitle.Left, 32);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Text = curAction == Action.Create ? "" : ingredient.Title;
            textBoxTitle.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategory.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(textBoxTitle.Left, textBoxTitle.Bottom + 20);
            comboBoxCategory.Width = textBoxTitle.Width;

            #region Filling comboBox.
            if (curAction == Action.Create || curAction == Action.Edit)
            {
                db.GetAllIngredientCategory().ForEach(i => comboBoxCategory.Items.Add(i));
                if (curAction == Action.Edit)
                    comboBoxCategory.SelectedItem = comboBoxCategory.Items.Cast<Category>().Single(i => i.Id == ingredient.Category.Id);
            }
            else
            {
                comboBoxCategory.Items.Add(ingredient.Category);
                comboBoxCategory.SelectedItem = ingredient.Category;
            }
            #endregion

            comboBoxCategory.DisplayMember = "Title";
            comboBoxCategory.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            comboBoxCategory.Name = "comboBoxCategory";
            // 
            // labelCategory
            // 
            labelCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelCategory.Top = comboBoxCategory.Top;
            labelCategory.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCategory.Size = new Size(92, 23);
            labelCategory.Text = "Категория";
            labelCategory.Size = new Size(labelCategory.PreferredWidth, labelCategory.PreferredHeight);
            labelCategory.Left = textBoxTitle.Left - (labelCategory.Width + 20);
            // 
            // numericCalories
            // 
            numericCalories.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            numericCalories.AutoSize = true;
            numericCalories.Location = new Point(textBoxTitle.Left, comboBoxCategory.Bottom + 20);
            numericCalories.Maximum = int.MaxValue;
            numericCalories.Value = curAction == Action.Create ? 0 : ingredient.Calories;
            numericCalories.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            numericCalories.Name = "numericTime";
            // 
            // labelCalories
            // 
            labelCalories.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelCalories.Top = numericCalories.Top;
            labelCalories.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCalories.Size = new Size(92, 23);
            labelCalories.Text = "Калории(ккал)";
            labelCalories.Size = new Size(labelCalories.PreferredWidth, labelCalories.PreferredHeight);
            labelCalories.Left = textBoxTitle.Left - (labelCalories.Width + 20);
            //
            // mainTableLayoutPanel
            //
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(textBoxTitle);
            panel.Controls.Add(labelCategory);
            panel.Controls.Add(comboBoxCategory);
            panel.Controls.Add(numericCalories);
            panel.Controls.Add(labelCalories);

            if (curAction != Action.Details)
                DrawLinkDataAction(id);
            curFilter = id == -1 ? "Новый" : ingredient.Title;
            ChangeNavigate(true);
        }


        private void ViewCategory(int id)
        {
            Category category = null;
            if (curAction == Action.Create)
                ViewModelLayout("Создать");
            else
            {
                category = curModels == CurrentModels.RCategories ? db.GetRecipeCategory(id) : db.GetIngredientCategory(id);
                ViewModelLayout(category.Title);
            }


            var mainTableLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var panel = new Panel();
            var labelTitle = new Label();
            var textBoxTitle = new TextBox();
            //
            // panel
            //
            panel.Dock = DockStyle.Fill;
            panel.AutoSize = true;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panel);
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelTitle.Top = 30;
            labelTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTitle.Text = "Название";
            labelTitle.Size = new Size(labelTitle.PreferredWidth, labelTitle.PreferredHeight);
            labelTitle.Left = panel.Width / 9;
            panel.Controls.Add(labelTitle);
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxTitle.Location = new Point(labelTitle.Right + 15, 30);
            textBoxTitle.Size = new Size(panel.Width - labelTitle.Left - textBoxTitle.Left, 32);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Text = (curAction == Action.Create ? "" : category.Title);
            textBoxTitle.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            panel.Controls.Add(textBoxTitle);

            if (curAction != Action.Details)
                DrawLinkDataAction(id);
            curFilter = id == -1 ? "Новый" : category.Title;
            ChangeNavigate(true);
        }


        private void ViewUnit(int id)
        {
            var unit = db.GetUnit(id);
            ViewModelLayout(curAction == Action.Create ? "Создать" : unit.Title);

            var mainTableLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var panel = new Panel();
            var labelTitle = new Label();
            var textBoxTitle = new TextBox();
            //
            // panel
            //
            panel.Dock = DockStyle.Fill;
            panel.AutoSize = true;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panel);
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            labelTitle.Top = 30;
            labelTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTitle.Text = "Название";
            labelTitle.Size = new Size(labelTitle.PreferredWidth, labelTitle.PreferredHeight);
            labelTitle.Left = panel.Width / 9;
            panel.Controls.Add(labelTitle);
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitle.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxTitle.Location = new Point(labelTitle.Right + 15, 30);
            textBoxTitle.Size = new Size(panel.Width - labelTitle.Left - textBoxTitle.Left, 32);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Text = (curAction == Action.Create ? "" : unit.Title);
            textBoxTitle.Enabled = (curAction == Action.Create || curAction == Action.Edit ? true : false);
            panel.Controls.Add(textBoxTitle);


            if (curAction != Action.Details)
                DrawLinkDataAction(id);
            curFilter = id == -1 ? "Новый" : unit.Title;
            ChangeNavigate(true);
        }


        private void ViewModelLayout(string title)
        {
            Controls.Clear();
            Template();

            var mainTableLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var layoutPanel = new Panel();
            var mainHeader = new Label();
            var header = new Label();
            var linkBack = new LinkLabel();
            //
            // layoutPanel
            //
            layoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(layoutPanel);
            // 
            // mainHeader
            // 
            mainHeader.Anchor = AnchorStyles.Top;
            mainHeader.Top = 25;
            mainHeader.Font = new Font("Times New Roman", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
            mainHeader.Text = title;
            mainHeader.Size = new Size(mainHeader.PreferredWidth, mainHeader.PreferredHeight);
            mainHeader.Left = (layoutPanel.Width - mainHeader.Width) / 2;
            layoutPanel.Controls.Add(mainHeader);
            // 
            // header
            // 
            header.Anchor = AnchorStyles.Top;
            header.Name = "header";
            header.Top = 60;
            header.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            switch (curModels)
            {
                case CurrentModels.Recipes:
                    header.Text = "Рецепт";
                    break;
                case CurrentModels.Ingredients:
                    header.Text = "Ингредиент";
                    break;
                case CurrentModels.RCategories:
                    header.Text = "Категория рецептов";
                    break;
                case CurrentModels.ICategories:
                    header.Text = "Категория ингредиентов";
                    break;
                case CurrentModels.Units:
                    header.Text = "Единица измерения";
                    break;
            }

            header.Size = new Size(header.PreferredWidth, header.PreferredHeight);
            header.Left = (layoutPanel.Width - header.Width) / 2;
            layoutPanel.Controls.Add(header);
            //
            // linkBack
            // 
            linkBack.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            linkBack.Top = 25;
            linkBack.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            linkBack.TabStop = true;
            linkBack.Text = "Назад";
            linkBack.Click += linkBack_Click;
            linkBack.Size = new Size(linkBack.PreferredWidth, linkBack.PreferredHeight);
            linkBack.Left = layoutPanel.Width - (linkBack.Width + 35);
            layoutPanel.Controls.Add(linkBack);
            

            layoutPanel.SizeChanged += (_sender, _e) =>
            {
                mainHeader.Left = (layoutPanel.Width - mainHeader.Width) / 2;
                header.Left = (layoutPanel.Width - header.Width) / 2;
            };
            
        }

        private void DrawLinkDataAction(int id)
        {
            var mainTableLayoutPanel = Controls["mainTableLayoutPanel"] as TableLayoutPanel;
            var linkDataAction = new LinkLabel();
            var panel = new Panel();
            //
            // panel
            //
            panel.Dock = DockStyle.Fill;
            panel.Controls.Add(linkDataAction);
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.Controls.Add(panel);
            //
            // linkDataAction
            //
            linkDataAction.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            linkDataAction.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            linkDataAction.TabStop = true;

            linkDataAction.Text = curAction == Action.Create ? "Создать" :
                                  curAction == Action.Edit   ? "Изменить" :
                                                               "Удалить";

            linkDataAction.LinkClicked += curModels == CurrentModels.Recipes ? new LinkLabelLinkClickedEventHandler(DataRecipeAction) :
                                          curModels == CurrentModels.Ingredients ? new LinkLabelLinkClickedEventHandler(DataIngredientAction) :
                                          curModels == CurrentModels.RCategories ? new LinkLabelLinkClickedEventHandler(DataRCategoryAction) :
                                          curModels == CurrentModels.ICategories ? new LinkLabelLinkClickedEventHandler(DataICategoryAction) :
                                                                                   new LinkLabelLinkClickedEventHandler(DataUnitAction);

            if (curAction != Action.Create)
                linkDataAction.Links[0].LinkData = id;

            linkDataAction.Size = new Size(linkDataAction.PreferredWidth, linkDataAction.PreferredHeight);
            linkDataAction.Location = new Point(panel.Width - linkDataAction.Width - 30, panel.Height - linkDataAction.Height - 30);

        }

        private void linkBack_Click(object sender, EventArgs e)
        {
            switch (curModels)
            {
                case CurrentModels.Recipes:
                    ButtonRecipes_Click(null, EventArgs.Empty);
                    break;
                case CurrentModels.Ingredients:
                    ButtonIngredients_Click(null, EventArgs.Empty);
                    break;
                case CurrentModels.RCategories:
                    ButtonRCategories_Click(null, EventArgs.Empty);
                    break;
                case CurrentModels.ICategories:
                    ButtonICategories_Click(null, EventArgs.Empty);
                    break;
                case CurrentModels.Units:
                    ButtonUnits_Click(null, EventArgs.Empty);
                    break;
            }
        }

        private void ReadPicture(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    (sender as PictureBox).Image = Image.FromFile(openFileDialog.FileName, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read image from disk. Original error: " + ex.Message);
                }
            }
        }

        #endregion

        #endregion


        #region DataAction

        private void DataRecipeAction(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void DataIngredientAction(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите продолжить действие?", "Подверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            if (curAction == Action.Delete)
            {
                db.DeleteIngredient((int)e.Link.LinkData);
            }

            if (curAction == Action.Create || curAction == Action.Edit)
            {
                var title = (Controls.Find("textBoxTitle", true)[0] as TextBox).Text;
                var category = (Controls.Find("comboBoxCategory", true)[0] as ComboBox).SelectedItem as Category;
                var kilocalories = (Controls.Find("numericTime", true)[0] as NumericUpDown).Value;
                var stream = new MemoryStream();

                (Controls.Find("pictureBox", true)[0] as PictureBox).Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                var picture = stream.ToArray();

                if (title == "" || category == null)
                {
                    MessageBox.Show("Имя и категория не выбраны!", "Некорректные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //if (picture == )

                if (curAction == Action.Create)
                {
                    db.AddIngredient(category.Id, title, (int)kilocalories, picture);
                }
                if (curAction == Action.Edit)
                {
                    db.UpdateIngredient(new Ingredient
                    {
                        Id = (int)e.Link.LinkData,
                        Title = title,
                        Category = category,
                        Calories = (int)kilocalories,
                        Picture = picture
                    });
                }
            }

            linkBack_Click(null, EventArgs.Empty);
        }

        private void DataRCategoryAction(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите продолжить действие?", "Подверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            if (curAction == Action.Delete)
            {
                    db.DeleteRecipeCategory((int)e.Link.LinkData);
            }

            if (curAction == Action.Create || curAction == Action.Edit)
            {
                var title = (Controls.Find("textBoxTitle", true)[0] as TextBox).Text;
                if (title == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Некорректные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (curAction == Action.Create)
                    db.AddRecipeCategory(title);

                if (curAction == Action.Edit)
                    db.UpdateRecipeCategory((int)e.Link.LinkData, title );
            }

            linkBack_Click(null, EventArgs.Empty);
        }

        private void DataICategoryAction(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите продолжить действие?", "Подверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            if (curAction == Action.Delete)
            {
                db.DeleteIngredientCategory((int)e.Link.LinkData);
            }

            if (curAction == Action.Create || curAction == Action.Edit)
            {
                var title = (Controls.Find("textBoxTitle", true)[0] as TextBox).Text;
                if (title == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Некорректные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (curAction == Action.Create)
                    db.AddIngredientCategory(title);

                if (curAction == Action.Edit)
                    db.UpdateIngredientCategory((int)e.Link.LinkData, title);
            }

            linkBack_Click(null, EventArgs.Empty);
        }

        private void DataUnitAction(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите продолжить действие?", "Подверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            if (curAction == Action.Delete)
            {
                db.DeleteUnit((int)e.Link.LinkData);
            }

            if (curAction == Action.Create || curAction == Action.Edit)
            {
                var title = (Controls.Find("textBoxTitle", true)[0] as TextBox).Text;
                if (title == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Некорректные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (curAction == Action.Create)
                {
                    db.AddUnit(title);
                }
                if (curAction == Action.Edit)
                {
                    db.UpdateUnit(new Unit { Id = (int)e.Link.LinkData, Title = title });
                }
            }

            linkBack_Click(null, EventArgs.Empty);
        }

        #endregion

    }
}
