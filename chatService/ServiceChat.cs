using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chatService
{
 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<Client> clients = new List<Client>();
        private List<string> messagesHistory = new List<string>();
        private int next_id = 1;
        public (int,List<string>) Connection(string name)
        {
            var curClient = new Client()
            {
                id = next_id,
                name = name,
                operationContext = OperationContext.Current
            };
            next_id++;
            clients.Add(curClient);
            return (curClient.id,messagesHistory);
        }

        public void Disconnection(int id)
        {
            var curClient = clients.FirstOrDefault(u => u.id == id);
            if (curClient != null)                        
                clients.Remove(curClient);            
        }

        public void SendMsg(int id, string msg)
        {
            var answer = DateTime.Now.ToShortTimeString() + " ";
            var curClient = clients.FirstOrDefault(u => u.id == id);
            if (curClient != null) answer += curClient.name;
            answer += ": " + msg;

            messagesHistory.Add(answer);
            if(clients.Count>0)
                foreach (var client in clients)
                {
                    client.operationContext.GetCallbackChannel<IServiceChatCallback>().CallbackMsg(answer);
                }
        }
    }
}
