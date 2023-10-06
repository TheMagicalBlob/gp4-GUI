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
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;

namespace Gp4ProjectBuilder {

    public partial class MainForm : Form { // ver 1.6.13
        public MainForm() {
            InitializeComponent();
            BorderFunc(this);
            AddControlEventHandlers(Controls, this);

            // Designer Will Delete This From InitializeComponent If Added Manually
            AppFolderPathTextBox.MouseClick += TextBoxReady;
            AppFolderPathTextBox.LostFocus += TextBoxReset;
        }


        // Application Variables
        public static int MouseIsDown = 0;
        public static bool OptionsAreOpen;
        public static bool[] TextBoxHasChanged = new bool[5];
        public static string[] DefaultTextBoxStrings = new string[] {
            "Paste Gamedata Path Here, Or Use The Browse Button...",
            "Custom .gp4 Output Directory...",
            "Custom Base Game .pkg Directory To GP4...",
            "Add Files/Folders You Want To Exclude, Seperated By Semicolons",
            "Add Custom .pkg Passcode Here (Defaults To All Zeros)"
        };
        public static Point MouseDif, LastPos;
        static Form Options;


        // GP4 Creation Variables
        public static byte[] BufferArray;
        int chunk_count, scenario_count, default_id, index = 0;
        int[] scenario_types, scenario_chunk_range, initial_chunk_count;
        string APP_FOLDER, app_ver = "", version = "", content_id, title_id = "CUSA12345", category = "?";
        string[] chunk_labels, parameter_labels, scenario_labels, file_paths;
        readonly string[] RequiredSFOVariables = new string[] { "APP_VER", "CATEGORY", "CONTENT_ID", "TITLE_ID", "VERSION" };
        static XmlDocument GP4;
        XmlDeclaration Declaration;
        XmlElement file, chunk, scenario, dir, subdir;

        // GP4 Options
        public static bool ignore_keystone = false;
        public static string
            passcode = "00000000000000000000000000000000",
            gp4_output_directory = @"C:\Users\Blob\Desktop\",
            pkg_source = ""
        ;
        public static string[] FilterStrings;
        private Button TmpBtn;


        #region Basic Form Functions
        #region Designer Managed
        private IContainer components = null;
        protected override void Dispose(bool disposing) {
            if(disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            this.CreateBtn = new System.Windows.Forms.Button();
            this.AppFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.OutputWindow = new System.Windows.Forms.RichTextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.DisableLogBox = new System.Windows.Forms.CheckBox();
            this.OptionsBtn = new System.Windows.Forms.Button();
            this.TmpBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateBtn
            // 
            this.CreateBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CreateBtn.Location = new System.Drawing.Point(371, 58);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateBtn.TabIndex = 0;
            this.CreateBtn.Text = "Build .gp4";
            this.CreateBtn.UseVisualStyleBackColor = true;
            this.CreateBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // AppFolderPathTextBox
            // 
            this.AppFolderPathTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppFolderPathTextBox.Location = new System.Drawing.Point(8, 33);
            this.AppFolderPathTextBox.Name = "AppFolderPathTextBox";
            this.AppFolderPathTextBox.Size = new System.Drawing.Size(437, 21);
            this.AppFolderPathTextBox.TabIndex = 0;
            this.AppFolderPathTextBox.Text = "Paste Gamedata Path Here, Or Use The Browse Button...";
            this.AppFolderPathTextBox.TextChanged += new System.EventHandler(this.AppFolderPathBox_TextChanged);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Gadugi", 8.5F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(132, 6);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(183, 16);
            this.Title.TabIndex = 3;
            this.Title.Text = ".gp4 Creator For Orbis Packages";
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MinimizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MinimizeBtn.Location = new System.Drawing.Point(401, 6);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(22, 22);
            this.MinimizeBtn.TabIndex = 4;
            this.MinimizeBtn.Text = "-";
            this.MinimizeBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.MinimizeBtn.UseVisualStyleBackColor = true;
            // 
            // ExitBtn
            // 
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExitBtn.Location = new System.Drawing.Point(423, 6);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(22, 22);
            this.ExitBtn.TabIndex = 5;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = true;
            // 
            // OutputWindow
            // 
            this.OutputWindow.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OutputWindow.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F);
            this.OutputWindow.ForeColor = System.Drawing.SystemColors.Window;
            this.OutputWindow.Location = new System.Drawing.Point(4, 103);
            this.OutputWindow.Name = "OutputWindow";
            this.OutputWindow.ReadOnly = true;
            this.OutputWindow.Size = new System.Drawing.Size(444, 241);
            this.OutputWindow.TabIndex = 6;
            this.OutputWindow.Text = "";
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BrowseBtn.Location = new System.Drawing.Point(305, 58);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(60, 22);
            this.BrowseBtn.TabIndex = 7;
            this.BrowseBtn.Text = "Browse...";
            this.BrowseBtn.UseVisualStyleBackColor = true;
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
            this.OptionsBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OptionsBtn.Location = new System.Drawing.Point(8, 6);
            this.OptionsBtn.Name = "OptionsBtn";
            this.OptionsBtn.Size = new System.Drawing.Size(75, 23);
            this.OptionsBtn.TabIndex = 9;
            this.OptionsBtn.Text = "Tool Options";
            this.OptionsBtn.UseVisualStyleBackColor = true;
            this.OptionsBtn.Click += new System.EventHandler(this.OptionsBtn_Click);
            // 
            // TmpBtn
            // 
            this.TmpBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TmpBtn.Location = new System.Drawing.Point(89, 7);
            this.TmpBtn.Name = "TmpBtn";
            this.TmpBtn.Size = new System.Drawing.Size(22, 22);
            this.TmpBtn.TabIndex = 0;
            this.TmpBtn.Text = "!";
            this.TmpBtn.UseVisualStyleBackColor = true;
            this.TmpBtn.Click += new System.EventHandler(this.DebugDumpAllOptions);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(452, 349);
            this.Controls.Add(this.TmpBtn);
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
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public static void BorderFunc(Form form) {
            MainBox = new GroupBox();
            MainBox.Location = new Point(0, -6);
            MainBox.Name = "MainBox";
            MainBox.Size = new Size(form.Size.Width, form.Size.Height + 7);
            form.Controls.Add(MainBox);
        }

        public static void AddControlEventHandlers(Control.ControlCollection Controls, Form form) { // Got Sick of Manually Editing InitializeComponent()
            #region DebugLabel
#if DEBUG
            Label DebugLabel = new Label();
            DebugLabel.Size = new Size(36, 19);
            DebugLabel.Location = new Point(form.Size.Width - 46 - (form.Controls.Find("ExitBtn", true)[0].Size.Width * 2), 3);
            DebugLabel.ForeColor = SystemColors.Control;
            DebugLabel.BackColor = SystemColors.WindowText;
            DebugLabel.BorderStyle = BorderStyle.FixedSingle;
            DebugLabel.Font = new Font("Franklin Gothic Medium", 7F, FontStyle.Bold);
            DebugLabel.Text = "(Dev)";
            Controls.Add(DebugLabel);
            DebugLabel.BringToFront();
#endif
            #endregion

            form.MouseDown += new MouseEventHandler(MouseDownFunc);
            form.MouseUp += new MouseEventHandler(MouseUpFunc);
            form.MouseMove += new MouseEventHandler(MoveForm);

            foreach(Control Item in Controls) {
                if(Item.HasChildren) { // Designer Added Some Things To The Form, And Some To The Group Box Used To Make The Border. This is me bing lazy. as long as it's not noticably slower
                    foreach(Control Child in Item.Controls) {

                        Child.MouseDown += new MouseEventHandler(MouseDownFunc);
                        Child.MouseUp += new MouseEventHandler(MouseUpFunc);

                        if(!Child.Name.Contains("PathBox")) // So You Can Drag Select The Text Lol
                            Child.MouseMove += new MouseEventHandler(MoveForm);
                    }
                }
                Item.MouseDown += new MouseEventHandler(MouseDownFunc);
                Item.MouseUp += new MouseEventHandler(MouseUpFunc);
                if(!Item.Name.Contains("PathBox")) // So You Can Drag Select The Text Lol
                    Item.MouseMove += new MouseEventHandler(MoveForm);
            }
            try {
                Controls.Find("MinimizeBtn", true)[0].Click += new EventHandler(MinimizeBtn_Click);
                Controls.Find("MinimizeBtn", true)[0].MouseEnter += new EventHandler(ExitBtnMH);
                Controls.Find("MinimizeBtn", true)[0].MouseLeave += new EventHandler(ExitBtnML);
                Controls.Find("ExitBtn", true)[0].Click += new EventHandler(ExitBtn_Click);
                Controls.Find("ExitBtn", true)[0].MouseEnter += new EventHandler(ExitBtnMH);
                Controls.Find("ExitBtn", true)[0].MouseLeave += new EventHandler(ExitBtnML);
                Controls.Find("MainBox", true)[0].MouseDown += new MouseEventHandler(MouseDownFunc);
                Controls.Find("MainBox", true)[0].MouseUp += new MouseEventHandler(MouseUpFunc);
                Controls.Find("MainBox", true)[0].MouseMove += new MouseEventHandler(MoveForm);
            } catch(IndexOutOfRangeException){}
        }
        private static void MinimizeBtn_Click(object sender, EventArgs e) => ActiveForm.WindowState = FormWindowState.Minimized;
        private static void ExitBtn_Click(object sender, EventArgs e) => Environment.Exit(0);
        public static void ExitBtnMH(object sender, EventArgs e) => ((Control)sender).ForeColor = Color.FromArgb(255, 227, 0);
        public static void ExitBtnML(object sender, EventArgs e) => ((Control)sender).ForeColor = Color.FromArgb(0,0,0);

        public static void MouseUpFunc(object sender, MouseEventArgs e) {
            MouseIsDown = 0;
            if(Options == null) return;
            Options.BringToFront();
        }
        public static void MouseDownFunc(object sender, MouseEventArgs e) {
            MouseDif = new Point(MousePosition.X - ActiveForm.Location.X, MousePosition.Y - ActiveForm.Location.Y);
            MouseIsDown = 1;
        }
        public static void MoveForm(object sender, MouseEventArgs e) {
            if(MouseIsDown != 0) {
                ActiveForm.Location = new Point(MousePosition.X - MouseDif.X, MousePosition.Y - MouseDif.Y);
                ActiveForm.Update();
                if(Options == null) return;
                Options.Location = new Point(MousePosition.X - MouseDif.X + 30, MousePosition.Y - MouseDif.Y + 60);
                Options.Update();
            }
        }
        #endregion


        public void Out(object s) {
            if(!DisableLogBox.Checked)
                OutputWindow.AppendText("\n" + s);
        }
        private void DebugDumpAllOptions(object sender, EventArgs e) {
            Out(APP_FOLDER);
            Out($"Passcode: {passcode}");
            Out($"GP4 Output Directory: {gp4_output_directory}");
            Out($"PKG Source: {pkg_source}");
            if(FilterStrings != null) {
                Out("Filter: ");
                foreach(string s in FilterStrings) {
                    Out($" {s}");
                }
            }
        }



        private void BrowseBtn_Click(object sender, EventArgs e) {
            FolderBrowserDialog Browser = new FolderBrowserDialog();
            if(Browser.ShowDialog() == DialogResult.OK)
                AppFolderPathTextBox.Text = Browser.SelectedPath;
        }

        private void OptionsBtn_Click(object sender, EventArgs e) {
            LastPos = Location;
            if(OptionsAreOpen) {
                Options.BringToFront();
                return;
            }
            OptionsPage NewPage = new OptionsPage();
            NewPage.Show();
            Options = NewPage;
        }

        private void AppFolderPathBox_TextChanged(object sender, EventArgs e) {
            APP_FOLDER = AppFolderPathTextBox.Text.Replace("\"", "");
            if (Directory.Exists(APP_FOLDER))
            TextBoxHasChanged[0] = true;
        }

        public static void TextBoxReady(object sender, EventArgs e) {
            var Sender = sender as TextBox;
            if(Sender.Font.Italic) {
                Sender.Font = new Font("Microsoft YaHei UI", 8.25F);
                Sender.Clear();
            }
        }
        public static void TextBoxReset(object sender, EventArgs e) {
            var Sender = sender as TextBox;
            if(!TextBoxHasChanged[Sender.TabIndex]) {
                Sender.Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Italic);
                Sender.Text = DefaultTextBoxStrings[Sender.TabIndex];
            }
        }

        private bool PrebuildChecks() {

            if(passcode.Length != 32) {
                Out("Incorrect Passcode Length, Must Be 32 Characters");
                return true;
            }


            if(!Directory.Exists(APP_FOLDER)) {
                Out($"The Directory Given Does Not Exist!\n{APP_FOLDER}");
                Out(".gp4 Creatuon Aborted");
                return true;
            }

            return false;
        }

        private void CreateBtn_Click(object sender, EventArgs e) {
            // TODO: 
            // - Add Error Handling For Missing Files Or Other Misc Crap
            // - figure out pfs compression / chunk bs for certain file formats
            /*
             playgo-chunks.dat
              0x00 - 0x8: Head
              0x0A: Chunk Count
              0x10: File End
              0x0E: Scenario Count
              0xE0: Scenario Data Section(s)
              0x14: Default ID

              0xD0: chunk label beggining
              0xD4: Chunk Label Byte Array Length
              0xD8: chunk label end (Padded)
              0xE0: Senario 1 type (*0xE0 + 0x20 For Each Scenario After?)
              0xF0: Scenario Labels
              0xF4: Scenario Label Array Byte Length

             param.sfo
              0x00 - 0x8: Head
              0x08 - Param Labels
              0x0C - Param Values
              0x10 - Param Count

              starting at 0x20:
              Param Offsets every 16 bytes
             */
            if(PrebuildChecks()) return;


            OutputWindow.Clear();
            OutputWindow.AppendText("Starting .gp4 Creation");

            var TimeStamp = $"{DateTime.Now.GetDateTimeFormats()[78]}"; // Sony One, For Consistency
            var miliseconds = DateTime.Now.Millisecond; // Format Sony Used Doesn't Have Miliseconds, But I Still Wanna Track It For Now
            var InternalTimeStamp = $"{DateTime.Now}"; // Alternate One To Accurately Track Build Times


            GP4 = new XmlDocument();
            Declaration = GP4.CreateXmlDeclaration("1.1", "utf-8", "yes");


            // Some Files Or Formats Aren't Supposed To Be Included. I Doubt I Got Lucky And My Game Happened To Have Them All, So I'm Probably Missing Shit
            bool FileShouldBeExcluded(string filepath) {

                if(!filepath.Contains('.'))
                    return false;

                // To Exclude certain Files From sce_sys while not excluding them entirely
                var filename = filepath.Remove(filepath.LastIndexOf(".")).Substring(filepath.LastIndexOf('\\') + 1);

                string[] blacklist = new string[] {
                  // Drunk Canadian Guy
                    "right.sprx",
                    $"{(ignore_keystone ? @"sce_sys\keystone" : "@@")}",
                    "sce_discmap.plt",
                    @"sce_sys\playgo-chunk",
                    @"sce_sys\psreserved.dat",
                    $@"sce_sys\{filename}.dds",
                    @"sce_sys\playgo-manifest.xml",
                    @"sce_sys\origin-deltainfo.dat",
                  // Al Azif
                    @"sce_sys\.metas",
                    @"sce_sys\.digests",
                    @"sce_sys\.image_key",
                    @"sce_sys\license.dat",
                    @"sce_sys\.entry_keys",
                    @"sce_sys\.entry_names",
                    @"sce_sys\license.info",
                    @"sce_sys\selfinfo.dat",
                    @"sce_sys\imageinfo.dat",
                    @"sce_sys\.unknown_0x21",
                    @"sce_sys\.unknown_0xC0",
                    @"sce_sys\pubtoolinfo.dat",
                    @"sce_sys\app\playgo-chunk",
                    @"sce_sys\.general_digests",
                    @"sce_sys\target-deltainfo.dat",
                    @"sce_sys\app\playgo-manifest.xml",
                    //
                };

                foreach(var blacklisted_file_or_folder in blacklist)
                    if(filepath.Contains(blacklisted_file_or_folder)) {
                        Out($"Ignoring: {filepath}");
                        return true;
                    }
                return false;
            }

            void LoadParameterLabels(string[] StringArray) {
                int byteIndex = 0;
                StringBuilder Builder;

                for(index = 0; index < StringArray.Length; index++) {
                    Builder = new StringBuilder();

                    while(BufferArray[byteIndex] != 0)
                        Builder.Append(Encoding.UTF8.GetString(new byte[] { BufferArray[byteIndex++] })); // Just Take A Byte, You Fussy Prick

                    byteIndex++; //!
                    StringArray[index] = Builder.ToString();
                }
            }


            //////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            ///--    Parse playgo-chunks.dat And Param.sfo To Get Most Variables    --\\\
            //////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            using(var playgo = File.OpenRead($@"{APP_FOLDER}\sce_sys\playgo-chunk.dat")) {
                // Read Chunk Count
                playgo.Position = 0x0A;
                chunk_count = (byte)playgo.ReadByte();
                chunk_labels = new string[chunk_count];

                // Read Scenario Count
                playgo.Position = 0x0E;
                scenario_count = (byte)playgo.ReadByte();
                scenario_types = new int[scenario_count];
                scenario_labels = new string[scenario_count];
                initial_chunk_count = new int[scenario_count];
                scenario_chunk_range = new int[scenario_count];

                // Read Default Scenario Id
                playgo.Position = 0x14;
                default_id = (byte)playgo.ReadByte();

                // Read Content ID Here Instead Of The .sfo Because Meh, User Has Bigger Issues If Those Aren't the Same
                BufferArray = new byte[36];
                playgo.Position = 0x40;
                playgo.Read(BufferArray, 0, 36);
                content_id = Encoding.UTF8.GetString(BufferArray);

                // Read Chunk Label Start Address From Pointer
                BufferArray = new byte[4];
                playgo.Position = 0xD0;
                playgo.Read(BufferArray, 0, 4);
                var chunk_label_pointer = BitConverter.ToInt32(BufferArray, 0);

                // Read Length Of Chunk Label Byte Array
                playgo.Position = 0xD4;
                playgo.Read(BufferArray, 0, 4);
                var chunk_label_array_length = BitConverter.ToInt32(BufferArray, 0);

                // Load Scenario(s)
                playgo.Position = 0xE0;
                playgo.Read(BufferArray, 0, 4);
                var scenarioPointer = BitConverter.ToInt32(BufferArray, 0);
                for(index = 0; index < scenario_count; index++) {
                    // Read Scenario Type
                    playgo.Position = scenarioPointer;
                    scenario_types[index] = (byte)playgo.ReadByte();

                    // Read Scenario initial_chunk_count
                    playgo.Position = (scenarioPointer + 0x14);
                    playgo.Read(BufferArray, 2, 2);
                    initial_chunk_count[index] = BitConverter.ToInt16(BufferArray, 2);
                    playgo.Read(BufferArray, 2, 2);
                    scenario_chunk_range[index] = BitConverter.ToInt16(BufferArray, 2);
                    scenarioPointer += 0x20;
                }

                // Load Scenario Label Array Byte Length
                BufferArray = new byte[2];
                playgo.Position = 0xF4;
                playgo.Read(BufferArray, 0, 2);
                var scenario_label_array_length = BitConverter.ToInt16(BufferArray, 0);

                // Load Scenario Label Pointer
                playgo.Position = 0xF0;
                BufferArray = new byte[4];
                playgo.Read(BufferArray, 0, 4);
                var scenario_label_array_pointer = BitConverter.ToInt32(BufferArray, 0);

                // Load Scenario Labels
                playgo.Position = scenario_label_array_pointer;
                BufferArray = new byte[scenario_label_array_length];
                playgo.Read(BufferArray, 0, BufferArray.Length);
                LoadParameterLabels(scenario_labels);

                // Load Chunk Labels
                BufferArray = new byte[chunk_label_array_length];
                playgo.Position = chunk_label_pointer;
                playgo.Read(BufferArray, 0, BufferArray.Length);
                LoadParameterLabels(chunk_labels);
            }



            ////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            ///--    Parse param.sfo For Various Parameters    --\\\
            ////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            using(var sfo = File.OpenRead($@"{APP_FOLDER}\sce_sys\param.sfo")) {
                // Read Pointer For Array Of Parameter Names
                sfo.Position = 0x8;
                BufferArray = new byte[4];
                sfo.Read(BufferArray, 0, 4);
                var ParamNameArrayPointer = BitConverter.ToInt32(BufferArray, 0);

                // Read Base Pointer For .pkg Parameters
                sfo.Position = 0x0C;
                sfo.Read(BufferArray, 0, 4);
                var ParamVariablesPointer = BitConverter.ToInt32(BufferArray, 0);

                // Read Parameter Name Array Length And Initialize Offset Array
                sfo.Position = 0x10;
                sfo.Read(BufferArray, 0, 4);
                var ParamNameArrayLength = BitConverter.ToInt32(BufferArray, 0);
                int[] ParameterOffsets = new int[ParamNameArrayLength];

                // Load Parameter Names
                BufferArray = new byte[ParamVariablesPointer - ParamNameArrayPointer];
                parameter_labels = new string[ParamNameArrayLength];
                sfo.Position = ParamNameArrayPointer;
                sfo.Read(BufferArray, 0, BufferArray.Length);
                LoadParameterLabels(parameter_labels);

                // Load Parameter Offsets
                sfo.Position = 0x20;
                BufferArray = new byte[4];
                for(index = 0; index < ParamNameArrayLength; sfo.Position += (0x10 - BufferArray.Length)) {
                    sfo.Read(BufferArray, 0, 4);
                    ParameterOffsets[index] = ParamVariablesPointer + BitConverter.ToInt32(BufferArray, 0);
                    index++;
                }

                // Load The Rest Of The Required .pkg Variables From param.sfo
                for(index = 0; index < ParamNameArrayLength; index++)
                    if(RequiredSFOVariables.Contains(parameter_labels[index])) { // Ignore Variables Not Needed For .gp4 Project Creation

                        sfo.Position = ParameterOffsets[index];
                        BufferArray = new byte[4];

                        switch(parameter_labels[index]) { // I'm Too Tired to think of a more elegant solution right now. If it works, it works

                            case "APP_VER":
                                BufferArray = new byte[5];
                                sfo.Read(BufferArray, 0, 5);
                                app_ver = Encoding.UTF8.GetString(BufferArray);
                                break;
                            case "CATEGORY": // gd / gp
                                sfo.Read(BufferArray, 0, 2);
                                category = Encoding.UTF8.GetString(BufferArray, 0, 2);
                                break;
                            case "CONTENT_ID":
                                BufferArray = new byte[36];
                                sfo.Read(BufferArray, 0, 36);
                                content_id = Encoding.UTF8.GetString(BufferArray);
                                break;
                            case "TITLE_ID":
                                BufferArray = new byte[9];
                                sfo.Read(BufferArray, 0, 9);
                                title_id = Encoding.UTF8.GetString(BufferArray);
                                break;
                            case "VERSION": // Remaster
                                BufferArray = new byte[5];
                                sfo.Read(BufferArray, 0, 5);
                                version = Encoding.UTF8.GetString(BufferArray);
                                break;
                        }
                    }
            }


            ////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\
            ///--     Read Project Files And Directories     --\\\
            ////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\
            DirectoryInfo directoryInfo = new DirectoryInfo(APP_FOLDER);
            FileInfo[] file_info = directoryInfo.GetFiles(".", SearchOption.AllDirectories);

            file_paths = new string[file_info.Length];
            for(index = 0; index < file_info.Length - 1; index++)
                file_paths[index] = file_info[index].FullName;


            ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
            ///--     Create Base .gp4 Elements     --\\\
            ///////////////////////\\\\\\\\\\\\\\\\\\\\\\
            var psproject = GP4.CreateElement("psproject");
                psproject.SetAttribute("fmt", "gp4");
                psproject.SetAttribute("version", "1000");

            var volume = GP4.CreateElement("volume");

            var volume_type = GP4.CreateElement("volume_type");
                volume_type.InnerText = $"pkg_{(category == "gd" ? "ps4_app" : "ps4_patch")}";

            var volume_id = GP4.CreateElement("volume_id");
                volume_id.InnerText = "PS4VOLUME";

            var volume_ts = GP4.CreateElement("volume_ts");
                volume_ts.InnerText = TimeStamp;

            var package = GP4.CreateElement("package");
                package.SetAttribute("content_id", content_id);
                package.SetAttribute("passcode", passcode);
                package.SetAttribute("storage_type", (category == "gp" ? "digital25" : "digital50"));
                package.SetAttribute("app_type", "full");
            if(category == "gp")
                package.SetAttribute("app_path", $"{content_id}-A{app_ver.Replace(".", "")}-V{version.Replace(".", "")}.pkg");

            var chunk_info = GP4.CreateElement("chunk_info");
                chunk_info.SetAttribute("chunk_count", $"{chunk_count}");
                chunk_info.SetAttribute("scenario_count", $"{scenario_count}");


            var chunks = GP4.CreateElement("chunks");
            for(int chunk_id = 0; chunk_id < chunk_count; chunk_id++) {
                chunk = GP4.CreateElement("chunk");
                chunk.SetAttribute("id", $"{chunk_id}");

                if(chunk_labels[chunk_id] == "") //  I Hope This Fix Works For Every Game...
                    chunk.SetAttribute("label", $"Chunk #{chunk_id}");
                else
                    chunk.SetAttribute("label", $"{chunk_labels[chunk_id]}");
                chunks.AppendChild(chunk);
            }


            var scenarios = GP4.CreateElement("scenarios");
                scenarios.SetAttribute("default_id", $"{default_id}");

            for(index = 0; index < scenario_count; index++) {
                scenario = GP4.CreateElement("scenario");
                scenario.SetAttribute("id", $"{index}");
                scenario.SetAttribute("type", $"{(scenario_types[index] == 1 ? "sp" : "mp")}");
                scenario.SetAttribute("initial_chunk_count", $"{initial_chunk_count[index]}");
                scenario.SetAttribute("label", $"{scenario_labels[index]}");
                scenario.InnerText = $"0-{scenario_chunk_range[index] - 1}";
                scenarios.AppendChild(scenario);
            }


            var files = GP4.CreateElement("files");
            for(index = 0; index < file_paths.Length - 1; index++) {
                if(FileShouldBeExcluded(file_paths[index])) goto Skip;
                file = GP4.CreateElement("file");
                file.SetAttribute("targ_path", (file_paths[index].Replace(APP_FOLDER + "\\", string.Empty)).Replace('\\', '/'));
                file.SetAttribute("orig_path", file_paths[index]);
                /* 
                   if (FileUsedPfsComporession(file_paths[path_index]))
                   file.SetAttribute("pfs_compression", "enabled");
                   if (FileWantsChunkyBeefStew(file_paths[path_index]))
                   file.SetAttribute("chunks", file_paths[path_index]);
                */
                files.AppendChild(file);
            Skip: { }
            }



            //////////////////////\\\\\\\\\\\\\\\\\\\\\\
            ///--     rootdir Directory Nesting     --\\\
            //////////////////////\\\\\\\\\\\\\\\\\\\\\\

            index = 0;
            var rootdir = GP4.CreateElement("rootdir");

            void AppendSubfolder(string _dir, XmlElement node) {
                foreach(string folder in Directory.GetDirectories(_dir)) {
                    subdir = GP4.CreateElement("dir");
                    subdir.SetAttribute("targ_name", folder.Substring(folder.LastIndexOf('\\') + 1));
                    if(folder.Substring(folder.LastIndexOf('\\') + 1) != "about")
                        node.AppendChild(subdir);
                    if(Directory.GetDirectories(folder).Length > 0) AppendSubfolder(folder, subdir);
                }
            }

            foreach(string folder in Directory.GetDirectories(APP_FOLDER)) {
                dir = GP4.CreateElement("dir");
                dir.SetAttribute("targ_name", folder.Substring(folder.LastIndexOf('\\') + 1));
                rootdir.AppendChild(dir);
                if(Directory.GetDirectories(folder).Length > 0) AppendSubfolder(folder, dir);
            }



            ////////////////////\\\\\\\\\\\\\\\\\\\\
            ///--     Build .gp4 Structure     --\\\
            ////////////////////\\\\\\\\\\\\\\\\\\\\
            GP4.AppendChild(Declaration);
            GP4.AppendChild(psproject);
            psproject.AppendChild(volume);
            psproject.AppendChild(files);
            psproject.AppendChild(rootdir);
            volume.AppendChild(volume_type);
            volume.AppendChild(volume_id);
            volume.AppendChild(volume_ts);
            volume.AppendChild(package);
            volume.AppendChild(chunk_info);
            chunk_info.AppendChild(chunks);
            chunk_info.AppendChild(scenarios);
            var stamp = GP4.CreateComment($"gengp4.exe Alternative. Time Taken For Build Process: {DateTime.Now.Minute - DateTime.Parse(TimeStamp).Minute}:{DateTime.Now.Second - DateTime.Parse(TimeStamp).Second}.{DateTime.Now.Millisecond - miliseconds}");
            GP4.AppendChild(stamp);
            GP4.Save($@"{gp4_output_directory}\{title_id}-{(category == "gd" ? "app" : "patch")}.gp4");
            
            OutputWindow.AppendText($"Finished!\nFile Saved At {gp4_output_directory.Remove(gp4_output_directory.Length - 1)}");
            Out($"Time Taken {DateTime.Now.Subtract(DateTime.Parse(InternalTimeStamp))}");
        }





        private Button CreateBtn;
        private TextBox AppFolderPathTextBox;
        private Label Title;
        private Button MinimizeBtn;
        private Button ExitBtn;
        private RichTextBox OutputWindow;
        private Button BrowseBtn;
        private CheckBox DisableLogBox;
        private Button OptionsBtn;
        private static GroupBox MainBox;
    }
}
