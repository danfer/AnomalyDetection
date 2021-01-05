using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTraffic.ML.Objects
{
    public class NetworkTrafficPrediction
    {
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
