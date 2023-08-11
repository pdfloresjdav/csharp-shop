using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ShopifySharp;

namespace Test2.Services
{
    public class ShopifyService
    {
        public async Task<Product> CreateProduct(Product product, string shopifyUrl, string shopifySecretToken)
        {
            // Create a new Shopify client.
            ShopifyClient shopifyClient = new ShopifyClient(shopifyUrl, shopifySecretToken);

            // Create the product in Shopify.
            var createdProduct = await shopifyClient.Products.Create(product);

            // Return the created product.
            return createdProduct;
        }

        public async Task AddProductImage(int productId, string imageUrl, string shopifyUrl, string shopifySecretToken)
        {
            // Create a new Shopify client.
            ShopifyClient shopifyClient = new ShopifyClient(shopifyUrl, shopifySecretToken);

            // Add the product image to Shopify.
            await shopifyClient.ProductImages.Create(productId, imageUrl);
        }
    }
}
