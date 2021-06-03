using System;
using System.Collections.Generic;
using CourseRecommendationSystemML.Model;

namespace CourseRecommendationSystemML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelBuilder.CreateModel();
            Console.WriteLine("Model Created");
        }
    }

    class Recommendations
    {
        SortedDictionary<float, string> CoursesList = new SortedDictionary<float, string>();
        string[] Courses = { "CSC101", "EEE121", "HUM100", "HUM110", "MTH104", "CSC102", "CSC103", "HUM102", "HUM111", "MTH105", "CSC110", "CSC241", "EEE241" };

        public void PrintAllRecommendations()
        {
            this.PredictCourses(1);
            foreach (KeyValuePair<float, string> kvp in CoursesList)
            {
                Console.WriteLine("Course {0} with {1} GPA", kvp.Value, kvp.Key);
            }
        }

        public void PredictCourses(int id)
        {
            foreach (var course in Courses)
            {
                ModelInput input = new ModelInput()
                {
                    StudentId = id,
                    CourseId = course
                };
                CoursesList.Add(ConsumeModel.Predict(input).Score, course);
            }
        }
    }
}
