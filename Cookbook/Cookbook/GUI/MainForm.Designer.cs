using System.Windows.Forms;

namespace Cookbook.GUI
{
    partial class MainForm
    {
        /// Обязательная переменная конструктора.
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSelection = new System.Windows.Forms.TabPage();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.trackTime = new System.Windows.Forms.TrackBar();
            this.buttonFind = new System.Windows.Forms.Button();
            this.listIngredients = new System.Windows.Forms.CheckedListBox();
            this.fridge = new System.Windows.Forms.ListBox();
            this.listCategoriesOfIngredient = new System.Windows.Forms.ListBox();
            this.tabPageFind = new System.Windows.Forms.TabPage();
            this.tabPageCategories = new System.Windows.Forms.TabPage();
            this.tableCategoriesPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelMain = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTime)).BeginInit();
            this.tabPageCategories.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageSelection);
            this.tabControl.Controls.Add(this.tabPageFind);
            this.tabControl.Controls.Add(this.tabPageCategories);
            this.tabControl.Location = new System.Drawing.Point(3, 55);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(759, 430);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageSelection
            // 
            this.tabPageSelection.BackColor = System.Drawing.Color.Linen;
            this.tabPageSelection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageSelection.Controls.Add(this.buttonClear);
            this.tabPageSelection.Controls.Add(this.labelTime);
            this.tabPageSelection.Controls.Add(this.trackTime);
            this.tabPageSelection.Controls.Add(this.buttonFind);
            this.tabPageSelection.Controls.Add(this.listIngredients);
            this.tabPageSelection.Controls.Add(this.fridge);
            this.tabPageSelection.Controls.Add(this.listCategoriesOfIngredient);
            this.tabPageSelection.Location = new System.Drawing.Point(4, 22);
            this.tabPageSelection.Name = "tabPageSelection";
            this.tabPageSelection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSelection.Size = new System.Drawing.Size(751, 404);
            this.tabPageSelection.TabIndex = 0;
            this.tabPageSelection.Text = "Selection";
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(549, 298);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(57, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(379, 270);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(58, 13);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "Cook Time";
            // 
            // trackTime
            // 
            this.trackTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackTime.AutoSize = false;
            this.trackTime.Location = new System.Drawing.Point(209, 261);
            this.trackTime.Name = "trackTime";
            this.trackTime.Size = new System.Drawing.Size(164, 45);
            this.trackTime.TabIndex = 4;
            // 
            // buttonFind
            // 
            this.buttonFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFind.Location = new System.Drawing.Point(604, 347);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(121, 38);
            this.buttonFind.TabIndex = 3;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // listIngredients
            // 
            this.listIngredients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listIngredients.FormattingEnabled = true;
            this.listIngredients.Location = new System.Drawing.Point(196, 14);
            this.listIngredients.Name = "listIngredients";
            this.listIngredients.Size = new System.Drawing.Size(347, 214);
            this.listIngredients.TabIndex = 2;
            this.listIngredients.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listIngredients_ItemCheck);
            // 
            // fridge
            // 
            this.fridge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fridge.FormattingEnabled = true;
            this.fridge.Location = new System.Drawing.Point(549, 6);
            this.fridge.Name = "fridge";
            this.fridge.Size = new System.Drawing.Size(191, 277);
            this.fridge.TabIndex = 1;
            this.fridge.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFridge_MouseDoubleClick);
            // 
            // listCategoriesOfIngredient
            // 
            this.listCategoriesOfIngredient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listCategoriesOfIngredient.FormattingEnabled = true;
            this.listCategoriesOfIngredient.Location = new System.Drawing.Point(8, 6);
            this.listCategoriesOfIngredient.Name = "listCategoriesOfIngredient";
            this.listCategoriesOfIngredient.Size = new System.Drawing.Size(182, 342);
            this.listCategoriesOfIngredient.TabIndex = 0;
            this.listCategoriesOfIngredient.SelectedIndexChanged += new System.EventHandler(this.listIngredientCategories_SelectedIndexChanged);
            // 
            // tabPageFind
            // 
            this.tabPageFind.AutoScroll = true;
            this.tabPageFind.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageFind.Location = new System.Drawing.Point(4, 22);
            this.tabPageFind.Name = "tabPageFind";
            this.tabPageFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFind.Size = new System.Drawing.Size(751, 404);
            this.tabPageFind.TabIndex = 1;
            this.tabPageFind.Text = "Find";
            this.tabPageFind.UseVisualStyleBackColor = true;
            // 
            // tabPageCategories
            // 
            this.tabPageCategories.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageCategories.Controls.Add(this.tableCategoriesPanel);
            this.tabPageCategories.Location = new System.Drawing.Point(4, 22);
            this.tabPageCategories.Name = "tabPageCategories";
            this.tabPageCategories.Size = new System.Drawing.Size(751, 404);
            this.tabPageCategories.TabIndex = 2;
            this.tabPageCategories.Text = "Recipe Categories";
            this.tabPageCategories.UseVisualStyleBackColor = true;
            // 
            // tableCategoriesPanel
            // 
            this.tableCategoriesPanel.AutoScroll = true;
            this.tableCategoriesPanel.BackColor = System.Drawing.Color.Ivory;
            this.tableCategoriesPanel.ColumnCount = 4;
            this.tableCategoriesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableCategoriesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableCategoriesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableCategoriesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableCategoriesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableCategoriesPanel.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableCategoriesPanel.Location = new System.Drawing.Point(0, 0);
            this.tableCategoriesPanel.Name = "tableCategoriesPanel";
            this.tableCategoriesPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableCategoriesPanel.RowCount = 10;
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCategoriesPanel.Size = new System.Drawing.Size(747, 400);
            this.tableCategoriesPanel.TabIndex = 0;
            // 
            // labelMain
            // 
            this.labelMain.AutoSize = true;
            this.labelMain.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMain.Location = new System.Drawing.Point(9, 9);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(186, 33);
            this.labelMain.TabIndex = 2;
            this.labelMain.Text = "Are you hungry?)";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(205, 21);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(449, 20);
            this.textBoxSearch.TabIndex = 3;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(670, 21);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(71, 20);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(764, 489);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.labelMain);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(671, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cookbook";
            this.tabControl.ResumeLayout(false);
            this.tabPageSelection.ResumeLayout(false);
            this.tabPageSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTime)).EndInit();
            this.tabPageCategories.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TabControl tabControl;
        private TabPage tabPageSelection;
        private Label labelMain;
        private TextBox textBoxSearch;
        private Button buttonSearch;
        private TabPage tabPageCategories;
        private Button buttonFind;
        private CheckedListBox listIngredients;
        private ListBox fridge;
        private ListBox listCategoriesOfIngredient;
        private Label labelTime;
        private TrackBar trackTime;
        private TableLayoutPanel tableCategoriesPanel;
        private Button buttonClear;
        private TabPage tabPageFind;
    }
}

