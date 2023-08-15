using Microsoft.AspNetCore.Mvc;

public class OrdersController : Controller
{
    private readonly IShopifyClient _shopifyClient;
    private readonly IRewardPointsRepository _rewardPointsRepository;

    public OrdersController(IShopifyClient shopifyClient, IRewardPointsRepository rewardPointsRepository)
    {
        _shopifyClient = shopifyClient;
        _rewardPointsRepository = rewardPointsRepository;
    }

    [HttpGet("/orders")]
    public async Task<IEnumerable<Order>> GetOrders()
    {
        var orders = await _shopifyClient.GetAllOrdersAsync();
        foreach (var order in orders)
        {
            var rewardPoints = CalculateRewardPoints(order);
            _rewardPointsRepository.AddRewardPoints(order.CustomerId, rewardPoints);
        }

        return orders;
    }

    private int CalculateRewardPoints(Order order)
    {
        var totalOrderValue = order.LineItems.Sum(lineItem => lineItem.Quantity * lineItem.Price);
        var rewardPointsPerDollar = 10;
        return totalOrderValue / rewardPointsPerDollar;
    }

    [HttpGet("/reward-points/{customerId}")]
    public async Task<int> GetRewardPoints(int customerId)
    {
        var rewardPoints = await _rewardPointsRepository.GetRewardPoints(customerId);
        return rewardPoints;
    }
}