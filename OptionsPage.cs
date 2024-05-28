using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GP4_GUI {

    public partial class OptionsPage : Form {

        public OptionsPage(MainForm MainForm, Point LastPos) {
            Venat = MainForm;

            InitializeComponent();
            MainForm.BorderFunc(this);
            MainForm.options_page_is_open = true;

            Location = new Point(LastPos.X + 30, LastPos.Y + 60);
            TinyVersionLabel.Text = Version;

#region Load Options
            // Restore Edited User Settings To Controls
            if(MainForm.Gp4OutputDirectory != null)
                OutputPathTextBox.Text = MainForm.Gp4OutputDirectory;

            if(MainForm.gp4.BasePkgPath != null)
                BasePkgPathTextBox.Text = MainForm.gp4.BasePkgPath;

            AbsolutePathCheckBox.Checked = MainForm.gp4.AbsoluteFilePaths;
            KeystoneToggleBox.Checked = MainForm.gp4.Keystone;
            VerboseOutputBox.Checked = MainForm.gp4.VerboseLogging;

            if(MainForm.gp4.BlacklistedFilesOrFolders != null) {
                var str = MainForm.gp4.BlacklistedFilesOrFolders[0];
                for(var i = 1; i< MainForm.gp4.BlacklistedFilesOrFolders.Length; i++)
                    str += $",{MainForm.gp4.BlacklistedFilesOrFolders[i]}";

                FilterTextBox.Text = str;
            }


            if(MainForm.gp4.Passcode != "00000000000000000000000000000000")
                CustomPasscodeTextBox.Text = MainForm.gp4.Passcode;
#endregion
        }

        private Button dummy;


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
            this.Title = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.VerboseOutputBox = new System.Windows.Forms.CheckBox();
            this.OutputPathBtn = new System.Windows.Forms.Button();
            this.BasePkgPathBtn = new System.Windows.Forms.Button();
            this.FilterBrowseBtn = new System.Windows.Forms.Button();
            this.TinyVersionLabel = new System.Windows.Forms.Label();
            this.AbsolutePathCheckBox = new System.Windows.Forms.CheckBox();
            this.dummy = new System.Windows.Forms.Button();
            this.CustomPasscodeTextBox = new GP4_GUI.TextBox();
            this.FilterTextBox = new GP4_GUI.TextBox();
            this.BasePkgPathTextBox = new GP4_GUI.TextBox();
            this.OutputPathTextBox = new GP4_GUI.TextBox();
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
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Gadugi", 8.5F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(162, 4);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(50, 16);
            this.Title.TabIndex = 0;
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
            this.OutputPathBtn.Click += new System.EventHandler(this.OutputPathBtn_Click);
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
            this.BasePkgPathBtn.Click += new System.EventHandler(this.BasePkgPathBtn_Click);
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
            this.TinyVersionLabel.TabIndex = 0;
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
            // dummy
            // 
            this.dummy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dummy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dummy.Font = new System.Drawing.Font("Microsoft Sans Serif", 0.1F);
            this.dummy.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dummy.Location = new System.Drawing.Point(0, 0);
            this.dummy.Name = "dummy";
            this.dummy.Size = new System.Drawing.Size(0, 0);
            this.dummy.TabIndex = 0;
            this.dummy.UseVisualStyleBackColor = false;
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
            this.CustomPasscodeTextBox.LostFocus += new System.EventHandler(this.CustomPasscodeTextBox_FocusChanged);
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
            this.FilterTextBox.LostFocus += new System.EventHandler(this.FilterTextBox_FocusLeft);
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
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(387, 170);
            this.Controls.Add(this.dummy);
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
            Venat.options_page_is_open = false;
            Dispose();
        }


        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        ///--     Options Related Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        #region Options Related Functions
        private void KeystoneToggleBox_CheckedChanged(object sender, EventArgs e) => Venat.gp4.Keystone = ((CheckBox)sender).Checked;
        private void LimitedOutputBox_CheckedChanged(object sender, EventArgs e) => Venat.gp4.VerboseLogging = ((CheckBox)sender).Checked;
        private void AbsolutePathCheckBox_CheckedChanged(object sender, EventArgs e) => Venat.gp4.AbsoluteFilePaths = ((CheckBox)sender).Checked;
        private void OutputPathTextBox_TextChanged(object sender, EventArgs e) => Venat.Gp4OutputDirectory = ((Control)sender).Text;
        private void CustomPasscodeTextBox_TextChanged(object sender, EventArgs e) => Venat.gp4.Passcode = ((Control)sender).Text;

        private void OutputPathBtn_Click(object sender, EventArgs e) {
            using(var Browser = new FolderBrowserDialog())
                if(Browser.ShowDialog() == DialogResult.OK)
                    OutputPathTextBox.Text = Browser.SelectedPath;
        }

        private void BasePkgPathTextBox_TextChanged(object sender, EventArgs e) => Venat.gp4.BasePkgPath = ((Control)sender).Text;

        private void BasePkgPathBtn_Click(object sender, EventArgs e) {
            using(var Browser = new OpenFileDialog())
                if(Browser.ShowDialog() == DialogResult.OK)
                    BasePkgPathTextBox.Text = Browser.FileName;
        }

        /// <summary>
        /// Remove Trailing Seperators To Avoid Improperly Counting Filtered Items In The Method Below.
        /// </summary>
        private void FilterTextBox_FocusLeft(object control, EventArgs _) {
            var textbox = ((TextBox)control);
            if (!textbox.IsDefault)
                textbox.Text = textbox.Text.TrimEnd(',', ';');
        }

        /// <summary>
        /// Parse Individual Items From Filter Text Box, And Add Them To The Blacklist
        /// </summary>
        private void FilterTextBox_TextChanged(object sender, EventArgs _) { // tst : eboot.bin, keystone, discname.txt; param.sfo
            TextBox Sender;
            if((Sender = ((TextBox)sender)).IsDefault) {
                Venat.gp4.BlacklistedFilesOrFolders = null;
                return;
            }

            StringBuilder Builder;


            // Get Amount Of Filtered Files/Paths
            var filter_strings_length = 1;
            foreach(var c in (FilterTextBox.Text).ToCharArray())
                if(c == ';' || c == ',')
                    filter_strings_length++;


            Venat.gp4.BlacklistedFilesOrFolders = new string[filter_strings_length];

            var buffer = Encoding.UTF8.GetBytes((FilterTextBox.Text + ';').ToCharArray());

            try {
                for(int array_index = 0, char_index = 0; array_index < Venat.gp4.BlacklistedFilesOrFolders.Length; array_index++) {
                    Builder = new StringBuilder();

                    while(buffer[char_index] != 0x3B && buffer[char_index] != 0x2C)
                        Builder.Append(Encoding.UTF8.GetString(new byte[] { buffer[char_index++] }));

                    char_index++;
                    Venat.gp4.BlacklistedFilesOrFolders[array_index] = Builder.ToString().Trim(' ');
                }
            }
            catch (IndexOutOfRangeException ex) {
                Venat.WLog($"\n{ex.StackTrace}");
#if DEBUG
                MainForm.DLog($"\n{ex.StackTrace}");
#endif
            }
        }

        private void FilterBrowseBtn_Click(object sender, EventArgs e) {
            var Browser = new OpenFileDialog();
            Browser.Multiselect = true;

            if(Browser.ShowDialog() == DialogResult.OK) {
                FilterTextBox.Clear();
                foreach(string file in Browser.FileNames)
                    FilterTextBox.Text += $"{file}, ";
            }
            Browser.Dispose();
        }

        private void CustomPasscodeTextBox_FocusChanged(object sender, EventArgs e) {
            var Sender = ((TextBox)sender);
            if(!Sender.IsDefault)
                Venat.gp4.Passcode = Sender.Text;
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
        private readonly MainForm Venat;
        private TextBox OutputPathTextBox;
        private TextBox BasePkgPathTextBox;
        private TextBox FilterTextBox;
        private TextBox CustomPasscodeTextBox;
        #endregion
    }
}
