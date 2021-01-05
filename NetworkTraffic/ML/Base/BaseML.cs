using Microsoft.ML;
using NetworkTraffic.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetworkTraffic.ML.Base
{
    public class BaseML
    {
        protected const string FEATURES = "Features";

        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, Constants.MODEL_FILENAME);

        protected readonly MLContext MlContext;

        protected IDataView GetDataView(string fileName, bool training = true) =>
            MlContext.Data.LoadFromTextFile<NetworkTrafficHistory>(fileName, separatorChar: ',', hasHeader: false);

        protected BaseML()
        {
            MlContext = new MLContext(2020);
        }
    }
}
