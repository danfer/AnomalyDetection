using Microsoft.ML;
using NetworkTraffic.ML.Base;
using NetworkTraffic.ML.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetworkTraffic.ML
{
    public class Trainer: BaseML
    {
        private const int PvalueHistoryLength = 3;
        private const int SeasonalityWindowSize = 3;
        private const int TrainingWindowSize = 7;
        private const int Confidence = 98;
        public void Train(string trainingFileName)
        {
            if (!File.Exists(trainingFileName))
            {
                Console.WriteLine($"Failed to find training data file ({trainingFileName}");

                return;
            }
            var trainingDataView = GetDataView(trainingFileName);

            var trainingPipeLine = MlContext.Transforms.DetectSpikeBySsa(
                nameof(NetworkTrafficPrediction.Prediction),
                nameof(NetworkTrafficHistory.BytesTransferred),
                confidence: Confidence,
                pvalueHistoryLength: PvalueHistoryLength,
                trainingWindowSize: TrainingWindowSize,
                seasonalityWindowSize: SeasonalityWindowSize);

            ITransformer trainedModel = trainingPipeLine.Fit(trainingDataView);

            MlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);

            Console.WriteLine("Model trained");
        }
    }
}
