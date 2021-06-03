using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.ML;
using CourseRecommendationSystemML.Model;

namespace CourseRecommendationSystemML.Model
{
    public class ConsumeModel
    {
        private Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine;

        public string MLNetModelPath = Path.GetFullPath("MLModel.zip");

        public ConsumeModel()
        {
            PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);
        }

        public ModelOutput Predict(ModelInput input)
        {
            ModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            MLContext mlContext = new MLContext();

            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}
