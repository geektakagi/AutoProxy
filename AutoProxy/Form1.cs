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
        public AutoProxySetting()
        {
            InitializeComponent();
        }

        private void ProxyEnable(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyEnable", 1);

            regkey.Close();
        }

        private void btnProxyDisable_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyEnable", 0);

            regkey.Close();
        }
    }
}
