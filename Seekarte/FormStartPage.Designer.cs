using System.Drawing;

namespace Seekarte
{
    partial class FormStartPage
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public int GetMapWidth()
        {
            return Map.Width;
        }

        public int GetMapHeight()
        {
            return Map.Height;
        }

        public void SetMapImage(Image image)
        {
            Map.Image = image;
        }

        public void SetCountryLabel(string text)
        {
            labelCountry.Text = text;
        }

        public void SetCountryLabel(bool visible)
        {
            labelCountry.Visible = visible;
        }



        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAdmin = new System.Windows.Forms.Button();
            this.buttonPolen = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Map = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTartarenreich = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelRunde = new System.Windows.Forms.Label();
            this.labelCountry = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAdmin
            // 
            this.buttonAdmin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAdmin.Location = new System.Drawing.Point(3, 3);
            this.buttonAdmin.Name = "buttonAdmin";
            this.buttonAdmin.Size = new System.Drawing.Size(188, 44);
            this.buttonAdmin.TabIndex = 0;
            this.buttonAdmin.Text = "admin";
            this.buttonAdmin.UseVisualStyleBackColor = true;
            this.buttonAdmin.Click += new System.EventHandler(this.Button1_Click);
            // 
            // buttonPolen
            // 
            this.buttonPolen.AccessibleName = "polen";
            this.buttonPolen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPolen.Location = new System.Drawing.Point(3, 53);
            this.buttonPolen.Name = "buttonPolen";
            this.buttonPolen.Size = new System.Drawing.Size(188, 44);
            this.buttonPolen.TabIndex = 1;
            this.buttonPolen.Text = "Polen";
            this.buttonPolen.UseVisualStyleBackColor = true;
            this.buttonPolen.Click += new System.EventHandler(this.buttonPolen_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(1387, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(344, 38);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Map
            // 
            this.Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Map.InitialImage = null;
            this.Map.Location = new System.Drawing.Point(3, 3);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(1528, 1094);
            this.Map.TabIndex = 2;
            this.Map.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1740, 1156);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.Controls.Add(this.Map, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1734, 1100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.buttonAdmin, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonPolen, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonTartarenreich, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.button2, 0, 8);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1537, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 10;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(194, 1094);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // buttonTartarenreich
            // 
            this.buttonTartarenreich.AccessibleName = "Tartarenreich";
            this.buttonTartarenreich.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTartarenreich.Location = new System.Drawing.Point(3, 103);
            this.buttonTartarenreich.Name = "buttonTartarenreich";
            this.buttonTartarenreich.Size = new System.Drawing.Size(188, 44);
            this.buttonTartarenreich.TabIndex = 2;
            this.buttonTartarenreich.Text = "Tartarenreich";
            this.buttonTartarenreich.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(3, 403);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 44);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.labelRunde, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.button3, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelCountry, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1734, 44);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // labelRunde
            // 
            this.labelRunde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRunde.AutoSize = true;
            this.labelRunde.Location = new System.Drawing.Point(3, 9);
            this.labelRunde.Name = "labelRunde";
            this.labelRunde.Size = new System.Drawing.Size(340, 25);
            this.labelRunde.TabIndex = 6;
            this.labelRunde.Text = "Runde 1";
            // 
            // labelCountry
            // 
            this.labelCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCountry.AutoSize = true;
            this.labelCountry.Location = new System.Drawing.Point(349, 9);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new System.Drawing.Size(340, 25);
            this.labelCountry.TabIndex = 7;
            this.labelCountry.Text = "Country";
            this.labelCountry.Visible = false;
            // 
            // FormStartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1740, 1156);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormStartPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormStartPage_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAdmin;
        private System.Windows.Forms.Button buttonPolen;
        private System.Windows.Forms.Button buttonTartarenreich;
        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelRunde;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label labelCountry;
    }
}

