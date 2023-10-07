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
using System.Xml.Linq;

namespace Gp4ProjectBuilder {

    public partial class OptionsPage : Form {
        public OptionsPage() {
            InitializeComponent();
            MainForm.BorderFunc(this);
            MainForm.OptionsAreOpen = true;
            Location = new Point(MainForm.LastPos.X + 30, MainForm.LastPos.Y + 60);
            LoadOptions();
        }

        #region Designer Managed Functions
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\\
        ///--     Designer Managed Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\\
        private IContainer components = null;
        protected override void Dispose(bool disposing) {
            if(disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            this.KeystoneToggleBox = new System.Windows.Forms.CheckBox();
            this.CustomGP4PathTextBox = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.IgnoreOutputBox = new System.Windows.Forms.CheckBox();
            this.CustomPKGPathTextBox = new System.Windows.Forms.TextBox();
            this.IgnoreFilterTextBox = new System.Windows.Forms.TextBox();
            this.CustomPasscodeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // KeystoneToggleBox
            // 
            this.KeystoneToggleBox.AutoSize = true;
            this.KeystoneToggleBox.Location = new System.Drawing.Point(8, 85);
            this.KeystoneToggleBox.Name = "KeystoneToggleBox";
            this.KeystoneToggleBox.Size = new System.Drawing.Size(103, 17);
            this.KeystoneToggleBox.TabIndex = 5;
            this.KeystoneToggleBox.Text = "Ignore Keystone";
            this.KeystoneToggleBox.UseVisualStyleBackColor = true;
            this.KeystoneToggleBox.CheckedChanged += new System.EventHandler(this.KeystoneToggleBox_CheckedChanged);
            // 
            // CustomGP4PathTextBox
            // 
            this.CustomGP4PathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.CustomGP4PathTextBox.Location = new System.Drawing.Point(6, 30);
            this.CustomGP4PathTextBox.Name = "CustomGP4PathTextBox";
            this.CustomGP4PathTextBox.Size = new System.Drawing.Size(376, 21);
            this.CustomGP4PathTextBox.TabIndex = 1;
            this.CustomGP4PathTextBox.Text = "Add A Custom .gp4 Output Directory Here...";
            this.CustomGP4PathTextBox.TextChanged += new System.EventHandler(this.CustomGP4PathTextBox_TextChanged);
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
            this.ExitBtn.Location = new System.Drawing.Point(359, 4);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 7;
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
            this.IgnoreOutputBox.TabIndex = 6;
            this.IgnoreOutputBox.Text = "Limit Log Output (Builds .gp4 Faster)";
            this.IgnoreOutputBox.UseVisualStyleBackColor = true;
            // 
            // CustomPKGPathTextBox
            // 
            this.CustomPKGPathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.CustomPKGPathTextBox.Location = new System.Drawing.Point(6, 56);
            this.CustomPKGPathTextBox.Name = "CustomPKGPathTextBox";
            this.CustomPKGPathTextBox.Size = new System.Drawing.Size(375, 21);
            this.CustomPKGPathTextBox.TabIndex = 2;
            this.CustomPKGPathTextBox.Text = "Base Game .pkg Path... (For Game Patches)";
            this.CustomPKGPathTextBox.TextChanged += new System.EventHandler(this.CustomPKGPathTextBox_TextChanged);
            // 
            // IgnoreFilterTextBox
            // 
            this.IgnoreFilterTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Italic);
            this.IgnoreFilterTextBox.Location = new System.Drawing.Point(7, 133);
            this.IgnoreFilterTextBox.Name = "IgnoreFilterTextBox";
            this.IgnoreFilterTextBox.Size = new System.Drawing.Size(375, 21);
            this.IgnoreFilterTextBox.TabIndex = 3;
            this.IgnoreFilterTextBox.Text = "Add Files/Folders You Want To Exclude, Seperated By Semicolons";
            this.IgnoreFilterTextBox.TextChanged += new System.EventHandler(this.IgnoreFilterTextBox_TextChanged);
            // 
            // CustomPasscodeTextBox
            // 
            this.CustomPasscodeTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.CustomPasscodeTextBox.Location = new System.Drawing.Point(6, 158);
            this.CustomPasscodeTextBox.MaxLength = 32;
            this.CustomPasscodeTextBox.Name = "CustomPasscodeTextBox";
            this.CustomPasscodeTextBox.Size = new System.Drawing.Size(375, 21);
            this.CustomPasscodeTextBox.TabIndex = 4;
            this.CustomPasscodeTextBox.Text = "Add Custom .pkg Passcode Here (Defaults To All Zeros)";
            this.CustomPasscodeTextBox.TextChanged += new System.EventHandler(this.CustomPasscodeTextBox_TextChanged);
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(387, 195);
            this.Controls.Add(this.CustomPasscodeTextBox);
            this.Controls.Add(this.IgnoreFilterTextBox);
            this.Controls.Add(this.CustomPKGPathTextBox);
            this.Controls.Add(this.IgnoreOutputBox);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.CustomGP4PathTextBox);
            this.Controls.Add(this.KeystoneToggleBox);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OptionsPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private void ExitBtn_Click(object sender, EventArgs e) {
            MainForm.OptionsAreOpen = false;
            this.Dispose();
        }
        private void LoadOptions() {
            if(MainForm.gp4_output_directory != "")
                CustomGP4PathTextBox.Text = MainForm.gp4_output_directory;
            if(MainForm.pkg_source != "")
                CustomPKGPathTextBox.Text = MainForm.pkg_source;

            if(MainForm.filter_array != null) {
                IgnoreFilterTextBox.Text = string.Empty;
                foreach(string file in MainForm.filter_array)
                IgnoreFilterTextBox.Text += $"{file},";
                IgnoreFilterTextBox.Text = IgnoreFilterTextBox.Text.TrimEnd(',');
            }


            if(MainForm.passcode != "00000000000000000000000000000000")
                CustomPasscodeTextBox.Text = MainForm.passcode;


            // Designer Will Delete These From InitializeComponent If Added Manually
            CustomPKGPathTextBox.MouseClick += MainForm.TextBoxReady;
            CustomPKGPathTextBox.LostFocus += MainForm.TextBoxReset;
            CustomGP4PathTextBox.MouseClick += MainForm.TextBoxReady;
            CustomGP4PathTextBox.LostFocus += MainForm.TextBoxReset;
            CustomPasscodeTextBox.MouseClick += MainForm.TextBoxReady;
            CustomPasscodeTextBox.LostFocus += MainForm.TextBoxReset;
            IgnoreFilterTextBox.MouseClick += MainForm.TextBoxReady;
            IgnoreFilterTextBox.LostFocus += MainForm.TextBoxReset;
        }


        #region Options Related Functions
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        ///--     Options Related Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        private void KeystoneToggleBox_CheckedChanged(object sender, EventArgs e) => MainForm.ignore_keystone = KeystoneToggleBox.Checked;

        private void CustomGP4PathTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "") return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            MainForm.gp4_output_directory = CustomGP4PathTextBox.Text;
            if(CustomGP4PathTextBox.Text != "" && CustomGP4PathTextBox.Text != MainForm.default_strings[1])
                MainForm.text_box_changed[1] = true;
        }
        private void CustomPKGPathTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "") return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            MainForm.pkg_source = CustomPKGPathTextBox.Text.Replace("\"", "");
            if(CustomPKGPathTextBox.Text != "" && CustomPKGPathTextBox.Text != MainForm.default_strings[2])
                MainForm.text_box_changed[2] = true;
        }
        private void IgnoreFilterTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "" || (IgnoreFilterTextBox.Text == MainForm.default_strings[3])) return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            StringBuilder Builder;
            int filter_strings_length = 0, char_index = 0;
            MainForm.buffer = Encoding.UTF8.GetBytes((IgnoreFilterTextBox.Text + ';').ToCharArray());

            foreach(char c in (IgnoreFilterTextBox.Text + ';').ToCharArray())
            if(c == ';' || c == ',') filter_strings_length++;

            MainForm.filter_array = new string[filter_strings_length];

            try {
                for(var array_index = 0; array_index < MainForm.filter_array.Length; array_index++) {
                    Builder = new StringBuilder();

                    while(MainForm.buffer[char_index] != 0x3B && MainForm.buffer[char_index] != 0x2C)
                    Builder.Append(Encoding.UTF8.GetString(new byte[] { MainForm.buffer[char_index++] })); // Just Take A Byte, You Fussy Prick

                    char_index++;
                    MainForm.filter_array[array_index] = Builder.ToString();
                    MainForm.text_box_changed[3] = true;
                }
            }
            catch (IndexOutOfRangeException ex) {
#if DEBUG
                Console.WriteLine($"\n{ex.StackTrace}");
#endif
            }
        }

        private void CustomPasscodeTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "") return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            MainForm.passcode = CustomPasscodeTextBox.Text;
            if(CustomPasscodeTextBox.Text != MainForm.default_strings[4])
                MainForm.text_box_changed[4] = true;
        }
#endregion




        #region ControlDeclarations
        ////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     Control Declarations     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\\
        private CheckBox KeystoneToggleBox;
        private TextBox CustomGP4PathTextBox;
        private Label Title;
        private Button ExitBtn;
        private CheckBox IgnoreOutputBox;
        private TextBox CustomPKGPathTextBox;
        private TextBox IgnoreFilterTextBox;
        private TextBox CustomPasscodeTextBox;
        #endregion
    }
}
