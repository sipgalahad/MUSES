using System.Collections.Generic;

namespace CodeX.Muses.Web.ControlPanelHQ.Program
{
    public class ClientAdapter
    {
        private Dictionary<string, Client> recipients = new Dictionary<string,Client>();

        public void SendMessage(string siteID, string type)
        {
            if (recipients.ContainsKey(siteID))
            {
                Client client = recipients[siteID];
                client.EnqueueMessage(type);
            }
        }

        public string GetMessage(string siteID)
        {
            string result = "";

            if (!recipients.ContainsKey(siteID))
                Join(siteID);

            Client client = recipients[siteID];
            result = client.DequeueMessage();

            return result;
        }

        public void Join(string siteID)
        {
            recipients[siteID] = new Client();
        }
        public void Fork(string siteID)
        {
            recipients.Remove(siteID);
        }

        public static ClientAdapter Instance = new ClientAdapter();
        private ClientAdapter() { }
    }
}