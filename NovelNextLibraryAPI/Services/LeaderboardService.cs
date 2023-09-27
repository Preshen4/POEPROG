using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NovelNestLibraryAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace NovelNestLibraryAPI.Services
{
    public class LeaderboardService
    {
        private readonly IMongoCollection<LeaderBoard> _leaderboardCollection;

        public LeaderboardService(IOptions<NovelNestLibraryDatabaseSettings> usersDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                usersDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                usersDatabaseSettings.Value.DatabaseName);

            _leaderboardCollection = mongoDatabase.GetCollection<LeaderBoard>(
                usersDatabaseSettings.Value.LeaderBoardCollectionName);
        }

        public async Task<List<LeaderBoard>> GetAllLeaderboardEntriesAsync()
        {
            return await _leaderboardCollection.Find(_ => true)
                .SortBy(entry => entry.Score)
                .ToListAsync();
        }

        public async Task AddLeaderboardEntryAsync(string userName, int score)
        {
            var existingEntry = await GetLeaderboardEntryAsync(userName);

            if (existingEntry == null)
            {
                var entry = new LeaderBoard
                {
                    UserName = userName,
                    Score = score
                };

                await _leaderboardCollection.InsertOneAsync(entry);
            }
            else
            {

                await UpdateLeaderboardEntryAsync(userName, score);
            }
        }

        public async Task UpdateLeaderboardEntryAsync(string userName, int newScore)
        {
            var filter = Builders<LeaderBoard>.Filter.Eq(entry => entry.UserName, userName);
            var update = Builders<LeaderBoard>.Update.Set(entry => entry.Score, newScore);

            await _leaderboardCollection.UpdateOneAsync(filter, update);
        }
        public async Task<LeaderBoard> GetLeaderboardEntryAsync(string userName)
        {
            return await _leaderboardCollection.Find(entry => entry.UserName == userName).FirstOrDefaultAsync();
        }
    }
}
