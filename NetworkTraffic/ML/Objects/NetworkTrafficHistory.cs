using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTraffic.ML.Objects
{
    public class NetworkTrafficHistory
    {
        [LoadColumn(0)]
        public string HostMachine { get; set; }

        [LoadColumn(1)]
        public DateTime Timestamp { get; set; }

        [LoadColumn(2)]
        public float BytesTransferred { get; set; }
    }
}
