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

        // Optional: You can add methods for tree traversal (e.g., depth-first or breadth-first)
        public void TraverseDepthFirst(Action<T> action)
        {
            TraverseDepthFirst(Root, action);
        }

        private void TraverseDepthFirst(TreeNode<T> node, Action<T> action)
        {
            if (node == null)
                return;

            action(node.Data);

            foreach (var child in node.Children)
            {
                TraverseDepthFirst(child, action);
            }
        }

        public List<TreeNode<CallNumberModel>> FindPath(Tree<CallNumberModel> tree, int targetCallNumber)
        {
            List<TreeNode<CallNumberModel>> path = new List<TreeNode<CallNumberModel>>();
            FindPath(tree.Root, targetCallNumber, path);
            return path;
        }

        private bool FindPath(TreeNode<CallNumberModel> currentNode, int targetCallNumber, List<TreeNode<CallNumberModel>> path)
        {
            if (currentNode == null)
            {
                return false;
            }

            // Check if the current node's call number matches the target
            if (currentNode.Data.CallNumber == targetCallNumber)
            {
                path.Add(currentNode);
                return true;
            }

            // Try to find the target in the children
            foreach (var child in currentNode.Children)
            {
                if (FindPath(child, targetCallNumber, path))
                {
                    // If the target is found in the child's subtree, add the current node to the path
                    path.Insert(0, currentNode);
                    return true;
                }
            }

            return false;
        }

    }
}
