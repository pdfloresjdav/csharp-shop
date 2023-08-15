using MongoDB.Driver;

public class RewardPointsRepository
{
    private readonly IMongoClient _mongoClient;

    public RewardPointsRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task AddRewardPoints(int customerId, int rewardPoints)
    {
        var database = _mongoClient.GetDatabase("reward_points");
        var collection = database.GetCollection<RewardPoints>("reward_points");

        var rewardPointsDocument = new RewardPoints
        {
            CustomerId = customerId,
            RewardPoints = rewardPoints
        };

        await collection.InsertOneAsync(rewardPointsDocument);
    }

    public async Task<int> GetRewardPoints(int customerId)
    {
        var database = _mongoClient.GetDatabase("reward_points");
        var collection = database.GetCollection<RewardPoints>("reward_points");

        var filter = Builders<RewardPoints>.Filter.Eq("CustomerId", customerId);
        var rewardPointsDocument = await collection.FindOneAsync(filter);

        return rewardPointsDocument?.RewardPoints ?? 0;
    }
}