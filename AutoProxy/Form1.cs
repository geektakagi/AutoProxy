using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoProxy
{
    public partial class AutoProxySetting : Form
    {
        public AutoProxySetting() {
            InitializeComponent();
        }

        private void ProxyEnable(object sender, EventArgs e) {
            SetReg_ProxyEnable(true);
        }

        private void btnProxyDisable_Click(object sender, EventArgs e) {
            SetReg_ProxyEnable(false);
        }

        private Boolean SetReg_ProxyEnable(Boolean flag)
        {
            const int ProxyEnable   = 1;
            const int ProxyDisable  = 0;

            int iProxyEnable = (flag == true) ? ProxyEnable : ProxyDisable;

            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyEnable", iProxyEnable, Microsoft.Win32.RegistryValueKind.DWord);

            regkey.Close();
            return true;
        }

        private Boolean SetReg_ProxyServer(String ServerAddr)
        {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyServer", ServerAddr, Microsoft.Win32.RegistryValueKind.String);

            regkey.Close();
            return true;
        }

        private Boolean SetReg_ProxyOverride(String Addr)
        {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyOverride", Addr, Microsoft.Win32.RegistryValueKind.String);

            regkey.Close();
            return true;
        }
    }
}
