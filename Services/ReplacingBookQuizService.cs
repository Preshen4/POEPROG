using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NovelNestLibraryAPI.Models;
using System.Text;

namespace NovelNestLibraryAPI.Services
{
    public class ReplacingBookQuizService
    {
        private readonly Random _random = new Random();
        public ReplacingBookQuizService()
        {

        }
        public async Task<ReplacingBookQuiz> GenerateQuiz()
        {
            ReplacingBookQuiz quiz = new ReplacingBookQuiz();
            await Task.Run(() =>
            {
                quiz = new ReplacingBookQuiz()
                {
                    CallNumber = GenerateRandomCallNumber(),
                    NumInCorrectOrder = 0,
                    TotalPoints = 150
                };
            });

            return quiz;
        }

        public async Task<List<string>> GenerateCorrectOrder(List<string> callNumber)
        {
            // Create a copy of callNumber before sorting
            List<string> sortedCallNumber = callNumber.ToList();

            for (int i = 1; i < sortedCallNumber.Count; i++)
            {
                string currentCallNumber = sortedCallNumber[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(sortedCallNumber[j], currentCallNumber) > 0)
                {
                    sortedCallNumber[j + 1] = sortedCallNumber[j];
                    j--;
                }

                sortedCallNumber[j + 1] = currentCallNumber;
            }

            return sortedCallNumber; // Return the sorted copy
        }

        private List<string> GenerateRandomCallNumber()
        {
            List<string> callNumber = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                callNumber.Add($"{GenerateRandomNumber():F2} {GenerateRandomAuthorInitials()}");
            }
            return callNumber;
        }
        private string GenerateRandomNumber()
        {
            int integerPart = _random.Next(1000);
            int decimalPart = _random.Next(100);

            // Format the integer and decimal parts with leading zeros
            string formattedIntegerPart = integerPart.ToString("D3");
            string formattedDecimalPart = decimalPart.ToString("D2");

            return $"{formattedIntegerPart}.{formattedDecimalPart}";
        }
        private string GenerateRandomAuthorInitials()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(new[] { chars[_random.Next(chars.Length)], chars[_random.Next(chars.Length)], chars[_random.Next(chars.Length)] });
        }
        private int GetNumInCorrectOrderPoints(ReplacingBookQuiz quiz)
        {
            switch (quiz.NumInCorrectOrder)
            {
                case 10:
                    return 25;
                case 9:
                    return 18;
                case 8:
                    return 15;
                case 7:
                    return 12;
                case 6:
                    return 10;
                case 5:
                    return 8;
                case 4:
                    return 6;
                case 3:
                    return 4;
                case 2:
                    return 2;
                case 1:
                    return 1;
                case 0:
                default:
                    return -50;
            }
        }
        public async Task<ReplacingBookQuiz> CalculatePoints(ReplacingBookQuiz quiz)
        {
            List<string> UserAnswer = quiz.CallNumber.ToList();
            List<string> CorrectAnswer = await GenerateCorrectOrder(quiz.CallNumber);


            foreach (var values in CorrectAnswer.Zip(UserAnswer, (x, y) => new { CorrectAnswer = x, UserAnswer = y }))
            {
                if (values.CorrectAnswer == values.UserAnswer)
                {
                    quiz.TotalPoints += 10;
                    quiz.NumInCorrectOrder++;
                }
                else
                {
                    quiz.TotalPoints -= 10;
                }
            }

            quiz.TotalPoints += GetNumInCorrectOrderPoints(quiz);

            return quiz;
        }
    }
}
