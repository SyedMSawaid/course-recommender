using Microsoft.ML.Data;

namespace CourseRecommendationSystemML.Model
{
    public class ModelInput
    {
        [ColumnName("student_id"), LoadColumn(0)]
        public float Student_id { get; set; }


        [ColumnName("course"), LoadColumn(1)]
        public string Course { get; set; }


        [ColumnName("gpa"), LoadColumn(2)]
        public float Gpa { get; set; }


    }
}
