using Newtonsoft.Json;
using ProductCatalog.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Services
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// To connect to the Restdb.io Database
        /// Examples: https://restdb.io/docs/rest-api-code-examples#restdb
        /// Request Methods: https://restdb.io/docs/rest-api#restdb
        /// </summary>
        /// <param name="method">request method (get, post, put or delete)</param>
        /// <returns>the response</returns>
        public IRestResponse AccessDatabase(Method requestMethod)
        {
            var client = new RestClient("https://product-b1ac.restdb.io/rest/product");
            var request = new RestRequest(requestMethod);
            request.AddHeader("cache-control", "no-cache");
            // The API-Key to my database
            request.AddHeader("x-apikey", "88b8a7a5deec8f4416d6894a550baa9625658");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IEnumerable<Product> GetAll()
        {
            IRestResponse response = AccessDatabase(Method.GET);
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(response.Content.ToString());
            var productsAscending = products.OrderBy(p => p.ProductID).ToList();
            return productsAscending.ToArray();
        }

        public Product GetById(int id)
        {
            IEnumerable<Product> products = GetAll();
            return products.First(p => p.ProductID == id);
        }

        /// <summary>
        /// The same as the AccessDatabase-Method but you have to add a parameter to the request
        /// </summary>
        /// <param name="product">product to add</param>
        public void AddProduct(Product product)
        {
            var client = new RestClient("https://product-b1ac.restdb.io/rest/product");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "88b8a7a5deec8f4416d6894a550baa9625658");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"Name\":\"" + product.Name + "\",\"Description\":\"" + product.Description + "\",\"UnitPrice\":\"" + product.UnitPrice + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        /// <summary>
        /// delete a record by id, you have to get the DocumentID for doing so
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            IRestResponse response = AccessDatabase(Method.GET);
            string docid = "";

            string[] arr = response.Content.ToString().Split("}");
            foreach (var item in arr)
            {
                if (item.Contains("\"ProductID\":" + id))
                {
                    string[] splitComma = item.Split(",");
                    foreach (string str in splitComma)
                    {
                        if (str.Contains("_id"))
                        {
                            string[] idstr = str.Split(":");
                            docid = idstr[1].Trim().Replace("\"", "");
                        }
                    }
                }
            }
            // docid should now contain the DocumentID of the item to delete
            // now you can delete the record by specifying the docid in the URL
            var client = new RestClient("https://product-b1ac.restdb.io/rest/product/" + docid);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "88b8a7a5deec8f4416d6894a550baa9625658");
            request.AddHeader("content-type", "application/json");
            IRestResponse responsedelete = client.Execute(request);
        }

        /// <summary>
        /// Search a product and return a view with all products that contain the searchtext
        /// </summary>
        /// <param name="name">searched text</param>
        /// <returns></returns>
        public IEnumerable<Product> SearchProduct(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IEnumerable<Product> products = GetAll();
                var results = new List<Product>();

                foreach (var item in products)
                {
                    if (item.Name.Trim().ToLower().Contains(name.Trim().ToLower()))
                    {
                        results.Add(item);
                    }
                }
                return results.ToArray();
            }
            return null;
        }
    }
}
