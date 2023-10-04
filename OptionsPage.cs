using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace Gp4ProjectBuilder {

    public partial class OptionsPage : Form {
        public OptionsPage() {
            InitializeComponent();
            MainForm.BorderFunc(this);
            MainForm.OptionsAreOpen = true;
            Location = new Point(MainForm.LastPos.X + 30, MainForm.LastPos.Y + 60);
        }


        private void CustomPKGPathBox_TextChanged(object sender, EventArgs e) {
            if(CustomPKGPathBox.Font.Italic) {

            }

        }

        private void IgnoreFilterBox_TextChanged(object sender, EventArgs e) {
            if(IgnoreFilterBox.Font.Italic) {

            }

        }

        private void CustomPasscodeBox_TextChanged(object sender, EventArgs e) {
            if(CustomPasscodeBox.Font.Italic) {

            }

        }

        private void CustomGP4PathBox_TextChanged(object sender, EventArgs e) {
            if(CustomGP4PathBox.Font.Italic) {

            }

        }


        #region Designer
        private IContainer components = null;
        protected override void Dispose(bool disposing) {
            if(disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            this.KeystoneToggleBox = new System.Windows.Forms.CheckBox();
            this.CustomGP4PathBox = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.IgnoreOutputBox = new System.Windows.Forms.CheckBox();
            this.CustomPKGPathBox = new System.Windows.Forms.TextBox();
            this.IgnoreFilterBox = new System.Windows.Forms.TextBox();
            this.CustomPasscodeBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // KeystoneToggleBox
            // 
            this.KeystoneToggleBox.AutoSize = true;
            this.KeystoneToggleBox.Location = new System.Drawing.Point(8, 85);
            this.KeystoneToggleBox.Name = "KeystoneToggleBox";
            this.KeystoneToggleBox.Size = new System.Drawing.Size(103, 17);
            this.KeystoneToggleBox.TabIndex = 1;
            this.KeystoneToggleBox.Text = "Ignore Keystone";
            this.KeystoneToggleBox.UseVisualStyleBackColor = true;
            this.KeystoneToggleBox.CheckedChanged += new System.EventHandler(this.KeystoneToggleBox_CheckedChanged);
            // 
            // CustomGP4PathBox
            // 
            this.CustomGP4PathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomGP4PathBox.Location = new System.Drawing.Point(6, 30);
            this.CustomGP4PathBox.Name = "CustomGP4PathBox";
            this.CustomGP4PathBox.Size = new System.Drawing.Size(376, 20);
            this.CustomGP4PathBox.TabIndex = 2;
            this.CustomGP4PathBox.Text = "Custom .gp4 Output Directory...";
            this.CustomGP4PathBox.TextChanged += new System.EventHandler(this.CustomGP4PathBox_TextChanged);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Gadugi", 8.5F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(162, 4);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(50, 16);
            this.Title.TabIndex = 3;
            this.Title.Text = "Options";
            // 
            // ExitBtn
            // 
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExitBtn.Location = new System.Drawing.Point(356, 2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 5;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // IgnoreOutputBox
            // 
            this.IgnoreOutputBox.AutoSize = true;
            this.IgnoreOutputBox.Location = new System.Drawing.Point(7, 107);
            this.IgnoreOutputBox.Name = "IgnoreOutputBox";
            this.IgnoreOutputBox.Size = new System.Drawing.Size(196, 17);
            this.IgnoreOutputBox.TabIndex = 8;
            this.IgnoreOutputBox.Text = "Limit Log Output (Builds .gp4 Faster)";
            this.IgnoreOutputBox.UseVisualStyleBackColor = true;
            // 
            // CustomPKGPathBox
            // 
            this.CustomPKGPathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomPKGPathBox.Location = new System.Drawing.Point(6, 56);
            this.CustomPKGPathBox.Name = "CustomPKGPathBox";
            this.CustomPKGPathBox.Size = new System.Drawing.Size(375, 20);
            this.CustomPKGPathBox.TabIndex = 9;
            this.CustomPKGPathBox.Text = "Custom Base Game .pkg Directory To GP4...";
            this.CustomPKGPathBox.TextChanged += new System.EventHandler(this.CustomPKGPathBox_TextChanged);
            // 
            // IgnoreFilterBox
            // 
            this.IgnoreFilterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IgnoreFilterBox.Location = new System.Drawing.Point(7, 133);
            this.IgnoreFilterBox.Name = "IgnoreFilterBox";
            this.IgnoreFilterBox.Size = new System.Drawing.Size(375, 20);
            this.IgnoreFilterBox.TabIndex = 10;
            this.IgnoreFilterBox.Text = "Add Files/Folders You Want To Filter Out Here, Seperated By Semicolons";
            this.IgnoreFilterBox.TextChanged += new System.EventHandler(this.IgnoreFilterBox_TextChanged);
            // 
            // CustomPasscodeBox
            // 
            this.CustomPasscodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomPasscodeBox.Location = new System.Drawing.Point(6, 158);
            this.CustomPasscodeBox.Name = "CustomPasscodeBox";
            this.CustomPasscodeBox.Size = new System.Drawing.Size(375, 20);
            this.CustomPasscodeBox.TabIndex = 11;
            this.CustomPasscodeBox.Text = "Add Custom .pkg Passcode Here (Defaults To All Zeros)";
            this.CustomPasscodeBox.TextChanged += new System.EventHandler(this.CustomPasscodeBox_TextChanged);
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(387, 195);
            this.Controls.Add(this.CustomPasscodeBox);
            this.Controls.Add(this.IgnoreFilterBox);
            this.Controls.Add(this.CustomPKGPathBox);
            this.Controls.Add(this.IgnoreOutputBox);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.CustomGP4PathBox);
            this.Controls.Add(this.KeystoneToggleBox);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OptionsPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void ExitBtn_Click(object sender, EventArgs e) {
            MainForm.OptionsAreOpen = false;
            this.Dispose();
        }

        #endregion


        private void Out(object s) {
            if(!IgnoreOutputBox.Checked)
                Console.WriteLine("\n" + s);
        }
        private void BrowseBtn_Click(object sender, EventArgs e) {
            FolderBrowserDialog Browser = new FolderBrowserDialog();
            if(Browser.ShowDialog() == DialogResult.OK)
                CustomGP4PathBox.Text = Browser.SelectedPath;
        }
        private void KeystoneToggleBox_CheckedChanged(object sender, EventArgs e) => MainForm.ignore_keystone = KeystoneToggleBox.Checked;






        private CheckBox KeystoneToggleBox;
        private TextBox CustomGP4PathBox;
        private Label Title;
        private Button ExitBtn;
        private CheckBox IgnoreOutputBox;
        private TextBox CustomPKGPathBox;
        private TextBox IgnoreFilterBox;
        private TextBox CustomPasscodeBox;
    }
}
