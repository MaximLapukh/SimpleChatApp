using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceChat" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServiceChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        (int, List<string>) Connection(string name);
        [OperationContract]
        void Disconnection(int id);
        [OperationContract(IsOneWay = true)]
        void SandMsg(int id,string msg);
    }
    public interface IServiceChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void CallbackMsg(string msg);
    }
}
