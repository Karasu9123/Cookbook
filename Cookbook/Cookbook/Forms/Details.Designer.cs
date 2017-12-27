namespace Cookbook.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Details));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panelRecipes = new System.Windows.Forms.Panel();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.panelRecipes.SuspendLayout();
            this.panelBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(582, 459);
            this.webBrowser.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(184, 0);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClear.Location = new System.Drawing.Point(12, 430);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(176, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // panelRecipes
            // 
            this.panelRecipes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelRecipes.AutoScroll = true;
            this.panelRecipes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelRecipes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRecipes.Controls.Add(this.tableLayoutPanel);
            this.panelRecipes.Location = new System.Drawing.Point(0, 0);
            this.panelRecipes.Name = "panelRecipes";
            this.panelRecipes.Size = new System.Drawing.Size(202, 424);
            this.panelRecipes.TabIndex = 4;
            // 
            // panelBrowser
            // 
            this.panelBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBrowser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBrowser.Controls.Add(this.webBrowser);
            this.panelBrowser.Location = new System.Drawing.Point(208, 0);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(586, 463);
            this.panelBrowser.TabIndex = 5;
            // 
            // Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(794, 465);
            this.Controls.Add(this.panelBrowser);
            this.Controls.Add(this.panelRecipes);
            this.Controls.Add(this.buttonClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(580, 435);
            this.Name = "Details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Детали";
            this.panelRecipes.ResumeLayout(false);
            this.panelRecipes.PerformLayout();
            this.panelBrowser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Panel panelRecipes;
        private System.Windows.Forms.Panel panelBrowser;
    }
}