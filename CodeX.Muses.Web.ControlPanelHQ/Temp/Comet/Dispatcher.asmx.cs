/****************************** Module Header ******************************\
* Module Name:    Dispatcher.asmx.cs
* Project:        CSASPNETReverseAJAX
* Copyright (c) Microsoft Corporation
*
* This web service is designed to be called by the Ajax client.
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
*
\*****************************************************************************/

using System.Web.Services;
using System.Web.Script.Services;

namespace CSASPNETReverseAJAX
{
    /// <summary>
    /// This web service contains methods that help dispatching events to the client.
    /// </summary>
    [WebService(Namespace = "http://tempuri2.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Dispatcher : System.Web.Services.WebService
    {
        /// <summary>
        /// Dispatch the new message event.
        /// </summary>
        /// <param name="userName">The loged in user name</param>
        /// <returns>the message content</returns>
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string WaitMessage(string userName)
        {
            return ClientAdapter.Instance.GetMessage(userName);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Message WaitMessage2(string userName)
        {
            return ClientAdapter.Instance.GetMessage2(userName);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Join(string userName)
        {
            ClientAdapter.Instance.Join(userName);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Fork(string userName)
        {
            ClientAdapter.Instance.Fork(userName);
        }
    }
}
