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
        private List<User> clients = new List<User>();
        private List<string> messagesHistory = new List<string>();
        private int next_id = 1;
        public (int,List<string>) Connection(string name)
        {
            var user = new User()
            {
                id = next_id,
                name = name,
                operationContext = OperationContext.Current
            };
            next_id++;
            clients.Add(user);
            return (user.id,messagesHistory);
        }

        public void Disconnection(int id)
        {
            var user = clients.FirstOrDefault(u => u.id == id);
            if (user != null)                        
                clients.Remove(user);
            

        }

        public void SendMsg(int id, string msg)
        {
            var answer = DateTime.Now.ToShortTimeString() + " ";
            var user = clients.FirstOrDefault(u => u.id == id);
            if (user != null) answer += user.name;
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
