using Microsoft.Build.Framework;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Tree;
using System;
using System.Collections.Generic;

namespace NovelNestLibraryAPI.Services
{
    public class FindCallNumberService
    {
        // Get current directory
        // https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getcurrentdirectory?view=net-8.0
        // Author : Microsoft
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "Data.txt");
        private Tree<CallNumberModel> tree;
        TreeNode<CallNumberModel> currentParent;
        public void CreateTree()
        {
            // Sets the tree root
            tree = new Tree<CallNumberModel>(new CallNumberModel(9999, "root"));
            // Reads all the lines in a text file and stores them in an array
            string[] lines = File.ReadAllLines(_path);
            currentParent = tree.Root;

            foreach (string line in lines)
            {
                // Get the number of tabs before the first character
                int tabs = CountTabsAtBeginning(line);
                // Split the line by the | character and remove any whitespace
                string[] parts = line.Split('|').Select(p => p.Trim()).ToArray();
                // Create a new CallNumberModel object
                CallNumberModel callNumber = new CallNumberModel(int.Parse(parts[0]), parts[1]);
                // Create a new TreeNode object
                TreeNode<CallNumberModel> parentNode = null;

                switch (tabs)
                {
                    case 0:
                        // Set the parent node to the root
                        parentNode = tree.Root;
                        break;
                    case 1:
                        // Set the parent node to the last child of the current parent
                        parentNode = currentParent.Children.LastOrDefault();
                        break;
                    case 2:
                        // Set the parent node to the last grandchild of the current parent
                        parentNode = currentParent.Children.LastOrDefault()?.Children.LastOrDefault();
                        break;
                }
                // Adds a child to the current parent
                parentNode?.AddChild(new TreeNode<CallNumberModel>(callNumber));
            }
        }
        public List<List<CallNumberModel>> CreateQuiz()
        {
            // Get all the children of the root
            var allChildren = tree.Root.Children.ToList();
            var questions = new List<List<CallNumberModel>>();

            // Shuffle the children
            allChildren.Shuffle(new Random());

            // Adds the first 4 children to the questions list
            questions.Add(CreateQuestion(allChildren.Take(4)));

            // Adds the first 4 firstChild to the questions list
            var firstChild = allChildren.FirstOrDefault();
            if (firstChild != null)
            {
                questions.Add(CreateQuestion(firstChild.Children.Take(4)));
            }

            // Adds the first 4 firstGrandchild to the questions list
            var firstGrandchild = firstChild?.Children.FirstOrDefault();
            if (firstGrandchild != null)
            {
                questions.Add(CreateQuestion(firstGrandchild.Children.Take(4)));
            }

            return questions;
        }

        // Creates a list of CallNumberModel objects from a list of TreeNode objects
        private List<CallNumberModel> CreateQuestion(IEnumerable<TreeNode<CallNumberModel>> nodes)
        {
            return nodes.Select(node => new CallNumberModel(node.Data.CallNumber, node.Data.Description)).ToList();
        }

        // Counts the number of tabs at the beginning of a string
        private int CountTabsAtBeginning(string line)
        {
            int tabCount = 0;

            foreach (char c in line)
            {
                if (c == '\t')
                {
                    tabCount++;
                }
                else
                {
                    break;
                }
            }

            return tabCount;
        }
    }
    public static class Extensions
    {
        // Fisher-Yates Shuffle Algorithm
        // https://www.geeksforgeeks.org/shuffle-a-given-array-using-fisher-yates-shuffle-algorithm/
        // Author : GeeksforGeeks
        public static void Shuffle<T>(this IList<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
