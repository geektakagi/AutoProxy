using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Xml;

namespace AutoProxy
{
    public partial class AutoProxySetting : Form
    {

        public AutoProxySetting() {
            InitializeComponent();
            InTasktray();

            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));

            String[] SSIDs = NativeWifi.GetConnectedNetworkSsids().ToArray();
            if(SSIDs.Length != 0) {
                Debug.WriteLine("[Connected Network SSID]" +SSIDs[0]);
                ApplyProxySettingsToSystem(SSIDs[0]);
            }
            else {
                Debug.WriteLine("No connection");
                SetReg_ProxyEnable(false);
            }

            InitStartupCheckBox();
            SetSettingInfoToDGV();                        

        }

        private void SetSettingInfoToDGV() {
            
        }

        private void InitStartupCheckBox() {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            if (regkey != null) {
                if ("null" != (string)regkey.GetValue(Application.ProductName, "null")) {
                    StartUpEnable.Checked = true;
                }
                else {
                    StartUpEnable.Checked = false;
                }
            }
            else {
                StartUpEnable.Checked = false;
            }
            //閉じる
            regkey.Close();
        }

        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e) {
            String[] SSIDs = NativeWifi.GetConnectedNetworkSsids().ToArray();
            Debug.WriteLine("Network Status Changed");

            if (SSIDs.Length != 0) {
                ApplyProxySettingsToSystem(SSIDs[0]);
            }
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

        private void NetworkChange_NetworkAvailabilityChanged(
            object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            //Invokeが必要か確認し、必要であればInvokeを呼び出す
            if (this.InvokeRequired) {
                System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler dlgt =
                    new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(
                        NetworkChange_NetworkAvailabilityChanged);

                this.Invoke(dlgt, new object[] { sender, e });
                return;
            }

            if (e.IsAvailable) {
                this.Text = "ネットワーク接続が有効";              

                String[] SSIDs = NativeWifi.GetConnectedNetworkSsids().ToArray();
                Debug.WriteLine("Connected Network SSID:" + SSIDs[0]);
                if (!ApplyProxySettingsToSystem(SSIDs[0])) {
                    Debug.WriteLine("Failed Apply Proxy Setting to System.");
                }

            }

            else {
                this.Text = "ネットワーク接続が無効";
                Debug.WriteLine("Disabled Network");
                SetReg_ProxyEnable(false);
            }
        }

        #region "formProc"

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            this.Activate();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            String[] SSIDs = NativeWifi.GetConnectedNetworkSsids().ToArray();
            Debug.WriteLine("OK clicked. Apply Settings.");
            if (SSIDs.Length != 0) {
                ApplyProxySettingsToSystem(SSIDs[0]);
            }

            InTasktray();
        }

        #endregion

        #region "Functions"

        private void InTasktray()
        {
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
        }

        private Boolean ApplyProxySettingsToSystem(String SSID)
        {
            SettingInfo s = new SettingInfo(SSID);

            if ( s.ProxyServerAddr != "" )
            {
                SetReg_ProxyServer(s.ProxyServerAddr + ":" + s.Port);
                // SetReg_ProxyOverride();
                SetReg_ProxyEnable(true);
            }
            else
            {
                SetReg_ProxyEnable(false);
            }
            return true;
        }

        private Boolean SetReg_ProxyEnable(Boolean flag)
        {
            const int ProxyEnable = 1;
            const int ProxyDisable = 0;

            int proxyEnableOrFalse = (flag == true) ? ProxyEnable : ProxyDisable;

            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyEnable", proxyEnableOrFalse, Microsoft.Win32.RegistryValueKind.DWord);

            regkey.Close();
            Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "Set:" + flag);
            return true;
        }

        private Boolean SetReg_ProxyServer(String ServerAddr)
        {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyServer", ServerAddr, Microsoft.Win32.RegistryValueKind.String);

            regkey.Close();
            Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "Set:" + ServerAddr);
            return true;
        }

        private void SetReg_ProxyOverride(String Addr)
        {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            regkey.SetValue("ProxyOverride", Addr, Microsoft.Win32.RegistryValueKind.String);

            regkey.Close();
            Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "Set:" + Addr);
        }

        private void SetReg_WindowsStartUp(Boolean enableFlag) {
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (false == enableFlag) {
                regkey.DeleteValue(Application.ProductName);
                Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Delete key");
            }
            else {                
                regkey.SetValue(Application.ProductName, Application.ExecutablePath);
                Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + "Set:" + enableFlag);                
            }
            regkey.Close();

        }

        #endregion

        private void StartUpEnable_CheckedChanged(object sender, EventArgs e) {
            SetReg_WindowsStartUp(StartUpEnable.Checked);
        }
    }
}