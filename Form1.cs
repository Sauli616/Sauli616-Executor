using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using XenoUI;

namespace Sauli616_Executor
{
    public partial class Form1 : Form
    {
        private Point mouseLocation;
        private const string ScriptsFolder = @"D:\Sauli616 Executor\Scripts";

        // Fullscreen toggle -muuttujat
        private bool isFullscreen = false;
        private FormWindowState previousWindowState;
        private FormBorderStyle previousBorderStyle;
        private Rectangle previousBounds;

        public Form1()
        {
            InitializeComponent();
            ClientsWindow.Initialize(false);

            // Salli näppäimistökuuntelu
            this.KeyPreview = true;

            // Ikkunan siirto
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;

            // F11 = Fullscreen toggle
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F11)
                {
                    ToggleFullscreen();
                    e.Handled = true;
                }
            };

            EnsureScriptsFolderExists();
            RemoveButtonFocusOutline();
        }

        #region Ikkunan siirto
        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isFullscreen)
                mouseLocation = e.Location;
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isFullscreen)
            {
                Point screenPos = Control.MousePosition;
                this.Location = new Point(screenPos.X - mouseLocation.X, screenPos.Y - mouseLocation.Y);
            }
        }

        private void Form1_MouseUp(object? sender, EventArgs e) { }
        #endregion

        #region Fullscreen Toggle
        private void ToggleFullscreen()
        {
            if (!isFullscreen)
            {
                // Tallenna nykyinen tila
                previousWindowState = this.WindowState;
                previousBorderStyle = this.FormBorderStyle;
                previousBounds = this.Bounds;

                // Siirry fullscreen
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Bounds = Screen.FromHandle(this.Handle).Bounds;
                this.TopMost = true;
            }
            else
            {
                // Palaa normaaliin
                this.TopMost = false;
                this.FormBorderStyle = previousBorderStyle;
                this.Bounds = previousBounds;
                this.WindowState = previousWindowState;
            }

            isFullscreen = !isFullscreen;
        }
        #endregion

        #region Xeno.dll
        [DllImport("Xeno.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern IntPtr GetClients();

        [DllImport("Xeno.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern void Execute(byte[] script, int[] PIDs, int count);

        [DllImport("Xeno.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void Attach();
        #endregion

        #region Apufunktiot
        private void EnsureScriptsFolderExists()
        {
            if (!Directory.Exists(ScriptsFolder))
                Directory.CreateDirectory(ScriptsFolder);
        }

        private void RemoveButtonFocusOutline()
        {
            Action<Button> removeOutline = btn =>
            {
                btn.GotFocus += (s, e) => this.ActiveControl = null;
            };
            foreach (var btn in new[] { button1, button2, button3, button4, button6, button7, button8, button9 })
                removeOutline(btn);
        }

        private List<int> GetReadyClientPIDs()
        {
            var pids = new List<int>();
            try
            {
                IntPtr ptr = GetClients();
                if (ptr == IntPtr.Zero) return pids;

                string json = Marshal.PtrToStringAnsi(ptr) ?? "";
                var list = JsonConvert.DeserializeObject<List<List<object>>>(json);

                if (list == null) return pids;

                foreach (var client in list)
                {
                    if (client.Count >= 4)
                    {
                        int pid = Convert.ToInt32(client[0]);
                        int state = Convert.ToInt32(client[3]);
                        if (state == 3) pids.Add(pid);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Client scan failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return pids;
        }

        private void ExecuteScriptOnClients(string script)
        {
            if (string.IsNullOrWhiteSpace(script)) return;

            var pids = GetReadyClientPIDs();
            if (pids.Count == 0)
            {
                MessageBox.Show("No ready clients. Attach first!", "No Clients", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(script + "\0");
                Execute(bytes, pids.ToArray(), pids.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Execution failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Tapahtumat
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Sauli616 Executor";
        }

        private void button2_Click(object sender, EventArgs e) // Inject
        {
            try { Attach(); }
            catch (Exception ex)
            {
                MessageBox.Show($"Attach failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Execute
        {
            ExecuteScriptOnClients(richTextBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e) // Open File
        {
            var ofd = new OpenFileDialog
            {
                Title = "Open Script",
                Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = Directory.Exists(ScriptsFolder) ? ScriptsFolder : Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(ofd.FileName);
                this.Text = $"Sauli616 Executor - {Path.GetFileName(ofd.FileName)}";
            }
        }

        private void button7_Click(object sender, EventArgs e) // Save File
        {
            var sfd = new SaveFileDialog
            {
                Title = "Save Script",
                Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                DefaultExt = "lua",
                FileName = "script.lua",
                InitialDirectory = ScriptsFolder
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, richTextBox1.Text);
                this.Text = $"Sauli616 Executor - {Path.GetFileName(sfd.FileName)}";
            }
        }

        private void button4_Click(object sender, EventArgs e) // Minimize
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e) // Close
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void button8_Click(object sender, EventArgs e) // Infinite Yield
        {
            const string iyUrl = "https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source";
            try
            {
                using var client = new WebClient();
                string script = client.DownloadString(iyUrl);
                ExecuteScriptOnClients(script);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load Infinite Yield:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e) // Fullscreen Toggle
        {
            ToggleFullscreen();
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(1000);
                }
                catch { }
            }
        }
    }
}
