﻿using AutoUpdaterDotNET;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Windows.Forms;
using xCheatsFunctions;

namespace xCheats_Launcher
{
    public partial class Launcher : Form
    {
        static bool isAdmin;
        private bool isFunction1Active;
        private bool isFunction2Active;
        private bool isFunction3Active;
        private Timer clickTimer;
        private bool isDragging = false;
        private int mouseX, mouseY;
        static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string appFolderPath = Path.Combine(appDataPath, "xCheats");
        static string configFilePath = Path.Combine(appFolderPath, "data\\Config\\config.ini");
        IniConfig config = new IniConfig();
        public Launcher()
        {
            System.Threading.Thread.Sleep(500);
            config.Check(configFilePath);
            //AutoUpdater.Start("https://xcheats.dovahkiinlounge.de/update.php");
            //AutoUpdater.ShowSkipButton = false;
            //AutoUpdater.LetUserSelectRemindLater = false;
            //AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Hours;
            //AutoUpdater.RemindLaterAt = 1;
            InitializeComponent();
            OffTggl.ForeColor = Color.Red;
            clickTimer = new Timer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += ClickTimer_Tick;
            config.Load(configFilePath);
            bool isOfflineMode = GlobalSettings.isOfflineMode;
            bool isAdminMode = GlobalSettings.AdminMode;
            bool backgroundMode = GlobalSettings.BackgroundWork;

            if (isOfflineMode && isAdminMode && backgroundMode)
            {
                // Both are true
                isFunction1Active = true;
                isFunction2Active = true;
                isFunction3Active = true;
                OffTggl.Checked = true;
                AdminTggl.Checked = true;
                BGTggl.Checked = true;
                UpdateOfflineLabel(true);
                UpdateAdmin(true);
                UpdateBackground(true);
            }
            else if (!isOfflineMode && !isAdminMode && !backgroundMode)
            {
                // Both are false
                isFunction1Active = false;
                isFunction2Active = false;
                isFunction3Active = false;
                OffTggl.Checked = false;
                AdminTggl.Checked = false;
                BGTggl.Checked = false;
                UpdateAdmin(false);
                UpdateOfflineLabel(false);
                UpdateBackground(false);
            }
            else
            {
                // One is true, the other is false
                isFunction1Active = isOfflineMode;
                isFunction2Active = isAdminMode;
                isFunction3Active = backgroundMode;
                OffTggl.Checked = isOfflineMode;
                AdminTggl.Checked = isAdminMode;
                BGTggl.Checked = backgroundMode;
                UpdateAdmin(isAdminMode);
                UpdateOfflineLabel(isOfflineMode);
                UpdateBackground(backgroundMode);
            }
            this.ShowInTaskbar = true;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - mouseX;
                int deltaY = e.Y - mouseY;

                Location = new System.Drawing.Point(Location.X + deltaX, Location.Y + deltaY);
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            OffTggl.Enabled = true;
            AdminTggl.Enabled = true;
            BGTggl.Enabled = true;
            clickTimer.Stop();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            string arguments = "-u6sq8#bhysa68@1c0&$";
            string mainAppPath = Environment.CurrentDirectory + "\\xCheats.exe";

            ProcessStartInfo startInfo = new ProcessStartInfo(mainAppPath);
            startInfo.Arguments = arguments;
            bool isAdmin = GlobalSettings.AdminMode;
            if (isAdmin)
            {
                startInfo.Verb = "runas"; // This indicates that the process should be run as an administrator
            }
            try
            {
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
        }


        private void zeroitLollipopButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);    
        }

        private void OffTggl_Click(object sender, EventArgs e)
        {
            isFunction1Active = !isFunction1Active;
            OffTggl.Enabled = false;
            clickTimer.Start();
            OffTggl.Checked = isFunction1Active;
            UpdateOfflineLabel(OffTggl.Checked);
            config.SetValue("Settings", "OfflineMode", OffTggl.Checked.ToString().ToLower());
            config.Save(configFilePath);
        }

        private void AdminTggl_Click(object sender, EventArgs e)
        {
            isFunction2Active = !isFunction2Active;
            AdminTggl.Enabled = false;
            clickTimer.Start();
            AdminTggl.Checked = isFunction2Active;
            UpdateAdmin(AdminTggl.Checked);
            config.SetValue("Settings", "AdminMode", AdminTggl.Checked.ToString().ToLower());
            config.Save(configFilePath);
        }

        private void UpdateOfflineLabel(bool isOfflineMode)
        {
            if (isOfflineMode)
            {
                OffTggl.Text = "Offline Enabled";
                OffTggl.ForeColor = Color.Red;
            }
            else
            {
                OffTggl.Text = "Online(Click Here to go Offline)";
                OffTggl.ForeColor = Color.Green;
            }
        }

        private void BGTggl_Click(object sender, EventArgs e)
        {
            BGTggl.Enabled = false;
            isFunction3Active = !isFunction3Active;
            clickTimer.Start();
            BGTggl.Checked = isFunction3Active;
            UpdateBackground(BGTggl.Checked);
            config.SetValue("Settings", "BackgroundWorker", BGTggl.Checked.ToString().ToLower());
            config.Save(configFilePath);
        }

        private void UpdateAdmin(bool isAdminMode)
        {
            if (isAdminMode)
            {
                AdminTggl.Text = "Admin Enabled";
                StartBtn.Text = "Start xCheats Admin";
                AdminTggl.ForeColor = Color.Green;
            }
            else
            {
                AdminTggl.Text = "Admin Disabled";
                StartBtn.Text = "Start xCheats";
                AdminTggl.ForeColor = Color.Red;
            }
        }

        private void UpdateBackground(bool backgroundMode)
        {
            if (backgroundMode)
            {
                BGTggl.Text = "BG Enabled";
                BGTggl.ForeColor = Color.Green;
            }
            else
            {
                BGTggl.Text = "BG Disabled";
                BGTggl.ForeColor = Color.Red;
            }
        }
    }
}
