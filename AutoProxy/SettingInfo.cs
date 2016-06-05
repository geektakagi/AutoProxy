using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;

namespace AutoProxy
{
    class SettingInfo
    {
        public string sSSID = "";
        public string sProxyServerAddr = "";
        public string sPort = "";

        public SettingInfo(string SSID) {
            XmlDocument myXmlDocument = new XmlDocument();
            try {                
                myXmlDocument.Load(@"C:\\Users\Kosuke\Source\Repos\AutoProxy\AutoProxy\proxys.xml");
            }
            catch (Exception) {
                Debug.WriteLine("xml file load error.");
                throw;
            }

            string sNodeKey = @"//SSID[@ssid='" + SSID + "']/name";
            // sNodeKey = @"//SSID[@ssid='iZb4q@e-dot1x']/name";
            XmlNodeList NodeList = myXmlDocument.SelectNodes(sNodeKey);
            if (NodeList != null)
            {
                sSSID = SSID;             

                Debug.WriteLine(NodeList.Count.ToString()); // 取得したノード数を出力
                for (int i = 0; i < NodeList.Count; i++)
                {
                    Debug.WriteLine(NodeList[i].InnerXml);  // 取得したノードの内容を出力
                }
            }
            else {

            }

        }
    }
}