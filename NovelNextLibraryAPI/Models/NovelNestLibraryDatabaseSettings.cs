namespace NovelNestLibraryAPI.Models
{
    public class NovelNestLibraryDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string LeaderBoardCollectionName { get; set; } = null!;

    }
}
