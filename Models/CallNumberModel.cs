namespace NovelNestLibraryAPI.Models
{
    public class CallNumberModel
    {
        public int CallNumber { get; set; }
        public string Description { get; set; }

        public CallNumberModel(int callnumber, string description)
        {
            CallNumber = callnumber;
            Description = description;
        }
    }
}
