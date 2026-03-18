using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoService
{
    private readonly IMongoCollection<User> _users;

    public MongoService(IConfiguration config)
    {
        var client = new MongoClient(
            config["MongoDbSettings:ConnectionString"]
        );

        var database = client.GetDatabase(
            config["MongoDbSettings:DatabaseName"]
        );

        _users = database.GetCollection<User>("Users");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }
}

public class User
{
    [BsonId] // Marks this as primary key
    [BsonRepresentation(BsonType.ObjectId)] // Converts ObjectId → string
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
}