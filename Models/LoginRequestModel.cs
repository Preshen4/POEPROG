﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NovelNestLibraryAPI.Models
{
    public class LoginRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
    }
}