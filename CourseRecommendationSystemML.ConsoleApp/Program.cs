using System;
using System.Collections.Generic;
using CourseRecommendationSystemML.Model;

namespace CourseRecommendationSystemML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                Student_id = 1F,
                Course = "EL101",
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Gpa with predicted Gpa from sample data...\n\n");
            Console.WriteLine($"Student_id: {sampleData.Student_id}");
            Console.WriteLine($"Course: {sampleData.Course}");
            Console.WriteLine($"\n\nPredicted Gpa: {predictionResult.Score}\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
            */

            Recommendations r = new Recommendations();
            r.PrintAllRecommendations();
        }
    }

    class Recommendations
    {
        SortedDictionary<float, string> CoursesList = new SortedDictionary<float, string>();
        string[] Courses = new string[] { "CS201", "CS202", "HM201", "IT202", "IT205", "MA201", "CS205", "CS206", "HM202", "HM203", "IT201", "IT203", "IT204" };

        public void PrintAllRecommendations()
        {
            this.PredictCourses(52);
            foreach(KeyValuePair<float, string> kvp in CoursesList)
            {
                Console.WriteLine("Course {0} with {1} GPA", kvp.Value, kvp.Key);
            }
        }

        public void PredictCourses(int id)
        {
            foreach(var course in Courses)
            {
                ModelInput input = new ModelInput()
                {
                    Student_id = id,
                    Course = course
                };
                CoursesList.Add(ConsumeModel.Predict(input).Score, course);
            }
        }
    }
}
