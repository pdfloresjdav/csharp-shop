using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using ShopifySharp;
using Test2.Models;

namespace Test2
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Get the Giphy API key and Shopify credentials from environment variables.
            string giphyApiKey = Environment.GetEnvironmentVariable("GIPHY_API_KEY");
            string shopifyUrl = Environment.GetEnvironmentVariable("SHOPIFY_URL");
            string shopifySecretToken = Environment.GetEnvironmentVariable("SHOPIFY_SECRET_TOKEN");
            string mongoCon = Environment.GetEnvironmentVariable("MONGO_CON");
            // Configure MongoDB connection
            var mongoClient = new MongoClient(mongoCon);
            var database = mongoClient.GetDatabase("ShopifyRewardDB");

            services.AddSingleton(database);

            // Configure ShopifySharp service
            var shopifyService = new ShopifyService(shopifyUrl, shopifySecretToken);
            services.AddSingleton(shopifyService);

            services.AddControllers();
            

            // Create a new Shopify client.
            ShopifyClient shopifyClient = new ShopifyClient(shopifyUrl, shopifySecretToken);

            // Query the Giphy API Trending endpoint.
            var trendingGifs = shopifyClient.Giphy.Trending(giphyApiKey);

            // Add each gif to Shopify as a product.
            foreach (var gif in trendingGifs)
            {
                // Create a new product.
                Product product = new Product();
                product.Title = gif.Title;
                product.Description = gif.Description;
                product.Vendor = "Acme Inc.";
                product.ProductType = "Digital Product";

                // Add a default variant with a random price.
                Variant variant = new Variant();
                variant.Price = Random.Range(1, 100);
                product.Variants.Add(variant);

                // Add another variant with a different height and width.
                variant = new Variant();
                variant.Height = 200;
                variant.Width = 200;
                product.Variants.Add(variant);

                // Add the product to Shopify.
                shopifyClient.Products.Create(product);

                // Add the gif still image as a product image.
                shopifyClient.ProductImages.Create(product.Id, gif.Images.Original.Url);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
