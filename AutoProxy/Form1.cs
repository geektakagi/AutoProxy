using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoProxy
{
    public partial class AutoProxySetting : Form
    {

        public AutoProxySetting() {
            InitializeComponent();
            InTasktray();

            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged +=
                new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(
                NetworkChange_NetworkAvailabilityChanged);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const long SC_CLOSE = 0xF060L;

            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE) {
                InTasktray();
                return;
            }

            base.WndProc(ref m);
        }

        private void ProxyEnable(object sender, EventArgs e) {
            SetReg_ProxyEnable(true);
        }

        private void btnProxyDisable_Click(object sender, EventArgs e) {
            SetReg_ProxyEnable(false);
        }

        private Boolean SetReg_ProxyEnable(Boolean flag) {
            const int ProxyEnable   = 1;
            const int ProxyDisable  = 0;

            int iProxyEnable = (flag == true) ? ProxyEnable : ProxyDisable;

            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyEnable", iProxyEnable, Microsoft.Win32.RegistryValueKind.DWord);

            regkey.Close();
            return true;
        }

        private Boolean SetReg_ProxyServer(String ServerAddr) {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyServer", ServerAddr, Microsoft.Win32.RegistryValueKind.String);

            regkey.Close();
            return true;
        }

        private Boolean SetReg_ProxyOverride(String Addr) {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyOverride", Addr, Microsoft.Win32.RegistryValueKind.String);

            regkey.Close();
            return true;
        }

        private void InTasktray()
        {
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
        }

        private void NetworkChange_NetworkAvailabilityChanged(
            object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            //Invokeが必要か確認し、必要であればInvokeを呼び出す
            if (this.InvokeRequired)
            {
                System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler dlgt =
                    new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(
                        NetworkChange_NetworkAvailabilityChanged);
                this.Invoke(dlgt, new object[] { sender, e });
                return;
            }

            if (e.IsAvailable)
            {
                this.Text = "ネットワーク接続が有効になりました。";
                if (!Debugger.IsAttached)
                    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));

            }

            else
            {
                this.Text = "ネットワーク接続が無効になりました。";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
    }
}