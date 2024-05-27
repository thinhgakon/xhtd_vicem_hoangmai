using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;

namespace HMXHTD
{
    class clsConfigXML
    {
        #region method Declare ...
        private XmlDocument xmlDoc = new XmlDocument();
        private string sFileName = "Vicem.xml";
        #endregion

        #region method UpdateKey
        public void UpdateKey(string strKey, string newValue)
        {
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + sFileName);
            if (!KeyExists(strKey))
                throw new ArgumentNullException("Key", "<" + strKey + "> does not exist in the configuration. Update failed.");
            XmlNode appSettingsNode =
               xmlDoc.SelectSingleNode("configuration/appSettings");
            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    childNode.Attributes["value"].Value = newValue;
            }
            xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + sFileName);
        }
        #endregion

        #region method KeyExists
        private bool KeyExists(string strKey)
        {
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + sFileName);
            XmlNode appSettingsNode =
              xmlDoc.SelectSingleNode("configuration/appSettings");
            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    return true;
            }
            return false;
        }
        #endregion

        #region method GetKey
        public string GetKey(string strKey)
        {
            try
            {
                string sValue = "";
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + sFileName);
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        sValue = childNode.Attributes["value"].Value;
                        break;
                    }
                }
                return sValue;
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}
