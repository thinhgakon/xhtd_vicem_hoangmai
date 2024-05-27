using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace SMSVLand.Core
{
    class MobifoneSMS
    {
        #region method login
        public string login(string userName, string password, string bindMode, ref string sid)
        {
            string status = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smsbrandname.mobifone.vn/smsg/login.jsp?userName=" + userName + "&password=" + password + "&bindMode=" + bindMode);

                request.KeepAlive = false;

                // Set some reasonable limits on resources used by this request
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                // Set credentials to use for this request.
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                
                string tmpValueReturn = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                if (tmpValueReturn.Trim() != "")
                {
                    tmpValueReturn = tmpValueReturn.Replace("{", "");
                    tmpValueReturn = tmpValueReturn.Replace("}", "");
                    tmpValueReturn = tmpValueReturn.Replace("\"", "");
                    string[] tmpValueReturnArr = tmpValueReturn.Split(',');
                    if (tmpValueReturnArr.Length > 0)
                    {
                        sid = tmpValueReturnArr[0].Replace("sid:", "").Trim();
                        status = tmpValueReturnArr[1].Replace("status:", "").Trim();
                    }
                }
            }
            catch(Exception Ex)
            {
                status = Ex.Message;
            }
            return status;
        } 
        #endregion

        #region method send
        public string send(string Sid, string Sender, string Recipient, string Content, ref string returnMessage)
        {
            Recipient = "0904686357";
            string status = "";
            try
            {
                string urlToSend = "http://smsbrandname.mobifone.vn/smsg/send.jsp?sid=" + Sid + "&sender=" + Sender + "&recipient=" + Recipient + "&content=" + Content;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToSend);

                request.KeepAlive = false;

                // Set some reasonable limits on resources used by this request
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                // Set credentials to use for this request.
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string tmpValueReturn = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                if (tmpValueReturn.Trim() != "")
                {
                    tmpValueReturn = tmpValueReturn.Replace("{", "");
                    tmpValueReturn = tmpValueReturn.Replace("}", "");
                    tmpValueReturn = tmpValueReturn.Replace("\"", "");
                    string[] tmpValueReturnArr = tmpValueReturn.Split(',');
                    if (tmpValueReturnArr.Length > 0)
                    {
                        returnMessage = tmpValueReturnArr[0].Replace("message:", "").Trim();
                        status = tmpValueReturnArr[1].Replace("status:", "").Trim();
                    }
                }
            }
            catch(Exception Ex)
            {
                returnMessage = Ex.Message;
            }
            return status;
        }
        #endregion
    }
}
