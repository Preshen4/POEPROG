using Microsoft.Build.Framework;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Tree;
using System;
using System.Collections.Generic;

namespace NovelNestLibraryAPI.Services
{
    public class FindCallNumberService
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "Data.txt");
        private Tree<CallNumberModel> tree = new Tree<CallNumberModel>(new CallNumberModel(9999, "root"));
        TreeNode<CallNumberModel> currentParent;
        public Tree<CallNumberModel> CreateTree()
        {
            string[] lines = File.ReadAllLines(_path);
            currentParent = tree.Root;

            foreach (string line in lines)
            {
                int tabs = CountTabsAtBeginning(line);
                string[] parts = line.Split('|').Select(p => p.Trim()).ToArray();
                CallNumberModel callNumber = new CallNumberModel(int.Parse(parts[0]), parts[1]);
                TreeNode<CallNumberModel> parentNode = null;

                switch (tabs)
                {
                    case 0:
                        parentNode = tree.Root;
                        break;
                    case 1:
                        parentNode = currentParent.Children.LastOrDefault();
                        break;
                    case 2:
                        parentNode = currentParent.Children.LastOrDefault()?.Children.LastOrDefault();
                        break;
                }

                parentNode?.AddChild(new TreeNode<CallNumberModel>(callNumber));
            }

            return tree;
        }
        public List<List<CallNumberModel>> CreateQuiz()
        {
            CreateTree();

            var allChildren = tree.Root.Children.ToList();
            allChildren.Shuffle(new Random());

            var questions = new List<List<CallNumberModel>>();

            // Create a question for selectedChildren
            questions.Add(CreateQuestion(allChildren.Take(4)));

            // Create a question for firstChildChildren
            var firstChild = allChildren.FirstOrDefault();
            if (firstChild != null)
            {
                questions.Add(CreateQuestion(firstChild.Children.Take(4)));
            }

            // Create a question for firstGrandchildChildren
            var firstGrandchild = firstChild?.Children.FirstOrDefault();
            if (firstGrandchild != null)
            {
                questions.Add(CreateQuestion(firstGrandchild.Children.Take(4)));
            }

            return questions;
        }

        private List<CallNumberModel> CreateQuestion(IEnumerable<TreeNode<CallNumberModel>> nodes)
        {
            return nodes.Select(node => new CallNumberModel(node.Data.CallNumber, node.Data.Description)).ToList();
        }

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
