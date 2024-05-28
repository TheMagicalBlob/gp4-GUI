﻿using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using libgp4;

namespace GP4_GUI {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            BorderFunc(this);
            AddControlEventHandlers(Controls, this);

            gp4 = new GP4Creator() {
                LoggingMethod = delegate (object str) {
                    OutputWindow.AppendLine((string)str);
                }
            };
        }


        //////////////////////\\\\\\\\\\\\\\\\\\\\\\
        ///--      DEBUG TESTING (GP4 LIB)     --\\\
        //////////////////////\\\\\\\\\\\\\\\\\\\\\\
        private CheckBox DEBUG_Patch;
        private Button gengp4TestBtn;
        private Button DEBUG_Random;
        private Button dummy;
        private Button blakisttestBtn;
        private CheckBox DEBUG_App;

        private void ClearLogBtn_Click(object sender = null, EventArgs e = null) => OutputWindow.Clear();
        private void gengp4TestBtn_Click(object sender = null, EventArgs e = null) => Process.Start(@"C:\Users\Blob\Desktop\gengp4 test.bat");
        private void DEBUG_App_Click(object sender, EventArgs e) => DEBUG_App.Checked = !(DEBUG_Patch.Checked = !DEBUG_Patch.Checked);
        private void DEBUG_Patch_Click(object sender, EventArgs e) => DEBUG_Patch.Checked = !(DEBUG_App.Checked = !DEBUG_App.Checked);
        private void DEBUG_Random_Click(object sender, EventArgs e) {
            gp4.GamedataFolder = @"D:\CUSA00744-" + (DEBUG_App.Checked ? "app" : DEBUG_Patch.Checked ? "patch" : "dingus");
            gp4.VerboseLogging = true;
            var newgp4path = gp4.CreateGP4(@"C:\Users\Blob\Desktop", true);

            var newgp4 = new GP4Reader(newgp4path);

            // instance tests \\
            newgp4.VerifyGP4();
            WLog("================== Instnce Tests Start ====================");
            var cat = newgp4.IsPatchProject;
            WLog($"Is Patch Project: {cat}");
            if (cat) WLog($"Source .pkg Path: {newgp4.BaseAppPkgPath}");


            WLog($"{newgp4.FileCount} Files");
            foreach(var f in newgp4.Files)
                WLog($"  {f}");
            WLog($"{newgp4.SubfolderCount} Subfolders");
            foreach(var s in newgp4.Subfolders)
                WLog($"  {s}");
            WLog($"{newgp4.SubfolderCount} Subfolders");
            foreach(var sn in newgp4.SubfolderNames)
                WLog($"  {sn}");


            WLog($"{newgp4.ChunkCount} Chunks");
            foreach(var c in newgp4.Chunks)
                WLog($"  {c}");
            WLog($"{newgp4.ScenarioCount} Scenarios");
            WLog($"(Default Scenario: {newgp4.DefaultScenarioId})");
            foreach(var sc in newgp4.Scenarios)
                WLog($"Scenario {sc.Id}: Label={sc.Label} Type={sc.Type} InitialChunkCount:{sc.InitialChunkCount} Range={sc.ChunkRange}");


            WLog($"Content Id: {newgp4.ContentID}");
            WLog($"Passcode: {newgp4.Passcode}");
            WLog($".sfo Timestamp: {newgp4.Timestamp}");
            WLog("================== Instnce Tests End ====================\n\n");
            //================\\


            // static tests \\
            WLog("================== Static Tests Start ====================");
            string[] files, subfolders;
            var _cat = GP4Reader.IsPatchPackage(newgp4path);
            WLog($"Is Patch Project: {_cat}");
            if(_cat) WLog($"Source .pkg Path: {GP4Reader.GetBasePkgPath(newgp4path)}");

            WLog($"{(files = GP4Reader.GetFileListing(newgp4path)).Length} Files");
            foreach(var f in files)
                WLog($"  {f}");
            WLog($"{(subfolders = GP4Reader.GetFolderListing(newgp4path)).Length} Subfolders");
            foreach(var s in subfolders)
                WLog($"  {s}");
            foreach(var sn in GP4Reader.GetFolderNames(newgp4path))
                WLog($"  {sn}");


            WLog($"{GP4Reader.GetChunkCount(newgp4path)} Chunks");
            foreach(var c in GP4Reader.GetChunkListing(newgp4path))
                WLog($"  {c}");
            WLog($"{GP4Reader.GetScenarioCount(newgp4path)} Scenarios");
            WLog($"(Default Scenario: {GP4Reader.GetDefaultScenarioId(newgp4path)})");
            foreach(var sc in GP4Reader.GetScenarioListing(newgp4path))
                WLog($"Scenario {sc.Id}: Label={sc.Label} Type={sc.Type} InitialChunkCount:{sc.InitialChunkCount} Range={sc.ChunkRange}");


            WLog($"Content Id: {GP4Reader.GetContentId(newgp4path)}");
            WLog($"Passcode: {GP4Reader.GetPkgPasscode(newgp4path)}");
            WLog($".sfo Timestamp: {GP4Reader.GetTimestamp(newgp4path)}");
            WLog("================== Static Tests End ====================");
            //===============\\

            System.Diagnostics.Process.Start(newgp4path);
        }
        //============================\\



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
            this.CreateBtn = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.DisableLogBox = new System.Windows.Forms.CheckBox();
            this.OptionsBtn = new System.Windows.Forms.Button();
            this.ClearLogBtn = new System.Windows.Forms.Button();
            this.DEBUG_Patch = new System.Windows.Forms.CheckBox();
            this.DEBUG_App = new System.Windows.Forms.CheckBox();
            this.gengp4TestBtn = new System.Windows.Forms.Button();
            this.DEBUG_Random = new System.Windows.Forms.Button();
            this.dummy = new System.Windows.Forms.Button();
            this.blakisttestBtn = new System.Windows.Forms.Button();
            this.OutputWindow = new GP4_GUI.RichTextBox();
            this.GamedataFolderPathBox = new GP4_GUI.TextBox();
            this.SuspendLayout();
            // 
            // CreateBtn
            // 
            this.CreateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.CreateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CreateBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CreateBtn.Location = new System.Drawing.Point(370, 58);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateBtn.TabIndex = 3;
            this.CreateBtn.Text = "Build .gp4";
            this.CreateBtn.UseVisualStyleBackColor = false;
            this.CreateBtn.Click += new System.EventHandler(this.BuildProjectFile);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Gadugi", 8.5F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(153, 6);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(141, 16);
            this.Title.TabIndex = 0;
            this.Title.Text = ".gp4 Project File Creator";
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.MinimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MinimizeBtn.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MinimizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MinimizeBtn.Location = new System.Drawing.Point(406, 2);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(22, 22);
            this.MinimizeBtn.TabIndex = 4;
            this.MinimizeBtn.Text = "-";
            this.MinimizeBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.MinimizeBtn.UseVisualStyleBackColor = false;
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExitBtn.Location = new System.Drawing.Point(428, 2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 5;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = false;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.BrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BrowseBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BrowseBtn.Location = new System.Drawing.Point(305, 58);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(60, 22);
            this.BrowseBtn.TabIndex = 7;
            this.BrowseBtn.Text = "Browse...";
            this.BrowseBtn.UseVisualStyleBackColor = false;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // DisableLogBox
            // 
            this.DisableLogBox.AutoSize = true;
            this.DisableLogBox.Location = new System.Drawing.Point(3, 59);
            this.DisableLogBox.Name = "DisableLogBox";
            this.DisableLogBox.Size = new System.Drawing.Size(82, 17);
            this.DisableLogBox.TabIndex = 8;
            this.DisableLogBox.Text = "Disable Log";
            this.DisableLogBox.UseVisualStyleBackColor = true;
            // 
            // OptionsBtn
            // 
            this.OptionsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.OptionsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OptionsBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OptionsBtn.Location = new System.Drawing.Point(8, 4);
            this.OptionsBtn.Name = "OptionsBtn";
            this.OptionsBtn.Size = new System.Drawing.Size(75, 23);
            this.OptionsBtn.TabIndex = 9;
            this.OptionsBtn.Text = "Tool Options";
            this.OptionsBtn.UseVisualStyleBackColor = false;
            this.OptionsBtn.Click += new System.EventHandler(this.OptionsBtn_Click);
            // 
            // ClearLogBtn
            // 
            this.ClearLogBtn.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClearLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearLogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearLogBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.ClearLogBtn.Location = new System.Drawing.Point(87, 5);
            this.ClearLogBtn.Name = "ClearLogBtn";
            this.ClearLogBtn.Size = new System.Drawing.Size(15, 16);
            this.ClearLogBtn.TabIndex = 15;
            this.ClearLogBtn.Text = "C";
            this.ClearLogBtn.UseVisualStyleBackColor = false;
            this.ClearLogBtn.Click += new System.EventHandler(this.ClearLogBtn_Click);
            // 
            // DEBUG_Patch
            // 
            this.DEBUG_Patch.AutoSize = true;
            this.DEBUG_Patch.Location = new System.Drawing.Point(171, 60);
            this.DEBUG_Patch.Name = "DEBUG_Patch";
            this.DEBUG_Patch.Size = new System.Drawing.Size(54, 17);
            this.DEBUG_Patch.TabIndex = 16;
            this.DEBUG_Patch.Text = "Patch";
            this.DEBUG_Patch.UseVisualStyleBackColor = true;
            this.DEBUG_Patch.Click += new System.EventHandler(this.DEBUG_Patch_Click);
            // 
            // DEBUG_App
            // 
            this.DEBUG_App.AutoSize = true;
            this.DEBUG_App.Checked = true;
            this.DEBUG_App.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DEBUG_App.Location = new System.Drawing.Point(127, 60);
            this.DEBUG_App.Name = "DEBUG_App";
            this.DEBUG_App.Size = new System.Drawing.Size(45, 17);
            this.DEBUG_App.TabIndex = 17;
            this.DEBUG_App.Text = "App";
            this.DEBUG_App.UseVisualStyleBackColor = true;
            this.DEBUG_App.Click += new System.EventHandler(this.DEBUG_App_Click);
            // 
            // gengp4TestBtn
            // 
            this.gengp4TestBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.gengp4TestBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gengp4TestBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.gengp4TestBtn.Location = new System.Drawing.Point(127, 79);
            this.gengp4TestBtn.Name = "gengp4TestBtn";
            this.gengp4TestBtn.Size = new System.Drawing.Size(90, 19);
            this.gengp4TestBtn.TabIndex = 15;
            this.gengp4TestBtn.Text = "createOrbisGP4";
            this.gengp4TestBtn.UseVisualStyleBackColor = false;
            this.gengp4TestBtn.Click += new System.EventHandler(this.gengp4TestBtn_Click);
            // 
            // DEBUG_Random
            // 
            this.DEBUG_Random.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.DEBUG_Random.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DEBUG_Random.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DEBUG_Random.Location = new System.Drawing.Point(262, 59);
            this.DEBUG_Random.Name = "DEBUG_Random";
            this.DEBUG_Random.Size = new System.Drawing.Size(32, 22);
            this.DEBUG_Random.TabIndex = 18;
            this.DEBUG_Random.Text = "test";
            this.DEBUG_Random.UseVisualStyleBackColor = false;
            this.DEBUG_Random.Click += new System.EventHandler(this.DEBUG_Random_Click);
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
            // blakisttestBtn
            // 
            this.blakisttestBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.blakisttestBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.blakisttestBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.blakisttestBtn.Location = new System.Drawing.Point(383, 81);
            this.blakisttestBtn.Name = "blakisttestBtn";
            this.blakisttestBtn.Size = new System.Drawing.Size(50, 22);
            this.blakisttestBtn.TabIndex = 19;
            this.blakisttestBtn.Text = "chk blk";
            this.blakisttestBtn.UseVisualStyleBackColor = false;
            this.blakisttestBtn.Click += new System.EventHandler(this.testBlacklist);
            // 
            // OutputWindow
            // 
            this.OutputWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.OutputWindow.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F);
            this.OutputWindow.ForeColor = System.Drawing.SystemColors.Window;
            this.OutputWindow.Location = new System.Drawing.Point(4, 103);
            this.OutputWindow.MaxLength = 21474836;
            this.OutputWindow.Name = "OutputWindow";
            this.OutputWindow.ReadOnly = true;
            this.OutputWindow.Size = new System.Drawing.Size(444, 241);
            this.OutputWindow.TabIndex = 6;
            this.OutputWindow.Text = "";
            // 
            // GamedataFolderPathBox
            // 
            this.GamedataFolderPathBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamedataFolderPathBox.Location = new System.Drawing.Point(8, 34);
            this.GamedataFolderPathBox.Name = "GamedataFolderPathBox";
            this.GamedataFolderPathBox.Size = new System.Drawing.Size(437, 21);
            this.GamedataFolderPathBox.TabIndex = 2;
            this.GamedataFolderPathBox.Text = "Paste The Gamedata Folder Path Here, Or Use The Browse Button...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(452, 349);
            this.Controls.Add(this.blakisttestBtn);
            this.Controls.Add(this.dummy);
            this.Controls.Add(this.DEBUG_Random);
            this.Controls.Add(this.DEBUG_App);
            this.Controls.Add(this.DEBUG_Patch);
            this.Controls.Add(this.gengp4TestBtn);
            this.Controls.Add(this.ClearLogBtn);
            this.Controls.Add(this.OptionsBtn);
            this.Controls.Add(this.DisableLogBox);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.OutputWindow);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.MinimizeBtn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.GamedataFolderPathBox);
            this.Controls.Add(this.CreateBtn);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "GP4 Project Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        ///--     Basic Form Init Functions     --\\\
        ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
        #region Basic Form Init Functions
        public void BorderFunc(Form form) {
            var MainBox = new GroupBox() {
                Name = "MainBox",
                Location = new Point(0, -6),
                Size = new Size(form.Size.Width, form.Size.Height + 7),
            };

            form.Controls.Add(MainBox);
        }

        public void AddControlEventHandlers(Control.ControlCollection Controls, Form form) {
            form.MouseDown += new MouseEventHandler(MouseDownFunc);
            form.MouseUp += new MouseEventHandler(MouseUpFunc);
            form.MouseMove += new MouseEventHandler(MoveForm);

            foreach(Control Item in Controls) {
                if(Item.HasChildren) { // Designer Added Some Things To The Form, And Some To The Group Box Used To Make The Border. This is me bing lazy. as long as it's not noticably slower
                    foreach(Control Child in Item.Controls) {

                        Child.MouseDown += new MouseEventHandler(MouseDownFunc);
                        Child.MouseUp += new MouseEventHandler(MouseUpFunc);

                        if(!Child.Name.Contains("TextBox") && !Child.Name.Contains("OutputWindow")) // So You Can Drag Select The Text Lol
                            Child.MouseMove += new MouseEventHandler(MoveForm);
                    }
                }
                Item.MouseDown += new MouseEventHandler(MouseDownFunc);
                Item.MouseUp += new MouseEventHandler(MouseUpFunc);
                if(!Item.Name.Contains("TextBox") && !Item.Name.Contains("OutputWindow")) // So You Can Drag Select The Text Lol
                    Item.MouseMove += new MouseEventHandler(MoveForm);
            }
            try {
                Controls.Find("MinimizeBtn", true)[0].Click += new EventHandler(MinimizeBtn_Click);
                Controls.Find("MinimizeBtn", true)[0].MouseEnter += new EventHandler(MinimizeBtnMH);
                Controls.Find("MinimizeBtn", true)[0].MouseLeave += new EventHandler(ExitBtnML);
                Controls.Find("ExitBtn", true)[0].Click += new EventHandler(ExitBtn_Click);
                Controls.Find("ExitBtn", true)[0].MouseEnter += new EventHandler(ExitBtnMH);
                Controls.Find("ExitBtn", true)[0].MouseLeave += new EventHandler(ExitBtnML);
                Controls.Find("MainBox", true)[0].MouseDown += new MouseEventHandler(MouseDownFunc);
                Controls.Find("MainBox", true)[0].MouseUp += new MouseEventHandler(MouseUpFunc);
                Controls.Find("MainBox", true)[0].MouseMove += new MouseEventHandler(MoveForm);
            }
            catch(IndexOutOfRangeException) { }
        }

        private static void MinimizeBtn_Click(object sender, EventArgs e) => ActiveForm.WindowState = FormWindowState.Minimized;
        private static void ExitBtn_Click(object sender, EventArgs e) => Environment.Exit(0);
        public static void ExitBtnMH(object sender, EventArgs e) => ((Control)sender).ForeColor = Color.FromArgb(230, 100, 100);
        public static void MinimizeBtnMH(object sender, EventArgs e) => ((Control)sender).ForeColor = Color.FromArgb(90, 100, 255);
        public static void ExitBtnML(object sender, EventArgs e) => ((Control)sender).ForeColor = Color.FromArgb(0, 0, 0);


        public void MouseUpFunc(object sender, MouseEventArgs e) {
            mouse_is_down = 0;
            Options?.BringToFront();
        }
        public void MouseDownFunc(object sender, MouseEventArgs e) {
            MouseDif = new Point(MousePosition.X - ActiveForm.Location.X, MousePosition.Y - ActiveForm.Location.Y);
            mouse_is_down = 1;
        }
        public void MoveForm(object sender, MouseEventArgs e) {
            if(mouse_is_down != 0) {
                ActiveForm.Location = new Point(MousePosition.X - MouseDif.X, MousePosition.Y - MouseDif.Y);
                ActiveForm.Update();
                if(Options == null) return;
                Options.Location = new Point(MousePosition.X - MouseDif.X + 30, MousePosition.Y - MouseDif.Y + 60);
                Options.Update();
            }
        }
        #endregion


        /////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     Application Variables     --\\\
        /////////////////////\\\\\\\\\\\\\\\\\\\\
        #region Application Variables
        public int mouse_is_down = 0;
        public bool options_page_is_open, limit_output;
        public string Gp4OutputDirectory;
        public Point MouseDif;
        private Form Options;
        public GP4Creator gp4;
        #endregion


        ////////////////////\\\\\\\\\\\\\\\\\\\
        ///--     Main Form Functions     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\
        #region Main Form Functions
        public void WLog(object str = null) => OutputWindow.AppendLine(str.ToString());

        public static void DLog(string str = "") {
#if DEBUG
            try {
                Debug.WriteLine(str);
            }
            catch(Exception){}

            try {
                Console.WriteLine(str);
            }
            catch(Exception){}
#endif
        }


        /// <summary>
        /// Create Page For Changing Various .gp4 Options. <br/>(passcode, source pkg, etc)
        /// </summary>
        private void OptionsBtn_Click(object sender, EventArgs e) {
            if(options_page_is_open) return;
            Options?.BringToFront();

            var NewPage = new OptionsPage(this, Location);
            Options = NewPage;
            NewPage.Show();
        }


        /// <summary> Open Windows' Ghastly File Browser Dialog To Search For The Gamedata Folder
        /// </summary>
        private void BrowseBtn_Click(object sender, EventArgs e) {
            using(var Browser = new FolderBrowserDialog())
                if(Browser.ShowDialog() == DialogResult.OK)
                    GamedataFolderPathBox.Text = Browser.SelectedPath;
        }


        private void BuildProjectFile(object sender, EventArgs e) {
            if(GamedataFolderPathBox.IsDefault) {
                WLog("Please Assign A Valid Gamedata Folder Before Building");
                return;
            }
            else if (!Directory.Exists(gp4.GamedataFolder = GamedataFolderPathBox.Text.Replace("\"", string.Empty))) {
                WLog("The Directory Application Folder Provided Could Not Be Found.");

                if (File.Exists(gp4.GamedataFolder))
                    WLog($"(Path: {gp4.GamedataFolder} Leads To A File, Not A Folder)");
                else
                    WLog($"(Folder: {gp4.GamedataFolder} Does Not Exist)");
                return;
            }


            // Assign An Output Directory For The .gp4 If None Has Been Set Yet.
            if(Gp4OutputDirectory == null)
                if(!gp4.AbsoluteFilePaths)
                    Gp4OutputDirectory = gp4.GamedataFolder;
                else
                    Gp4OutputDirectory = gp4.GamedataFolder.Remove(gp4.GamedataFolder.LastIndexOf('\\'));

            gp4.CreateGP4(Gp4OutputDirectory, true);
        }
        #endregion


        ////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     Control Declarations     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\\
        #region ControlDeclarations
        private Button CreateBtn;
        private Button ClearLogBtn;
        private TextBox GamedataFolderPathBox;
        private Label Title;
        private Button MinimizeBtn;
        private Button ExitBtn;
        private RichTextBox OutputWindow;
        private Button BrowseBtn;
        private CheckBox DisableLogBox;
        private Button OptionsBtn;
        #endregion

        private void testBlacklist(object sender, EventArgs e) {
            foreach(var b in gp4.BlacklistedFilesOrFolders) {
                WLog(b);
            }
        }

        private Button DesignerManip; // Manipulate Designer Stupidity (Stop Creating Methods Inside Existing Code, You Fucking Moron)
    }


    public class RichTextBox : System.Windows.Forms.RichTextBox {

        /// <summary> Appends Text to The Currrent Text of A Text Box, Followed By The Standard Line Terminator.<br/>Scrolls To Keep The Newest Line In View. </summary>
        /// <param name="str"> The String To Output. </param>
        public void AppendLine(string str = "") {
            if(str.Length <= 0) AppendText("\n");

            else AppendText($"{str}\n");

            ScrollToCaret();
        }
    }

    public class TextBox : System.Windows.Forms.TextBox {
        public TextBox() {
            IsDefault = true;

            TextChanged += Set;

            GotFocus += delegate (object _, EventArgs __) {
                if(IsDefault) {
                    Font = new Font("Microsoft YaHei UI", 8.25F);
                    Clear();
                    IsDefault = false;
                }
            };

            Click += delegate (object _, EventArgs __) { // Just In Case, I Suppose.
                if(IsDefault) {
                    Font = new Font("Microsoft YaHei UI", 8.25F);
                    Clear();
                    IsDefault = false;
                }
            };

            LostFocus += delegate (object _, EventArgs __) {
                if(Text.Length <= 0 || Text.Trim().Length <= 0) {
                    Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Italic);
                    Text = DefaultText;
                    IsDefault = true;
                }
            };
        }


        private string DefaultText;
        public bool IsDefault { get; private set; }


        /// <summary> Yoink Default Text From First Text Assignment.
        ///</summary>
        void Set(object s, EventArgs e) {
            DefaultText = Text;

            TextChanged -= Set;
            TextChanged += delegate (object control, EventArgs _) {
                if(IsDefault && Text.Length > 0) {
                    Font = new Font("Microsoft YaHei UI", 8.25F);
                    IsDefault = false;
                }
            };
        }
    }
}
