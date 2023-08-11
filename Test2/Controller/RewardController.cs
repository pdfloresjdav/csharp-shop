using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopifySharp;

namespace Test2.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RewardController : ControllerBase
  {
    private readonly IMongoCollection<CustomerReward> _rewardCollection;
    private readonly ShopifyService _shopifyService;

    public RewardController(IMongoDatabase database, ShopifyService shopifyService)
    {
      _rewardCollection = database.GetCollection<CustomerReward>("rewards");
      _shopifyService = shopifyService;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetRewardBalance(int customerId)
    {
      try
      {
        var customerReward = await _rewardCollection.Find(r => r.CustomerId == customerId).FirstOrDefaultAsync();
        if (customerReward == null)
        {
          return NotFound("Customer reward not found.");
        }

        return Ok(customerReward.RewardPoints);
      }
      catch (Exception ex)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
      }
    }
  }

  public class CustomerReward
  {
    public int CustomerId { get; set; }
    public int RewardPoints { get; set; }
  }

}
