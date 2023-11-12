namespace NovelNestLibraryAPI.Models
{
    public class FindCallNumberTree
    {
        public FindCallNumberTreeNode Root { get; }

        public FindCallNumberTree(int rootKey, string rootDescription)
        {
            Root = new FindCallNumberTreeNode(rootKey, rootDescription);
        }
    }
}
