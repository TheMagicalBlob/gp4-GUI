using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gp4ProjectBuilder {

    public partial class OptionsPage : Form {

        public OptionsPage() {
            InitializeComponent();
            MainForm.BorderFunc(this);
            MainForm.options_page_is_open = true;
            Location = new Point(MainForm.LastPos.X + 30, MainForm.LastPos.Y + 60);
            LoadOptions();
        }

        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\\
        ///--     Designer Managed Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\\
        #region Designer Managed Functions
        #pragma warning disable CS0168 // var not used
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
            this.LimitedOutputBox = new System.Windows.Forms.CheckBox();
            this.SourcePkgPathTextBox = new System.Windows.Forms.TextBox();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.CustomPasscodeTextBox = new System.Windows.Forms.TextBox();
            this.OutputDirectoryBtn = new System.Windows.Forms.Button();
            this.SourcePkgPathBtn = new System.Windows.Forms.Button();
            this.FilterBrowseBtn = new System.Windows.Forms.Button();
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
            this.CustomGP4PathTextBox.Size = new System.Drawing.Size(317, 21);
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
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExitBtn.Location = new System.Drawing.Point(359, 4);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 7;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // LimitedOutputBox
            // 
            this.LimitedOutputBox.AutoSize = true;
            this.LimitedOutputBox.Location = new System.Drawing.Point(7, 107);
            this.LimitedOutputBox.Name = "LimitedOutputBox";
            this.LimitedOutputBox.Size = new System.Drawing.Size(196, 17);
            this.LimitedOutputBox.TabIndex = 6;
            this.LimitedOutputBox.Text = "Limit Log Output (Builds .gp4 Faster)";
            this.LimitedOutputBox.UseVisualStyleBackColor = true;
            this.LimitedOutputBox.CheckedChanged += new System.EventHandler(this.LimitedOutputBox_CheckedChanged);
            // 
            // SourcePkgPathTextBox
            // 
            this.SourcePkgPathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.SourcePkgPathTextBox.Location = new System.Drawing.Point(6, 56);
            this.SourcePkgPathTextBox.Name = "SourcePkgPathTextBox";
            this.SourcePkgPathTextBox.Size = new System.Drawing.Size(317, 21);
            this.SourcePkgPathTextBox.TabIndex = 2;
            this.SourcePkgPathTextBox.Text = "Base Game .pkg Path... (For Game Patches)";
            this.SourcePkgPathTextBox.TextChanged += new System.EventHandler(this.SourcePkgPathTextBox_TextChanged);
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Italic);
            this.FilterTextBox.Location = new System.Drawing.Point(7, 133);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(316, 21);
            this.FilterTextBox.TabIndex = 3;
            this.FilterTextBox.Text = "Add Files/Folders You Want To Exclude, Seperated By Semicolons";
            this.FilterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
            // 
            // CustomPasscodeTextBox
            // 
            this.CustomPasscodeTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.CustomPasscodeTextBox.Location = new System.Drawing.Point(6, 158);
            this.CustomPasscodeTextBox.MaxLength = 32;
            this.CustomPasscodeTextBox.Name = "CustomPasscodeTextBox";
            this.CustomPasscodeTextBox.Size = new System.Drawing.Size(317, 21);
            this.CustomPasscodeTextBox.TabIndex = 4;
            this.CustomPasscodeTextBox.Text = "Add Custom .pkg Passcode Here (Defaults To All Zeros)";
            this.CustomPasscodeTextBox.TextChanged += new System.EventHandler(this.CustomPasscodeTextBox_TextChanged);
            // 
            // OutputDirectoryBtn
            // 
            this.OutputDirectoryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.OutputDirectoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OutputDirectoryBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OutputDirectoryBtn.Location = new System.Drawing.Point(325, 30);
            this.OutputDirectoryBtn.Name = "OutputDirectoryBtn";
            this.OutputDirectoryBtn.Size = new System.Drawing.Size(60, 22);
            this.OutputDirectoryBtn.TabIndex = 8;
            this.OutputDirectoryBtn.Text = "Browse...";
            this.OutputDirectoryBtn.UseVisualStyleBackColor = false;
            this.OutputDirectoryBtn.Click += new System.EventHandler(this.OutputDirectoryBtn_Click);
            // 
            // SourcePkgPathBtn
            // 
            this.SourcePkgPathBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.SourcePkgPathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SourcePkgPathBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SourcePkgPathBtn.Location = new System.Drawing.Point(325, 55);
            this.SourcePkgPathBtn.Name = "SourcePkgPathBtn";
            this.SourcePkgPathBtn.Size = new System.Drawing.Size(60, 22);
            this.SourcePkgPathBtn.TabIndex = 9;
            this.SourcePkgPathBtn.Text = "Browse...";
            this.SourcePkgPathBtn.UseVisualStyleBackColor = false;
            this.SourcePkgPathBtn.Click += new System.EventHandler(this.SourcePkgPathBtn_Click);
            // 
            // FilterBrowseBtn
            // 
            this.FilterBrowseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.FilterBrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FilterBrowseBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FilterBrowseBtn.Location = new System.Drawing.Point(325, 132);
            this.FilterBrowseBtn.Name = "FilterBrowseBtn";
            this.FilterBrowseBtn.Size = new System.Drawing.Size(60, 22);
            this.FilterBrowseBtn.TabIndex = 10;
            this.FilterBrowseBtn.Text = "Browse...";
            this.FilterBrowseBtn.UseVisualStyleBackColor = false;
            this.FilterBrowseBtn.Click += new System.EventHandler(this.FilterBrowseBtn_Click);
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(387, 195);
            this.Controls.Add(this.FilterBrowseBtn);
            this.Controls.Add(this.SourcePkgPathBtn);
            this.Controls.Add(this.OutputDirectoryBtn);
            this.Controls.Add(this.CustomPasscodeTextBox);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.SourcePkgPathTextBox);
            this.Controls.Add(this.LimitedOutputBox);
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
            MainForm.options_page_is_open = false;
            this.Dispose();
        }
        private void LoadOptions() {
            if(MainForm.gp4_output_directory != "")
                CustomGP4PathTextBox.Text = MainForm.gp4_output_directory;
            if(MainForm.pkg_source != "")
                SourcePkgPathTextBox.Text = MainForm.pkg_source;

            if(MainForm.user_blacklist != null) {
                FilterTextBox.Text = string.Empty;
                foreach(string file in MainForm.user_blacklist)
                FilterTextBox.Text += $"{file},";
                FilterTextBox.Text = FilterTextBox.Text.TrimEnd(',');
            }

            KeystoneToggleBox.Checked = MainForm.ignore_keystone;

            if(MainForm.passcode != "00000000000000000000000000000000")
                CustomPasscodeTextBox.Text = MainForm.passcode;


            // Designer Will Delete These From InitializeComponent If Added Manually
            SourcePkgPathTextBox.MouseClick += MainForm.TextBoxReady;
            SourcePkgPathTextBox.LostFocus += MainForm.TextBoxReset;
            CustomGP4PathTextBox.MouseClick += MainForm.TextBoxReady;
            CustomGP4PathTextBox.LostFocus += MainForm.TextBoxReset;
            CustomPasscodeTextBox.MouseClick += MainForm.TextBoxReady;
            CustomPasscodeTextBox.LostFocus += MainForm.TextBoxReset;
            FilterTextBox.MouseClick += MainForm.TextBoxReady;
            FilterTextBox.LostFocus += MainForm.TextBoxReset;
        }


        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        ///--     Options Related Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        #region Options Related Functions
        private void KeystoneToggleBox_CheckedChanged(object sender, EventArgs e) => MainForm.ignore_keystone = KeystoneToggleBox.Checked;
        private void LimitedOutputBox_CheckedChanged(object sender, EventArgs e) => MainForm.limit_output = LimitedOutputBox.Checked;
        
        private void CustomGP4PathTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "") return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            MainForm.gp4_output_directory = CustomGP4PathTextBox.Text;
            if(CustomGP4PathTextBox.Text != "" && CustomGP4PathTextBox.Text != MainForm.default_strings[1])
                MainForm.text_box_changed[1] = true;
        }
        private void OutputDirectoryBtn_Click(object sender, EventArgs e) {
            FolderBrowserDialog Browser = new FolderBrowserDialog();

            if(Browser.ShowDialog() == DialogResult.OK)
                CustomGP4PathTextBox.Text = Browser.SelectedPath;
        }

        private void SourcePkgPathTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "") return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            MainForm.pkg_source = SourcePkgPathTextBox.Text.Replace("\"", "");
            if(SourcePkgPathTextBox.Text != "" && SourcePkgPathTextBox.Text != MainForm.default_strings[2])
                MainForm.text_box_changed[2] = true;
        }
        private void SourcePkgPathBtn_Click(object sender, EventArgs e) {
            OpenFileDialog Browser = new OpenFileDialog();

            if(Browser.ShowDialog() == DialogResult.OK)
                SourcePkgPathTextBox.Text = Browser.FileName;
            Browser.Dispose();
        }
        private void FilterTextBox_TextChanged(object sender, EventArgs e) { // tst : eboot.bin, keystone, discname.txt; param.sfo
            if(MainForm.text_box_changed[3] == true && ((TextBox)sender).Text == "") {
                //MainForm.filter_array = null;
                return;
            }

            if(((TextBox)sender).Text == "" || (FilterTextBox.Text == MainForm.default_strings[3])) return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);
            

            int filter_strings_length = 0, char_index = 0;
            StringBuilder Builder;

            foreach(char c in (FilterTextBox.Text + ';').ToCharArray())
                if(c == ';' || c == ',')
                    filter_strings_length++;

            MainForm.user_blacklist = new string[filter_strings_length];
            MainForm.buffer = Encoding.UTF8.GetBytes((FilterTextBox.Text + ';').ToCharArray());

            try {
                for(var array_index = 0; array_index < MainForm.user_blacklist.Length; array_index++) {
                    Builder = new StringBuilder();

                    while(MainForm.buffer[char_index] != 0x3B && MainForm.buffer[char_index] != 0x2C)
                        Builder.Append(Encoding.UTF8.GetString(new byte[] { MainForm.buffer[char_index++] }));

                    char_index++;
                    MainForm.user_blacklist[array_index] = Builder.ToString().Trim(' ');
                    MainForm.text_box_changed[3] = true;
                }
            }
            catch (IndexOutOfRangeException ex) {
#if DEBUG
                Console.WriteLine($"\n{ex.StackTrace}");
#endif
            }
        }
        private void FilterBrowseBtn_Click(object sender, EventArgs e) {
            OpenFileDialog Browser = new OpenFileDialog();
            Browser.Multiselect = true;

            if(Browser.ShowDialog() == DialogResult.OK) {
                FilterTextBox.Clear();
                foreach(string file in Browser.FileNames)
                    FilterTextBox.Text += $"{file}, ";
            }
            Browser.Dispose();
        }

        private void CustomPasscodeTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "") return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

            if(CustomPasscodeTextBox.Text != MainForm.default_strings[4])
                MainForm.text_box_changed[4] = true;
            else return;

            MainForm.passcode = CustomPasscodeTextBox.Text;
        }
#endregion



        ////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     Control Declarations     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\\
        #region ControlDeclarations
        private CheckBox KeystoneToggleBox;
        private TextBox CustomGP4PathTextBox;
        private Label Title;
        private Button ExitBtn;
        private CheckBox LimitedOutputBox;
        private TextBox SourcePkgPathTextBox;
        private TextBox FilterTextBox;
        private TextBox CustomPasscodeTextBox;
        private Button OutputDirectoryBtn;
        private Button SourcePkgPathBtn;
        private Button FilterBrowseBtn;
        #endregion
    }
}
