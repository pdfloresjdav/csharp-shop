using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ShopifySharp;

namespace Test2.Services
{
    public class GiphyService
    {
        public async Task<List<Product>> GetTrendingGifs(string giphyApiKey)
        {
            // Create a new Giphy client.
            ShopifyClient giphyClient = new ShopifyClient("https://api.giphy.com/v1/gifs/trending", "API_KEY");

            // Query the Giphy API Trending endpoint.
            var trendingGifs = await giphyClient.Giphy.Trending(giphyApiKey);

            // Return the list of trending gifs.
            return trendingGifs;
        }
    }
}
