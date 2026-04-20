using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class DataSubmittedEvent
    {
        public string Data { get; }

        public DataSubmittedEvent(string data)
        {
            Data = data;
        }
    }
}