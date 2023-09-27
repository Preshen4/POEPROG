using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NovelNestLibraryAPI.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        public int ReplaceScore { get; set; } = 0;
        public int IdAreaScore { get; set; } = 0;
        public int FindCallNumScore { get; set; } = 0;
        public int TotalScore { get; set; } = 0;


    }
}
