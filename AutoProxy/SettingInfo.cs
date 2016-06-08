using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;

namespace AutoProxy {
    class SettingInfo {     
        public String   sSSID = "";
        public String   sProxyServerAddr = "";
        public String   sPort = "80";

        public enum SettingKeys {
            name
            , proxy
            , port
        }

        public SettingInfo() {

        }

        public SettingInfo(String SSID) {
            XmlDocument myXmlDocument = new XmlDocument();
            try {                
                myXmlDocument.Load(@"proxys.xml");

                String sNodeKey = @"//SSID[@ssid='" + SSID + "']";
                XmlNodeList NodeList = myXmlDocument.SelectNodes(sNodeKey);

                if (NodeList != null) {
                    for (int i = 0; i < NodeList.Count; i++) {
                        sNodeKey = @"//SSID[@ssid='" + SSID + "']/name";
                        NodeList = myXmlDocument.SelectNodes(sNodeKey);
                        sSSID = NodeList[0].InnerText;
                        Debug.WriteLine("SettingInfo LoadedSetting sSSID:" + NodeList[0].InnerText);

                        sNodeKey = @"//SSID[@ssid='" + SSID + "']/proxy";
                        NodeList = myXmlDocument.SelectNodes(sNodeKey);
                        sProxyServerAddr = NodeList[0].InnerText;
                        Debug.WriteLine("SettingInfo LoadedSetting sProxyServerAddr:" + NodeList[0].InnerText);

                        sNodeKey = @"//SSID[@ssid='" + SSID + "']/port";
                        NodeList = myXmlDocument.SelectNodes(sNodeKey);
                        sPort = NodeList[0].InnerText;
                        Debug.WriteLine("SettingInfo LoadedSetting sPort:" + NodeList[0].InnerText);
                    }
                }
            }
            catch (Exception) {
                Debug.WriteLine("xml file load error.");
                throw;
            }            
        }

        public Boolean WriteSetting(SettingKeys Key, String sValue) {
            return true;
        }
    }
}