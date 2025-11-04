using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Net;                    // <--- TARVITAAN NETTISKRIPEILLE
using Newtonsoft.Json;
using XenoUI;

namespace Sauli616_Executor
{
    public partial class Form1 : Form
    {
        private Point mouseLocation;
        private const string ScriptsFolder = @"D:\Sauli616 Executor\Scripts";

        public Form1()
        {
            InitializeComponent();
            ClientsWindow.Initialize(false);

            // Ikkunan siirto
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;

            // Luo Scripts-kansio
            EnsureScriptsFolderExists();

            // Poistaa sinisen fokuskehyksen kaikista napeista
            RemoveButtonFocusOutline();
        }

        #region Ikkunan siirto

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point screenPos = Control.MousePosition;
                this.Location = new Point(screenPos.X - mouseLocation.X, screenPos.Y - mouseLocation.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) { }

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
            try
            {
                if (!Directory.Exists(ScriptsFolder))
                    Directory.CreateDirectory(ScriptsFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create folder:\n{ScriptsFolder}\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RemoveButtonFocusOutline()
        {
            Action<Button> removeOutline = btn =>
            {
                btn.GotFocus += (s, e) => { this.ActiveControl = null; };
            };

            removeOutline(button1);  // Execute
            removeOutline(button2);  // Inject
            removeOutline(button3);  // Close
            removeOutline(button4);  // Minimize
            removeOutline(button6);  // Open File
            removeOutline(button7);  // Save File
            removeOutline(button8);  // Infinite Yield
        }

        private List<int> GetReadyClientPIDs()
        {
            var pids = new List<int>();
            try
            {
                IntPtr ptr = GetClients();
                if (ptr == IntPtr.Zero) return pids;

                string json = Marshal.PtrToStringAnsi(ptr);
                var list = JsonConvert.DeserializeObject<List<List<object>>>(json);

                if (list != null)
                {
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
            }
            catch { }
            return pids;
        }

        private void ExecuteScriptOnClients(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
            {
                MessageBox.Show("Script is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                MessageBox.Show($"Executed on {pids.Count} client(s).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            try { Attach(); MessageBox.Show("Attach called.", "Attach", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            catch (Exception ex) { MessageBox.Show($"Attach failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                try
                {
                    richTextBox1.Text = File.ReadAllText(ofd.FileName);
                    this.Text = $"Sauli616 Executor - {Path.GetFileName(ofd.FileName)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Open failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    File.WriteAllText(sfd.FileName, richTextBox1.Text);
                    this.Text = $"Sauli616 Executor - {Path.GetFileName(sfd.FileName)}";
                    MessageBox.Show($"Saved:\n{sfd.FileName}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Save failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        // INFINITE YIELD -NAPPI
        private void button8_Click(object sender, EventArgs e)
        {
            const string iyUrl = "https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source";

            try
            {
                using (var client = new WebClient())
                {
                    string script = client.DownloadString(iyUrl);
                    ExecuteScriptOnClients(script);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load Infinite Yield:\n{ex.Message}", "Network Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
