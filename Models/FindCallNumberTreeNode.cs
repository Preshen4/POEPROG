namespace NovelNestLibraryAPI.Models
{
    public class FindCallNumberTreeNode
    {
        public int Key { get; }
        public string Description { get; }
        public List<FindCallNumberTreeNode> Children { get; }

        public FindCallNumberTreeNode(int key, string description)
        {
            Key = key;
            Description = description;
            Children = new List<FindCallNumberTreeNode>();
        }

        public void AddChild(FindCallNumberTreeNode child)
        {
            Children.Add(child);
        }
    }
}
