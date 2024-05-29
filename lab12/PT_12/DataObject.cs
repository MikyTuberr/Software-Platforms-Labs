using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class DataObject
    {
        public DateTime Date { get; set; }
        public string ClientName { get; set; }

        public int NumberOfMessage { get; set; }
    }

}
