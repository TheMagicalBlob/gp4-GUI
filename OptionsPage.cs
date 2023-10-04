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

    public partial class OptionsPage : Form { // ver 1.1.3
        public OptionsPage() {
            InitializeComponent();
            MainForm.BorderFunc(this);
            MainForm.AddControlEventHandlers(Controls, this);
        }

        // Main Variables
        byte[] BufferArray;
        int chunk_count, scenario_count, default_id, index = 0;
        int[] scenario_types, scenario_chunk_range, initial_chunk_count;
        bool ignore_keystone = false;
        string OUTPUT_DIRECTORY = @"C:\Users\Blob\Desktop\", APP_FOLDER, app_ver = "", version = "", content_id, title_id = "CUSA12345", passcode = "00000000000000000000000000000000", category = "?";
        string[] chunk_labels, parameter_labels, scenario_labels, file_paths,
        RequiredVariables = new string[] { "APP_VER", "CATEGORY", "CONTENT_ID", "TITLE_ID", "VERSION" };

        // Base Xml And XmlDeclaration
        static XmlDocument GP4;
        XmlElement file, chunk, scenario, dir, subdir;
        XmlDeclaration Declaration;

        #region Designer
        private IContainer components = null;
        protected override void Dispose(bool disposing) {
            if(disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            this.Btn = new System.Windows.Forms.Button();
            this.KeystoneToggleBox = new System.Windows.Forms.CheckBox();
            this.CustomGP4PathBox = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.IgnoreOutputBox = new System.Windows.Forms.CheckBox();
            this.CustomPKGPathBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Btn
            // 
            this.Btn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Btn.Location = new System.Drawing.Point(7, 2);
            this.Btn.Name = "Btn";
            this.Btn.Size = new System.Drawing.Size(59, 23);
            this.Btn.TabIndex = 0;
            this.Btn.Text = "Button";
            this.Btn.UseVisualStyleBackColor = true;
            this.Btn.Visible = false;
            this.Btn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // KeystoneToggleBox
            // 
            this.KeystoneToggleBox.AutoSize = true;
            this.KeystoneToggleBox.Location = new System.Drawing.Point(8, 81);
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
            this.CustomGP4PathBox.Location = new System.Drawing.Point(6, 27);
            this.CustomGP4PathBox.Name = "CustomGP4PathBox";
            this.CustomGP4PathBox.Size = new System.Drawing.Size(376, 20);
            this.CustomGP4PathBox.TabIndex = 2;
            this.CustomGP4PathBox.Text = "Custom .gp4 Output Directory...";
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
            // CloseBtn
            // 
            this.CloseBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CloseBtn.Location = new System.Drawing.Point(356, 2);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(22, 22);
            this.CloseBtn.TabIndex = 5;
            this.CloseBtn.Text = "X";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // IgnoreOutputBox
            // 
            this.IgnoreOutputBox.AutoSize = true;
            this.IgnoreOutputBox.Location = new System.Drawing.Point(7, 103);
            this.IgnoreOutputBox.Name = "IgnoreOutputBox";
            this.IgnoreOutputBox.Size = new System.Drawing.Size(196, 17);
            this.IgnoreOutputBox.TabIndex = 8;
            this.IgnoreOutputBox.Text = "Limit Log Output (Builds .gp4 Faster)";
            this.IgnoreOutputBox.UseVisualStyleBackColor = true;
            // 
            // CustomPKGPathBox
            // 
            this.CustomPKGPathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomPKGPathBox.Location = new System.Drawing.Point(6, 53);
            this.CustomPKGPathBox.Name = "CustomPKGPathBox";
            this.CustomPKGPathBox.Size = new System.Drawing.Size(375, 20);
            this.CustomPKGPathBox.TabIndex = 9;
            this.CustomPKGPathBox.Text = "Custom Base Game .pkg Directory To GP4...";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(7, 146);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(375, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Custom Base Game .pkg Directory To GP4...";
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(387, 195);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CustomPKGPathBox);
            this.Controls.Add(this.IgnoreOutputBox);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.CustomGP4PathBox);
            this.Controls.Add(this.KeystoneToggleBox);
            this.Controls.Add(this.Btn);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OptionsPage";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void CloseBtn_Click(object sender, EventArgs e) => this.Dispose();
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
        private void KeystoneToggleBox_CheckedChanged(object sender, EventArgs e) => ignore_keystone = KeystoneToggleBox.Checked;

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

            Console.Clear();
            Console.WriteLine("Starting .gp4 Creation");

            var TimeStamp = $"{DateTime.Now.GetDateTimeFormats()[78]}"; // Sony One, For Consistency
            var miliseconds = DateTime.Now.Millisecond; // Format Sony Used Doesn't Have Miliseconds, But I Still Wanna Track It For Now
            var InternalTimeStamp = $"{DateTime.Now}"; // Alternate One To Accurately Track Build Times

            APP_FOLDER = CustomGP4PathBox.Text.Replace("\"", "");
            if(!Directory.Exists(APP_FOLDER)) {
                Out($"The Directory Given Does Not Exist!\n{APP_FOLDER}");
                Out(".gp4 Creatuon Aborted");
                return;
            }

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
                    if(RequiredVariables.Contains(parameter_labels[index])) { // Ignore Variables Not Needed For .gp4 Project Creation

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
            GP4.Save($@"{OUTPUT_DIRECTORY}\{title_id}-{(category == "gd" ? "app" : "patch")}.gp4");

            Out($"Finished!\nFile Saved At {OUTPUT_DIRECTORY.Remove(OUTPUT_DIRECTORY.Length - 1)}");
            Out($"Time Taken {DateTime.Now.Subtract(DateTime.Parse(InternalTimeStamp))}");
        }





        private Button Btn;
        private CheckBox KeystoneToggleBox;
        private TextBox CustomGP4PathBox;
        private Label Title;
        private Button CloseBtn;
        private CheckBox IgnoreOutputBox;
        private TextBox CustomPKGPathBox;
        private TextBox textBox1;
        private GroupBox MainBox;
    }
}
