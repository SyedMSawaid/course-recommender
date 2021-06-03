using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using CourseRecommendationSystemML.Model;
using System.Data.Common;
using Npgsql;

namespace API
{
    public class MLModelBuilder
    {
        private string CONNECTION_STRING = @"Server=localhost; Port=5432; Database=apidatabase; Username=postgres; Password=admin";
        private string MODEL_FILE = Path.GetFullPath("MLModel.zip");

        private MLContext mlContext = new MLContext(seed: 1);

        public void CreateModel()
        {

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<ModelInput>();

            string sqlCommand = "SELECT \"StudentId\", \"CourseId\", \"Marks\" FROM public.\"Enrollments\"";

            var connection = new NpgsqlConnection(CONNECTION_STRING);

            var factor = DbProviderFactories.GetFactory(connection);

            DatabaseSource dbSource = new DatabaseSource(factor, CONNECTION_STRING, sqlCommand);

            IDataView data = loader.Load(dbSource);

            IEstimator<ITransformer> trainingPipeline = BuildTrainingPipeline(mlContext);

            ITransformer mlModel = TrainModel(mlContext, data, trainingPipeline);

            Evaluate(mlContext, data, trainingPipeline);

            SaveModel(mlContext, mlModel, MODEL_FILE, data.Schema);
        }

        public IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("StudentId", "StudentId")
                                      .Append(mlContext.Transforms.Conversion.MapValueToKey("CourseId", "CourseId"));

            var trainer = mlContext.Recommendation().Trainers.MatrixFactorization(labelColumnName: @"Marks", matrixColumnIndexColumnName: @"StudentId", matrixRowIndexColumnName: @"CourseId");

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        public ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine("=============== Training  model ===============");

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine("=============== End of training process ===============");
            return model;
        }

        private void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
        }

        private void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema)
        {
            Console.WriteLine($"=============== Saving the model  ===============");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine("The model is saved to {0}", GetAbsolutePath(modelRelativePath));
        }

        public string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        public void PrintRegressionMetrics(RegressionMetrics metrics)
        {
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Metrics for Recommendation model      ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       LossFn:        {metrics.LossFunction:0.##}");
            Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Absolute loss: {metrics.MeanAbsoluteError:#.##}");
            Console.WriteLine($"*       Squared loss:  {metrics.MeanSquaredError:#.##}");
            Console.WriteLine($"*       RMS loss:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }

        public void PrintRegressionFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<RegressionMetrics>> crossValidationResults)
        {
            var L1 = crossValidationResults.Select(r => r.Metrics.MeanAbsoluteError);
            var L2 = crossValidationResults.Select(r => r.Metrics.MeanSquaredError);
            var RMS = crossValidationResults.Select(r => r.Metrics.RootMeanSquaredError);
            var lossFunction = crossValidationResults.Select(r => r.Metrics.LossFunction);
            var R2 = crossValidationResults.Select(r => r.Metrics.RSquared);

            Console.WriteLine($"*************************************************************************************************************");
            Console.WriteLine($"*       Metrics for Recommendation model      ");
            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"*       Average L1 Loss:       {L1.Average():0.###} ");
            Console.WriteLine($"*       Average L2 Loss:       {L2.Average():0.###}  ");
            Console.WriteLine($"*       Average RMS:           {RMS.Average():0.###}  ");
            Console.WriteLine($"*       Average Loss Function: {lossFunction.Average():0.###}  ");
            Console.WriteLine($"*       Average R-squared:     {R2.Average():0.###}  ");
            Console.WriteLine($"*************************************************************************************************************");
        }
    }
}
