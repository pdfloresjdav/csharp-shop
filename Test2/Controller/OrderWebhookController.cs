using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderWebhookController : ControllerBase
    {
        private readonly IMongoCollection<CustomerReward> _rewardCollection;

        public OrderWebhookController(IMongoDatabase database)
        {
            _rewardCollection = database.GetCollection<CustomerReward>("rewards");
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveOrderWebhook([FromBody] OrderWebhookData data)
        {
            try
            {
                // Calculate reward points based on order value (you need to define the conversion rate)
                int rewardPoints = CalculateRewardPoints(data.OrderTotal);

                var customerReward = await _rewardCollection.FindOneAndUpdateAsync(
                    Builders<CustomerReward>.Filter.Eq(r => r.CustomerId, data.CustomerId),
                    Builders<CustomerReward>.Update.Inc(r => r.RewardPoints, rewardPoints),
                    new FindOneAndUpdateOptions<CustomerReward, CustomerReward>
                    {
                        IsUpsert = true,
                        ReturnDocument = ReturnDocument.After
                    });

                return Ok(customerReward.RewardPoints);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private int CalculateRewardPoints(decimal orderTotal)
        {
            // Calculate reward points based on your conversion rate logic
            return (int)Math.Floor(orderTotal * 0.1m); // For example, 10% of order total
        }
    }

    public class OrderWebhookData
    {
        public int CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
