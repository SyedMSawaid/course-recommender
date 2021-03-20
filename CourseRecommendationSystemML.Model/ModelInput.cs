using Microsoft.ML.Data;

namespace CourseRecommendationSystemML.Model
{
    public class ModelInput
    {
        [ColumnName("StudentId"), LoadColumn(0)]
        public float StudentId { get; set; }


        [ColumnName("CourseId"), LoadColumn(1)]
        public string CourseId { get; set; }


        [ColumnName("Marks"), LoadColumn(2)]
        public float Marks { get; set; }


    }
}
