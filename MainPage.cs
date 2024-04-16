using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using libgp4;
using System.Collections.Generic;
using System.Reflection;


namespace GP4_GUI {

    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            BorderFunc(this);
            AddControlEventHandlers(Controls, this);

            // Designer Will Delete This From InitializeComponent If Added Manually
            AppFolderPathTextBox.MouseClick += TextBoxReady;
            AppFolderPathTextBox.LostFocus  += TextBoxReset;

            GP4DirCheck();

            gp4 = new GP4Creator("");
        }
        public const string Version = "ver 1.17.53 ";


        private class rTextBox : TextBox {
            public rTextBox() {

                IsDefault = true;
                TextChanged += SetDefaultText;
            }
            private void SetDefaultText(object sender, EventArgs e) => DefaultText = Text;
            public void Reset() { IsDefault = true; Text = DefaultText; }

            public string DefaultText;
            public bool IsDefault;
        }



        // Testing Options For .gp4 Library
        #if DEBUG
        private static string basegp4path = @"C:\Users\Blob\Desktop\t\CUSA00341";
        private static readonly string[] ext = new string[] { "-app.gp4", "-patch.gp4" };
        private static string testgp4path = @"C:\Users\Blob\Desktop\t\CUSA00341-patch.gp4";

        private static int extIndex = 1

       ;private Button AppOrPatchBtn;
        private Button clearLogBtn;
        private Button CheckintegrityBtn;
        private void GP4DirCheck() {
            if(!File.Exists(testgp4path)) {
                foreach(var f in Directory.GetFiles(Directory.GetCurrentDirectory()))
                    if(f.Contains(".gp4") && f.Contains(ext[extIndex])) {
                        testgp4path = f;
                        basegp4path = f.Remove(f.LastIndexOf('-'));
                        DLog($"Found .gp4 At: {testgp4path}\n(New Base: {basegp4path})");
                    }
                    else DLog($".gp4 File Not Found ({ext[extIndex]})"); 
            }
        }

        private void AppOrPatchBtn_Click(object sender, EventArgs e) {
            ((Control)sender).Text = (testgp4path = basegp4path + ext[extIndex ^= 1]).Substring(testgp4path.LastIndexOf("\\") + 10);
            ClearLogBtn_Click();
        }

        private void ClearLogBtn_Click(object sender = null, EventArgs e = null) => OutputWindow.Clear();
        private void CheckintegrityBtn_Click(object sender, EventArgs e) => WLog(new GP4Reader(testgp4path).CheckGP4Integrity());

        private void DButton1_Click(object sender, EventArgs e) {

            WLog($"f.Passcode: {GP4Reader.GetPkgPasscode(testgp4path)}");
            WLog("--");


            if(GP4Reader.IsPatchPackage(testgp4path)) {
                WLog("App Is A Patch Project");
                WLog(GP4Reader.GetBasePkgPath(@"D:\PS4\CUSA00341-patch.gp4"));
            }
            else
                WLog("App Is A Base Game Package");
            WLog("--");

            // Chunks
            var ScenarioCount = GP4Reader.GetScenarioCount(testgp4path);
            if(ScenarioCount != null)
               WLog($" Scenario Count: {ScenarioCount}");

            else
                WLog($"Scenarios: Null");
            WLog("--");
            //\\

            // Chunks
            var ChunkCount = GP4Reader.GetChunkCount(testgp4path);
            if(ChunkCount != null)
                WLog($" Chunk Count: {ChunkCount}");

            else
                WLog($"Chunks: Null");
            WLog("--");
            //\\

            // Chunks
            var Chunks = GP4Reader.GetChunkListing(testgp4path);
            if(Chunks != null)
                foreach(var i in Chunks)
                    WLog($" Chunks: {i}");

            else
                WLog($"Chunks: Null");
            WLog("--");
            //\\

            // Scenarios
            var Scenarios = GP4Reader.GetScenarioListing(testgp4path);
            if(Scenarios != null)
                foreach(var i in Scenarios)
                    WLog($" Scenarios: {i.Id} {i.Type} {i.InitialChunkCount} {i.Label} {i.ChunkRange}");

            else
                WLog($"Scenarios: Null");
            WLog("--");
            //\\


            // Files
            var Files = GP4Reader.GetFileListing(testgp4path);
            if(Files != null)
                foreach(var i in Files)
                    WLog($" files: {i}");

            else
                WLog($"files: Null");
            WLog("--");
            //\\


            // Folders
            var Folders = GP4Reader.GetFolderListing(testgp4path);
            if(Folders.Length > 0)
                foreach(var i in Folders)
                    WLog($" folders: {i}");

            else
                WLog($"folders: Null");
            WLog("--");
            //\\


            // Folder Namwes
            var FolderNames = GP4Reader.GetFolderNames(testgp4path);
            if(FolderNames != null)
                foreach(var i in FolderNames)
                    WLog($" folder names: {i}");

            else
                WLog($"folder names: Null");
            WLog("--");
            //\\


            Update();
        }
        private void DButton2_Click(object sender, EventArgs e) {
            var f = new GP4Reader(testgp4path);
            
            WLog($"f.Passcode: {f.Passcode}");
            WLog("--");

            if(f.IsPatchProject) {
                WLog("App Is A Patch Project");
                WLog(f.BaseAppPkgPath);
            }
            else
                WLog("App Is A Base Game Package");
            WLog("--");
            
            // Chunks
            if(f.Chunks != null)
                foreach(var i in f.Chunks)
                    WLog($" Chunks: {i}");

            else
                WLog($"Chunks: Null");
            WLog("--");
            //\\


            // Scenarios
            if(f.Scenarios != null)
                foreach(var i in f.Scenarios)
                    WLog($" Scenarios: {i.Id} {i.Type} {i.InitialChunkCount} {i.Label} {i.ChunkRange}");

            else
                WLog($"Scenarios: Null");
            WLog("--");
            //\\


            // Folders
            if(f.Files != null)
                foreach(var i in f.Files)
                    WLog($" files: {i}");

            else
                WLog($"files: Null");
            WLog("--");
            //\\



            // Folders
            if(f.Subfolders != null)
                foreach(var i in f.Subfolders)
                    WLog($" folders: {i}");
            
                else
                WLog($"folders: Null");
            WLog("--");
            //\\
            Update();
        }

        #endif


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
            this.AppFolderPathTextBox = new GP4_GUI.MainForm.rTextBox();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.OutputWindow = new System.Windows.Forms.RichTextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.DisableLogBox = new System.Windows.Forms.CheckBox();
            this.OptionsBtn = new System.Windows.Forms.Button();
            this.CheckintegrityBtn = new System.Windows.Forms.Button();
            this.AppOrPatchBtn = new System.Windows.Forms.Button();
            this.clearLogBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateBtn
            // 
            this.CreateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.CreateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CreateBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CreateBtn.Location = new System.Drawing.Point(371, 58);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateBtn.TabIndex = 0;
            this.CreateBtn.Text = "Build .gp4";
            this.CreateBtn.UseVisualStyleBackColor = false;
            this.CreateBtn.Click += new System.EventHandler(this.BuildProjectFile);
            // 
            // AppFolderPathTextBox
            // 
            this.AppFolderPathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppFolderPathTextBox.Location = new System.Drawing.Point(8, 33);
            this.AppFolderPathTextBox.Name = "AppFolderPathTextBox";
            this.AppFolderPathTextBox.Size = new System.Drawing.Size(437, 21);
            this.AppFolderPathTextBox.TabIndex = 0;
            this.AppFolderPathTextBox.Text = "Paste The Gamedata Folder Path Here, Or Use The Browse Button...";
            this.AppFolderPathTextBox.TextChanged += new System.EventHandler(this.AppFolderPathBox_TextChanged);
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
            this.MinimizeBtn.Location = new System.Drawing.Point(401, 6);
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
            this.ExitBtn.Location = new System.Drawing.Point(423, 6);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 5;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = false;
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
            this.DisableLogBox.Location = new System.Drawing.Point(8, 58);
            this.DisableLogBox.Name = "DisableLogBox";
            this.DisableLogBox.Size = new System.Drawing.Size(82, 17);
            this.DisableLogBox.TabIndex = 8;
            this.DisableLogBox.Text = "Disable Log";
            this.DisableLogBox.UseVisualStyleBackColor = true;
            // 
            // OptionsBtn
            // 
            this.OptionsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.OptionsBtn.Enabled = false;
            this.OptionsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OptionsBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OptionsBtn.Location = new System.Drawing.Point(8, 6);
            this.OptionsBtn.Name = "OptionsBtn";
            this.OptionsBtn.Size = new System.Drawing.Size(75, 23);
            this.OptionsBtn.TabIndex = 9;
            this.OptionsBtn.Text = "Tool Options";
            this.OptionsBtn.UseVisualStyleBackColor = false;
            this.OptionsBtn.Click += new System.EventHandler(this.OptionsBtn_Click);
            // 
            // CheckintegrityBtn
            // 
            this.CheckintegrityBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.CheckintegrityBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CheckintegrityBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CheckintegrityBtn.Location = new System.Drawing.Point(15, 75);
            this.CheckintegrityBtn.Name = "CheckintegrityBtn";
            this.CheckintegrityBtn.Size = new System.Drawing.Size(42, 22);
            this.CheckintegrityBtn.TabIndex = 13;
            this.CheckintegrityBtn.Text = "Verify";
            this.CheckintegrityBtn.UseVisualStyleBackColor = false;
            this.CheckintegrityBtn.Click += new System.EventHandler(this.CheckintegrityBtn_Click);
            // 
            // AppOrPatchBtn
            // 
            this.AppOrPatchBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.AppOrPatchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AppOrPatchBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.AppOrPatchBtn.Location = new System.Drawing.Point(74, 75);
            this.AppOrPatchBtn.Name = "AppOrPatchBtn";
            this.AppOrPatchBtn.Size = new System.Drawing.Size(66, 22);
            this.AppOrPatchBtn.TabIndex = 14;
            this.AppOrPatchBtn.Text = "-patch.gp4";
            this.AppOrPatchBtn.UseVisualStyleBackColor = false;
            this.AppOrPatchBtn.Click += new System.EventHandler(this.AppOrPatchBtn_Click);
            // 
            // clearLogBtn
            // 
            this.clearLogBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(232)))));
            this.clearLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearLogBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.clearLogBtn.Location = new System.Drawing.Point(154, 75);
            this.clearLogBtn.Name = "clearLogBtn";
            this.clearLogBtn.Size = new System.Drawing.Size(46, 22);
            this.clearLogBtn.TabIndex = 15;
            this.clearLogBtn.Text = "WIPE";
            this.clearLogBtn.UseVisualStyleBackColor = false;
            this.clearLogBtn.Click += new System.EventHandler(this.ClearLogBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(452, 349);
            this.Controls.Add(this.clearLogBtn);
            this.Controls.Add(this.AppOrPatchBtn);
            this.Controls.Add(this.CheckintegrityBtn);
            this.Controls.Add(this.OptionsBtn);
            this.Controls.Add(this.DisableLogBox);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.OutputWindow);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.MinimizeBtn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.AppFolderPathTextBox);
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
        public static void BorderFunc(Form form) {
            var MainBox = new GroupBox();
            MainBox.Location = new Point(0, -6);
            MainBox.Name = "MainBox";
            MainBox.Size = new Size(form.Size.Width, form.Size.Height + 7);
            form.Controls.Add(MainBox);
        }

        public static void AddControlEventHandlers(Control.ControlCollection Controls, Form form) {
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

        public static void MouseUpFunc(object sender, MouseEventArgs e) {
            mouse_is_down = 0;
            Options?.BringToFront();
        }
        public static void MouseDownFunc(object sender, MouseEventArgs e) {
            MouseDif = new Point(MousePosition.X - ActiveForm.Location.X, MousePosition.Y - ActiveForm.Location.Y);
            mouse_is_down = 1;
        }
        public static void MoveForm(object sender, MouseEventArgs e) {
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
        public static int mouse_is_down = 0;
        public static bool options_page_is_open, limit_output;
        public static bool[] text_box_changed = new bool[5];
        public static string[] default_strings = new string[] {
            "Paste The Gamedata Folder Path Here, Or Use The Browse Button...",
            "Add A Custom .gp4 Output Directory Here...",
            "Base Game .pkg Path... (For Game Patches)",
            "Add Files/Folders You Want To Exclude From The .gp4, Seperated By Semicolons",
            "Add Custom .pkg Passcode Here (Defaults To All Zeros)"
        };
        public static Point MouseDif, LastPos;
        public static Form Options;
        public static GP4Creator gp4;
        #endregion


        ////////////////////\\\\\\\\\\\\\\\\\\\
        ///--     Main Form Functions     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\
        #region Main Form Functions

        public static void TextBoxReady(object sender, EventArgs e) {
            var Sender = sender as TextBox;
            if(Sender.Font.Italic) {
                Sender.Font = new Font("Microsoft YaHei UI", 8.25F);
                Sender.Clear();
            }
        }
        public static void TextBoxReset(object sender, EventArgs e) {
            var Sender = sender as TextBox;
            if(!text_box_changed[Sender.TabIndex] | Sender.Text == "") {
                Sender.Text = default_strings[Sender.TabIndex];
                Sender.Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Italic);
            }
        }


        /// <summary> Apply The Path In The Text Box To gamedata_path.
        /// </summary>
        private void AppFolderPathBox_TextChanged(object sender, EventArgs e) {
            if(((TextBox)sender).Text == "")
                return;

            ((TextBox)sender).Font = new Font("Microsoft YaHei UI", 8.25F);


            if(Directory.Exists(AppFolderPathTextBox.Text.Replace("\"", ""))) {
                gp4 = new GP4Creator(((TextBox)sender).Text.Replace("\"", string.Empty));
                text_box_changed[0] = true;
                OptionsBtn.Enabled = true;
            }
        }


        /// <summary>
        /// Create Page For Changing Various .gp4 Options. <br/>(passcode, source pkg, etc)
        /// </summary>
        private void OptionsBtn_Click(object sender, EventArgs e) {
            LastPos = Location;

            Options?.BringToFront();
            if(options_page_is_open) return;

            OptionsPage NewPage = new OptionsPage();
            Options = NewPage;
            NewPage.Show();
        }


        /// <summary> Open Windows' Ghastly File Browser Dialog To Search For The Gamedata Folder
        /// </summary>
        private void BrowseBtn_Click(object sender, EventArgs e) {
            FolderBrowserDialog Browser = new FolderBrowserDialog();

            if(Browser.ShowDialog() == DialogResult.OK)
                AppFolderPathTextBox.Text = Browser.SelectedPath;
        }


        /// <summary>
        /// 
        /// </summary>
        private void BuildProjectFile(object sender, EventArgs e) {
            //gp4.CreateGP4();
        }
        #endregion






        ///////////////\\\\\\\\\\\\\\
        ///--     SFO Tests     --\\\
        ///////////////\\\\\\\\\\\\\\


        private static readonly string[] varstrings = new string[] { "Legacy", "First", "Second", "Third" };
        private void DumpSfoVariables(long Time, int ind) {

            var LogFilePath = $@"\[{varstrings[ind]}]-{null}-{(null == "gp" ? "patch" : "app")}_{null}.gp4";
            DLog($"[Output Path: {LogFilePath}]");

            using(var f = File.CreateText(Directory.GetCurrentDirectory() + LogFilePath)) {
                for(int i = 0; i < SfoParamLabels.Length; i++)
                    f.WriteLine($"{SfoParamLabels[i]}: {SfoParams[i]}");

                f.WriteLine($"Timing: {Time}ms");

                DLog($"Finished! Saved Log File At: {Directory.GetCurrentDirectory() + LogFilePath}\n\n");
            }
        }

        private static object[] SfoParams;
        private static string[]
            SfoParamLabels
        ;
        private void sfoDebugTest1Btn_Click(object sender, EventArgs e) {

            // Verify .sfo Path Or Search For One In The Current Directory
            var sfopath = @"D:\PS4\CUSA10249-patch\sce_sys\param - Copy.sfo";
            if(!File.Exists(sfopath))
                foreach(var f in Directory.GetFiles(Directory.GetCurrentDirectory()))
                    if(f.Contains(".sfo")) {
                        sfopath = f;
                        break;
                    }

            var timer = Stopwatch.StartNew();


            // Third Test Start
            DLog($"###################################\n       Beginning Third Test\n###################################\n");
            var ticks = timer.ElapsedMilliseconds;
            using(var sfo = File.OpenRead(sfopath)) {

                byte[] buffer;

                List<string> Labels;

                int[]
                    ParamOffsets,
                    DataTypes,
                    ParamLengths
                ;


                // Check PSF File Magic, + 4 Bytes To Skip Label Base Ptr
                sfo.Read(buffer = new byte[12], 0, 12);
                if(BitConverter.ToInt64(buffer, 0) != 1104986460160)
                    throw new InvalidDataException($"File Magic For .sfo Wasn't Valid ([Expected: 00-50-53-46-01-01-00-00] != [Read: {BitConverter.ToString(buffer)}])");


                // Read Base Pointer For .pkg Parameters
                sfo.Read(buffer = new byte[4], 0, 4);
                var ParamVariablesPointer = BitConverter.ToInt32(buffer, 0);

                // Read PSF Parameter Count
                sfo.Read(buffer, 0, 4);
                var ParameterCount = BitConverter.ToInt32(buffer, 0);

                // Initialize Arrays
                SfoParams = new object[ParameterCount];
                SfoParamLabels = new string[ParameterCount];
                DataTypes = new int[ParameterCount];
                ParamLengths = new int[ParameterCount];
                ParamOffsets = new int[ParameterCount];



                // Load Related Data For Each Parameter
                for(int i = 0; i < ParameterCount; i++) {

                    sfo.Position += 3; // Skip Label Offset

                    // Read And Check Data Type (4 = Int32, 2 = UTf8, 0 = Rsv4 )
                    if((DataTypes[i] = sfo.ReadByte()) == 2 || DataTypes[i] == 4) {
                        sfo.Read(buffer, 0, 4);
                        ParamLengths[i] = BitConverter.ToInt32(buffer, 0);

                        sfo.Read(buffer, 0, 4);
                        ParamOffsets[i] = BitConverter.ToInt32(buffer, 0);
                    }

                    sfo.Position += 4; // Skip Param Offset
                }
                
                // Load Parameter Labels
                for(int index = 0, @byte; index < ParameterCount; index++) {
                    var ByteList = new List<byte>();

                    // Read To End Of Label
                    for(; (@byte = sfo.ReadByte()) != 0; ByteList.Add((byte)@byte));

                    SfoParamLabels[index] = Encoding.UTF8.GetString(ByteList.ToArray());
                }


                for(int i = 0; i < ParameterCount; ParamVariablesPointer += ParamOffsets[i++]) {
                    sfo.Position = ParamVariablesPointer;

                    sfo.Read(buffer = new byte[ParamLengths[i]], 0, ParamLengths[i]);

                    DLog($"\nLabel: {SfoParamLabels[i]}");

                    // String
                    if(DataTypes[i] == 2) {
                        if(ParamLengths[i] > 1 && buffer[ParamLengths[i] - 1] == 0)
                            SfoParams[i] = Encoding.UTF8.GetString(buffer, 0, buffer.Length - 1);
                        else
                            SfoParams[i] = Encoding.UTF8.GetString(buffer);


                        if(((string)SfoParams[i])[0] == 0)
                            SfoParams[i] = "Empty String";

                        DLog($"Param: {SfoParams[i]}");
                    }

                    // Int32
                    else if(DataTypes[i] == 4) {
                        SfoParams[i] = BitConverter.ToInt32(buffer, 0);
                        DLog($"Param: {SfoParams[i]}");
                    }
                }

                #region
                var Stop = timer.ElapsedMilliseconds - ticks;
                DLog($"\nTest 3 Timing (milliseconds): {Stop}");
                DLog($"Dumping...");
                DumpSfoVariables(Stop, 3);
                #endregion
            }
            // --

        }



        /////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     GP4 Related Functions     --\\\
        /////////////////////\\\\\\\\\\\\\\\\\\\\
        #region GP4 Related Functions
        private void WLog(object s) {
            DLog(s);

            if (!DisableLogBox.Checked)
                OutputWindow.AppendText("\n" + s);
        }

        private static void DLog(object o = null) {
            if(o == null) o = "\n";

            if(!Console.IsOutputRedirected)
                try { Debug.WriteLine(o.ToString()); }
                catch(Exception) { }

                try { Console.WriteLine(o.ToString()); }
                catch(Exception) { }
        }



        private bool CheckGP4Settings() {
            /*
            if(Passcode.Length != 32) {
                WLog("Incorrect Passcode Length, Must Be 32 Characters");
                return true;
            }
            if(!Directory.Exists(gamedata_folder)) {
                WLog($"Could Not Find The Game Data Directory\n{gamedata_folder}");
                WLog(".gp4 Creation Aborted");
                return true;
            }
            if(!Directory.Exists(Gp4OutputDirectory)) {
                WLog($"Could Not Find The Selected .gp4 Output Directory\n({Gp4OutputDirectory})");
                Gp4OutputDirectory = gamedata_folder.Remove(gamedata_folder.LastIndexOf(@"\"));
                WLog($".gp4 Will Be Placed In {Gp4OutputDirectory}");
            }
            */

            return false;
        }

        #endregion

        ////////////////////\\\\\\\\\\\\\\\\\\\\
        ///--     Control Declarations     --\\\
        ////////////////////\\\\\\\\\\\\\\\\\\\\
        #region ControlDeclarations
        private Button CreateBtn;
        private rTextBox AppFolderPathTextBox;
        private Label Title;
        private Button MinimizeBtn;
        private Button ExitBtn;
        private RichTextBox OutputWindow;
        private Button BrowseBtn;
        private CheckBox DisableLogBox;
        private Button OptionsBtn;
        #endregion
    }
}
