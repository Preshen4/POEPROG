using NovelNestLibraryAPI.Models;

namespace NovelNestLibraryAPI.Tree
{
    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; }

        public Tree(T rootData)
        {
            Root = new TreeNode<T>(rootData);
        }
    }
}
