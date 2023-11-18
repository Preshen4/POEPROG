using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Tree;

namespace NovelNestLibraryAPI.Services
{
    public class FindCallNumberService
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "Data.txt");
        string path = "C://Repos//prog7312-part-2-Preshen4//Data.txt";
        private Tree<CallNumberModel> tree = new Tree<CallNumberModel>(new CallNumberModel(9999, "root"));
        public Tree<CallNumberModel> CreateTree()
        {
            TreeNode<CallNumberModel> currentParent = tree.Root;
            using (StreamReader sr = new StreamReader(_path))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    int tabs = CountTabsAtBeginning(line);
                    line = line.Replace("\t", "");
                    string[] parts = line.Split("|");
                    CallNumberModel callNumber = new CallNumberModel(int.Parse(parts[0]), parts[1]);
                    switch (tabs)
                    {
                        case 0:
                            currentParent = tree.Root;
                            currentParent.AddChild(new TreeNode<CallNumberModel>(callNumber));
                            break;

                        case 1:
                            if (currentParent.Children.Count > 0)
                            {
                                // Add a node to the last node added at the root
                                currentParent.Children[currentParent.Children.Count - 1].AddChild(new TreeNode<CallNumberModel>(callNumber));
                            }
                            break;

                        case 2:
                            if (currentParent.Children.Count > 0 &&
                                currentParent.Children[currentParent.Children.Count - 1].Children.Count > 0)
                            {
                                // Add a node to the last node added in the second level
                                currentParent.Children[currentParent.Children.Count - 1]
                                    .Children[currentParent.Children[currentParent.Children.Count - 1].Children.Count - 1]
                                    .AddChild(new TreeNode<CallNumberModel>(callNumber));
                            }
                            break;
                    }
                }
            }

            return tree;

        }

        public List<int> FindCallNumber(int callNumber)
        {
            List<TreeNode<CallNumberModel>> pathToTarget = tree.FindPath(tree, callNumber);
            List<int> path = new List<int>();
            foreach (var node in pathToTarget)
            {
                path.Add(node.Data.CallNumber);
            }
            path.Remove(0);
            return path;
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

}
