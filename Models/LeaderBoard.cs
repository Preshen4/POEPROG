using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovelNestLibraryAPI.Models
{
    public class LeaderBoard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}
