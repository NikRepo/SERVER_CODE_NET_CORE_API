using CommonServiceHelper.Service_Model;
using Microsoft.AspNetCore.Mvc;
using ProductService.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        private readonly IProductRespository _productRespository;

        public Products()
        {
            _productRespository = new ProductBuilder();
        }
        // GET: api/<Product>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok( _productRespository.GetAllProducts());//new string[] { "value1", "value2" };
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_productRespository.GetProduct(id));
        }

        // POST api/<Product>
        [HttpPost]
        public  IActionResult Post([FromBody] ProductModel product)
        {
            var entity =  _productRespository.AddProduct(product);
            return  new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
        }

        //// PUT api/<Product>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Product>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
