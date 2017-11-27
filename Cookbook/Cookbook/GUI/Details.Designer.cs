namespace Cookbook.GUI
{
    partial class Details
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewRecipes = new System.Windows.Forms.ListView();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // listViewRecipes
            // 
            this.listViewRecipes.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewRecipes.Location = new System.Drawing.Point(0, 0);
            this.listViewRecipes.Name = "listViewRecipes";
            this.listViewRecipes.Size = new System.Drawing.Size(187, 573);
            this.listViewRecipes.TabIndex = 0;
            this.listViewRecipes.UseCompatibleStateImageBehavior = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(187, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(569, 573);
            this.webBrowser.TabIndex = 1;
            // 
            // Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 573);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.listViewRecipes);
            this.Name = "Details";
            this.Text = "Details";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewRecipes;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}