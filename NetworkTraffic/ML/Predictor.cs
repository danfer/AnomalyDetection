using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using NetworkTraffic.ML.Base;
using NetworkTraffic.ML.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetworkTraffic.ML
{
    public class Predictor : BaseML
    {
        public void Predict(string inputDataFile)
        {
            if (!File.Exists(ModelPath))
            {
                Console.WriteLine($"Failed to find model at {ModelPath}");

                return;
            }

            if (!File.Exists(inputDataFile))
            {
                Console.WriteLine($"Failed to find input data at {inputDataFile}");

                return;

            }
            ITransformer mlModel = MlContext.Model.Load(ModelPath, out var modelInputSchema);

            if (mlModel == null)
            {
                Console.WriteLine("Failed to load model");

                return;
            }
            // Using ML Transforms TimeSeries
            var predictionEngine = mlModel.CreateTimeSeriesEngine<NetworkTrafficHistory, NetworkTrafficPrediction>(MlContext);

            // var predictionEngine = MlContext.Model.CreatePredictionEngine<NetworkTrafficHistory, NetworkTrafficPrediction>(mlModel);
           
              var inputData =
                MlContext.Data.LoadFromTextFile<NetworkTrafficHistory>(inputDataFile, separatorChar: ',');

            var rows = MlContext.Data.CreateEnumerable<NetworkTrafficHistory>(inputData, false);

            Console.WriteLine($"Based on input file ({inputDataFile}):");

            foreach (var row in rows)
            {
                var prediction = predictionEngine.Predict(row);

                Console.Write($"HOST: {row.HostMachine} TIMESTAMP: {row.Timestamp} TRANSFER: {row.BytesTransferred} ");
                Console.Write($"ALERT: {prediction.Prediction[0]} SCORE: {prediction.Prediction[1]:f2} P-VALUE: {prediction.Prediction[2]:F2}{Environment.NewLine}");
            }
        }
    }
}
