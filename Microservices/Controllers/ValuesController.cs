﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microservices.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;

        public ValuesController(IProductRepository iProductRepository)
        {

            _iProductRepository = iProductRepository;
        }

        //List<string> list = new List<string>();
        // GET api/values
        [HttpGet]
        public Task<string> GetAll() => GetAllProducts();

        private async Task<string> GetAllProducts(){

            var products = await _iProductRepository.GetAll();
            return JsonConvert.SerializeObject(products);
        }



        // GET api/values/5
        [HttpGet("{name}")]
        public Task<string> GetByName(string name) => GetProductsByName(name);


        private async Task<string> GetProductsByName(string name)
        {

            var products = await _iProductRepository.GetByName(name);
            return JsonConvert.SerializeObject(products);
        }



        //// GET api/values/5
        //[HttpGet("{id}")]
        //public Task<string> Get(string id) => GetProductsById(id);
       

        //private async Task<string> GetProductsById(string id)
        //{

        //    var products = await _iProductRepository.Get(id);
        //    return JsonConvert.SerializeObject(products);
        //}

        // POST api/values
        [HttpPost]
        public async Task<string> Post([FromBody]Dictionary<string,Product> products)
        {
            foreach(var product in products.Values)
            {
                product.Created = Helper.Helper.Created(DateTime.Now);
                await _iProductRepository.Add(product);
            }

            return "GOT IT";
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody]Product product)
        {
            if (string.IsNullOrEmpty(id)) return "invalid id!!";

            return await _iProductRepository.Update(id, product);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return "invalid id!!";

            await _iProductRepository.Remove(id);
            return string.Empty;
        }
    }
}
