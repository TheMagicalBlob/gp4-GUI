using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GP4_GUI {

    public partial class OptionsPage : Form {

        public OptionsPage(MainForm MainForm, Point LastPos) {
            _MainForm = MainForm;

            InitializeComponent();
            _MainForm.BorderFunc(this);
            _MainForm.options_page_is_open = true;

            Location = new Point(LastPos.X + 30, LastPos.Y + 60);
            TinyVersionLabel.Text = MainForm.Version;

#region Load Options
            // Restore Edited User Settings To Controls

            if(_MainForm.Gp4OutputDirectory != null)
                OutputPathTextBox.Text = _MainForm.Gp4OutputDirectory;

            if(_MainForm.gp4.SourcePkgPath != null)
                BasePkgPathTextBox.Text = _MainForm.gp4.SourcePkgPath;

            if(_MainForm.gp4.BlacklistedFilesOrFolders != null) {
                FilterTextBox.Text = string.Empty;
                foreach(string file in _MainForm.gp4.BlacklistedFilesOrFolders)
                    FilterTextBox.Text += $"{file},";
                FilterTextBox.Text = FilterTextBox.Text.TrimEnd(',');
            }

            AbsolutePathCheckBox.Checked = _MainForm.gp4.AbsoluteFilePaths;
            KeystoneToggleBox.Checked = _MainForm.gp4.Keystone;

            if(_MainForm.gp4.Passcode != "00000000000000000000000000000000")
                CustomPasscodeTextBox.Text = _MainForm.gp4.Passcode;


            // Designer Will Delete These From InitializeComponent If Added Manually
            BasePkgPathTextBox.MouseClick += _MainForm.TextBoxReady;
            BasePkgPathTextBox.LostFocus += _MainForm.TextBoxReset;
            OutputPathTextBox.MouseClick += _MainForm.TextBoxReady;
            OutputPathTextBox.LostFocus += _MainForm.TextBoxReset;
            CustomPasscodeTextBox.MouseClick += _MainForm.TextBoxReady;
            CustomPasscodeTextBox.LostFocus += _MainForm.TextBoxReset;
            FilterTextBox.MouseClick += _MainForm.TextBoxReady;
            FilterTextBox.LostFocus += _MainForm.TextBoxReset;
#endregion
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
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.VerboseOutputBox = new System.Windows.Forms.CheckBox();
            this.BasePkgPathTextBox = new System.Windows.Forms.TextBox();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.CustomPasscodeTextBox = new System.Windows.Forms.TextBox();
            this.OutputPathBtn = new System.Windows.Forms.Button();
            this.BasePkgPathBtn = new System.Windows.Forms.Button();
            this.FilterBrowseBtn = new System.Windows.Forms.Button();
            this.TinyVersionLabel = new System.Windows.Forms.Label();
            this.AbsolutePathCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // KeystoneToggleBox
            // 
            this.KeystoneToggleBox.AutoSize = true;
            this.KeystoneToggleBox.Location = new System.Drawing.Point(169, 87);
            this.KeystoneToggleBox.Name = "KeystoneToggleBox";
            this.KeystoneToggleBox.Size = new System.Drawing.Size(103, 17);
            this.KeystoneToggleBox.TabIndex = 5;
            this.KeystoneToggleBox.Text = "Ignore Keystone";
            this.KeystoneToggleBox.UseVisualStyleBackColor = true;
            this.KeystoneToggleBox.CheckedChanged += new System.EventHandler(this.KeystoneToggleBox_CheckedChanged);
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.OutputPathTextBox.Location = new System.Drawing.Point(6, 30);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(317, 21);
            this.OutputPathTextBox.TabIndex = 1;
            this.OutputPathTextBox.Text = "Add A Custom .gp4 Output Directory Here...";
            this.OutputPathTextBox.TextChanged += new System.EventHandler(this.OutputPathTextBox_TextChanged);
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
            // VerboseOutputBox
            // 
            this.VerboseOutputBox.AutoSize = true;
            this.VerboseOutputBox.Location = new System.Drawing.Point(278, 87);
            this.VerboseOutputBox.Name = "VerboseOutputBox";
            this.VerboseOutputBox.Size = new System.Drawing.Size(100, 17);
            this.VerboseOutputBox.TabIndex = 6;
            this.VerboseOutputBox.Text = "Verbose Output";
            this.VerboseOutputBox.UseVisualStyleBackColor = true;
            this.VerboseOutputBox.CheckedChanged += new System.EventHandler(this.LimitedOutputBox_CheckedChanged);
            // 
            // BasePkgPathTextBox
            // 
            this.BasePkgPathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.BasePkgPathTextBox.Location = new System.Drawing.Point(6, 56);
            this.BasePkgPathTextBox.Name = "BasePkgPathTextBox";
            this.BasePkgPathTextBox.Size = new System.Drawing.Size(317, 21);
            this.BasePkgPathTextBox.TabIndex = 2;
            this.BasePkgPathTextBox.Text = "Base Game .pkg Path... (For Game Patches)";
            this.BasePkgPathTextBox.TextChanged += new System.EventHandler(this.BasePkgPathTextBox_TextChanged);
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Italic);
            this.FilterTextBox.Location = new System.Drawing.Point(7, 114);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(316, 21);
            this.FilterTextBox.TabIndex = 3;
            this.FilterTextBox.Text = "Blacklisted Files/Folders To Exclude, Seperated By ; or ,";
            this.FilterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
            // 
            // CustomPasscodeTextBox
            // 
            this.CustomPasscodeTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.CustomPasscodeTextBox.Location = new System.Drawing.Point(6, 139);
            this.CustomPasscodeTextBox.MaxLength = 32;
            this.CustomPasscodeTextBox.Name = "CustomPasscodeTextBox";
            this.CustomPasscodeTextBox.Size = new System.Drawing.Size(317, 21);
            this.CustomPasscodeTextBox.TabIndex = 4;
            this.CustomPasscodeTextBox.Text = "Add Custom .pkg Passcode Here (Defaults To All Zeros)";
            this.CustomPasscodeTextBox.TextChanged += new System.EventHandler(this.CustomPasscodeTextBox_TextChanged);
            // 
            // OutputPathBtn
            // 
            this.OutputPathBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.OutputPathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OutputPathBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OutputPathBtn.Location = new System.Drawing.Point(325, 30);
            this.OutputPathBtn.Name = "OutputPathBtn";
            this.OutputPathBtn.Size = new System.Drawing.Size(60, 22);
            this.OutputPathBtn.TabIndex = 8;
            this.OutputPathBtn.Text = "Browse...";
            this.OutputPathBtn.UseVisualStyleBackColor = false;
            this.OutputPathBtn.Click += new System.EventHandler(this.OutputDirectoryBtn_Click);
            // 
            // BasePkgPathBtn
            // 
            this.BasePkgPathBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.BasePkgPathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BasePkgPathBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BasePkgPathBtn.Location = new System.Drawing.Point(325, 55);
            this.BasePkgPathBtn.Name = "BasePkgPathBtn";
            this.BasePkgPathBtn.Size = new System.Drawing.Size(60, 22);
            this.BasePkgPathBtn.TabIndex = 9;
            this.BasePkgPathBtn.Text = "Browse...";
            this.BasePkgPathBtn.UseVisualStyleBackColor = false;
            this.BasePkgPathBtn.Click += new System.EventHandler(this.SourcePkgPathBtn_Click);
            // 
            // FilterBrowseBtn
            // 
            this.FilterBrowseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.FilterBrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FilterBrowseBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FilterBrowseBtn.Location = new System.Drawing.Point(325, 113);
            this.FilterBrowseBtn.Name = "FilterBrowseBtn";
            this.FilterBrowseBtn.Size = new System.Drawing.Size(60, 22);
            this.FilterBrowseBtn.TabIndex = 10;
            this.FilterBrowseBtn.Text = "Browse...";
            this.FilterBrowseBtn.UseVisualStyleBackColor = false;
            this.FilterBrowseBtn.Click += new System.EventHandler(this.FilterBrowseBtn_Click);
            // 
            // TinyVersionLabel
            // 
            this.TinyVersionLabel.AutoSize = true;
            this.TinyVersionLabel.Font = new System.Drawing.Font("Cambria", 7F);
            this.TinyVersionLabel.Location = new System.Drawing.Point(1, 1);
            this.TinyVersionLabel.Name = "TinyVersionLabel";
            this.TinyVersionLabel.Size = new System.Drawing.Size(57, 12);
            this.TinyVersionLabel.TabIndex = 11;
            this.TinyVersionLabel.Text = "placeholder";
            // 
            // AbsolutePathCheckBox
            // 
            this.AbsolutePathCheckBox.AutoSize = true;
            this.AbsolutePathCheckBox.Location = new System.Drawing.Point(13, 87);
            this.AbsolutePathCheckBox.Name = "AbsolutePathCheckBox";
            this.AbsolutePathCheckBox.Size = new System.Drawing.Size(150, 17);
            this.AbsolutePathCheckBox.TabIndex = 12;
            this.AbsolutePathCheckBox.Text = "Use Absolute Path Names";
            this.AbsolutePathCheckBox.UseVisualStyleBackColor = true;
            this.AbsolutePathCheckBox.CheckedChanged += new System.EventHandler(this.AbsolutePathCheckBox_CheckedChanged);
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(387, 170);
            this.Controls.Add(this.AbsolutePathCheckBox);
            this.Controls.Add(this.TinyVersionLabel);
            this.Controls.Add(this.FilterBrowseBtn);
            this.Controls.Add(this.BasePkgPathBtn);
            this.Controls.Add(this.OutputPathBtn);
            this.Controls.Add(this.CustomPasscodeTextBox);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.BasePkgPathTextBox);
            this.Controls.Add(this.VerboseOutputBox);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.OutputPathTextBox);
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
            _MainForm.options_page_is_open = false;
            Dispose();
        }


        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        ///--     Options Related Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        #region Options Related Functions
        private void KeystoneToggleBox_CheckedChanged(object sender, EventArgs e) => _MainForm.gp4.Keystone = KeystoneToggleBox.Checked;
        private void LimitedOutputBox_CheckedChanged(object sender, EventArgs e) => _MainForm.gp4.VerboseLogging = VerboseOutputBox.Checked;
        private void AbsolutePathCheckBox_CheckedChanged(object sender, EventArgs e) => _MainForm.gp4.AbsoluteFilePaths = AbsolutePathCheckBox.Checked;
        
        private void OutputPathTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text.Length > 0 && OutputPathTextBox.Text != _MainForm.default_strings[1]) {
                ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

                _MainForm.Gp4OutputDirectory = OutputPathTextBox.Text;
                _MainForm.text_box_changed[1] = true;
            }
        }
        private void OutputDirectoryBtn_Click(object sender, EventArgs e) {
            var Browser = new FolderBrowserDialog();

            if(Browser.ShowDialog() == DialogResult.OK)
                OutputPathTextBox.Text = Browser.SelectedPath;
        }

        private void BasePkgPathTextBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text.Length > 0 && BasePkgPathTextBox.Text != _MainForm.default_strings[2]) {
                ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

                _MainForm.gp4.SourcePkgPath = BasePkgPathTextBox.Text.Replace("\"", "");
                _MainForm.text_box_changed[2] = true;
            }
        }
        private void SourcePkgPathBtn_Click(object sender, EventArgs e) {
            var Browser = new OpenFileDialog();

            if(Browser.ShowDialog() == DialogResult.OK)
                BasePkgPathTextBox.Text = Browser.FileName;
            Browser.Dispose();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e) { // tst : eboot.bin, keystone, discname.txt; param.sfo
            if(_MainForm.text_box_changed[3] == true && ((TextBox)sender).Text == "") {
                _MainForm.gp4.BlacklistedFilesOrFolders = null;
                return;
            }

            if(((TextBox)sender).Text == "" || (FilterTextBox.Text == _MainForm.default_strings[3])) return;
            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);
            

            int filter_strings_length = 0, char_index = 0;
            StringBuilder Builder;

            foreach(char c in (FilterTextBox.Text + ';').ToCharArray())
                if(c == ';' || c == ',')
                    filter_strings_length++;

            _MainForm.gp4.BlacklistedFilesOrFolders = new string[filter_strings_length];
            var buffer = Encoding.UTF8.GetBytes((FilterTextBox.Text + ';').ToCharArray());

            try {
                for(var array_index = 0; array_index < _MainForm.gp4.BlacklistedFilesOrFolders.Length; array_index++) {
                    Builder = new StringBuilder();

                    while(buffer[char_index] != 0x3B && buffer[char_index] != 0x2C)
                        Builder.Append(Encoding.UTF8.GetString(new byte[] { buffer[char_index++] }));

                    char_index++;
                    _MainForm.gp4.BlacklistedFilesOrFolders[array_index] = Builder.ToString().Trim(' ');
                    _MainForm.text_box_changed[3] = true;
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
            if(((TextBox)sender).Text.Length > 0 && CustomPasscodeTextBox.Text != _MainForm.default_strings[4]) {
                ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);

                _MainForm.gp4.Passcode = CustomPasscodeTextBox.Text;
                _MainForm.text_box_changed[4] = true;
            }
        }
        #endregion



        ////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     Control Declarations     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\\
        #region ControlDeclarations
        private Label TinyVersionLabel;
        private Label Title;
        private Button ExitBtn;
        private Button OutputPathBtn;
        private Button BasePkgPathBtn;
        private Button FilterBrowseBtn;
        private CheckBox KeystoneToggleBox;
        private CheckBox VerboseOutputBox;
        private CheckBox AbsolutePathCheckBox;
        private TextBox OutputPathTextBox;
        private TextBox BasePkgPathTextBox;
        private TextBox FilterTextBox;
        private TextBox CustomPasscodeTextBox;
        private MainForm _MainForm;
        #endregion
    }
}
