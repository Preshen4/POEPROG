namespace NovelNestLibraryAPI.Tree
{
    // Create a Tree Node class
    // https://codereview.stackexchange.com/questions/163957/building-a-tree-from-a-flat-listnodes
    // Author: Heslacher
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }
    }
}
