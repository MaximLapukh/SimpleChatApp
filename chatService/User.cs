using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace chatService
{
    class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
