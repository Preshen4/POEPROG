using NovelNestLibraryAPI.Models;

namespace NovelNestLibraryAPI.Tree
{
    // Create a Tree Node class
    // https://codereview.stackexchange.com/questions/163957/building-a-tree-from-a-flat-listnodes
    // Author: SNag

    // Generic types
    // https://www.tutorialsteacher.com/csharp/csharp-generics#:~:text=In%20C%23%2C%20generic%20means%20not,without%20the%20specific%20data%20type
    // Author : Tutorialsteacher
    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; }

        public Tree(T rootData)
        {
            Root = new TreeNode<T>(rootData);
        }
    }
}
