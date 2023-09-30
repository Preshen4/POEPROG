using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NovelNestLibraryAPI.Models;

namespace NovelNestLibraryAPI.Services;
public class UserService
{
    private readonly IMongoCollection<Users> _usersCollection;

    public UserService(
        IOptions<NovelNestLibraryDatabaseSettings> usersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            usersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            usersDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<Users>(
            usersDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<Users>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<Users?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Users newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, Users updatedBook) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<bool> IsUsernameOrEmailTakenAsync(string username, string email)
    {
        var user = await _usersCollection.Find(x => x.Username == username || x.Email == email).FirstOrDefaultAsync();
        return user != null;
    }

    public async Task<Users> FindUserAsync(string email, string password) =>
        await _usersCollection.Find(x => (x.Email == email) && (x.Password == password)).FirstOrDefaultAsync();

}