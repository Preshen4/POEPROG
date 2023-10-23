using NovelNestLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovelNestLibraryAPI.Services
{

    public class IdentifyingAreaService
    {
        private readonly Dictionary<string, string> quizData = new Dictionary<string, string>
        {
            { "000", "General Knowledge" },
            { "100", "Philosophy" },
            { "200", "Religion" },
            { "300", "Social Science" },
            { "400", "Languages" },
            { "500", "Science" },
            { "600", "Technology" },
            { "700", "Arts and Recreation" },
            { "800", "Literature" },
            { "900", "History" }
        };

        public (List<string> questions, List<string> answers) GetQuizQuestions()
        {
            Random _random = new Random();
            List<string> questions = new List<string>();
            List<string> answers = new List<string>();

            if (_random.Next(0, 2) == 0)
            {
                questions = GetDescriptionQuestions();
                answers = GetDescriptionAnswers(questions);
            }
            else
            {
                questions = GetCallNumberQuestions();
                answers = GetCallNumberAnswers(questions);
            }

            return (questions, answers);
        }

        private List<string> GetDescriptionQuestions ()
        {
            return quizData.Values.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        }

        private List<string> GetDescriptionAnswers (List<string> questions)
        {
            List<string> answers = new List<string>();
            foreach (string question in questions)
            {
                answers.Add(quizData.FirstOrDefault(x => x.Value == question).Key);
            }

            // Generate a list of values that are not in the questions list
            List<string> remainingKeys = quizData.Keys.Where(key => !answers.Contains(key)).ToList();

            // Shuffle the remaining values
            remainingKeys = remainingKeys.OrderBy(x => Guid.NewGuid()).ToList();

            // Add the first 3 values from the shuffled remaining values to the answers list
            answers.AddRange(remainingKeys.Take(3));

            // Shuffle the entire answers list to randomize the order
            answers = answers.OrderBy(x => Guid.NewGuid()).ToList();

            return answers;
        }

        private List<string> GetCallNumberQuestions()
        {
            return quizData.Keys.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        }

        private List<string> GetCallNumberAnswers(List<string> questions)
        {
            List<string> answers = new List<string>();
            foreach (string question in questions)
            {
                answers.Add(quizData[question]);
            }

            // Generate a list of values that are not in the questions list
            List<string> remainingValues = quizData.Values.Where(value => !answers.Contains(value)).ToList();

            // Shuffle the remaining values
            remainingValues = remainingValues.OrderBy(x => Guid.NewGuid()).ToList();

            // Add the first 3 values from the shuffled remaining values to the answers list
            answers.AddRange(remainingValues.Take(3));

            // Shuffle the entire answers list to randomize the order
            answers = answers.OrderBy(x => Guid.NewGuid()).ToList();

            return answers;
        }

        public bool CheckCallNumberAnswers(Dictionary<string, string> userResponses)
        {
            foreach (KeyValuePair<string, string> response in userResponses)
            {
                if (quizData[response.Key] != response.Value)
                {
                    return false;
                }
            }

            return true;
        }
        public bool CheckDescriptionAnswers(Dictionary<string, string> userResponses)
        {
            foreach (KeyValuePair<string, string> response in userResponses)
            {
                if (quizData[response.Value] != response.Key)
                {
                    return false;
                }
            }

            return true;
        }

    }



}
