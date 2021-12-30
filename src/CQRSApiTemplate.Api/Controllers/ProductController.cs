using CQRSApiTemplate.Application.Features.Products.Commands.CreateProduct;
using CQRSApiTemplate.Application.Features.Products.Commands.DeleteProduct;
using CQRSApiTemplate.Application.Features.Products.Commands.UpdateProduct;
using CQRSApiTemplate.Application.Features.Products.Queries.GetProductsByCatetoryIdQuery;
using Microsoft.AspNetCore.Mvc;
using ResultModel;
using System.Net;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Api.Controllers
{
    public class ProductController : BaseApiController
    {
        /// <summary>
        ///  Get products by category Id
        /// </summary>
        /// <param name="command">Category Id</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetProductsByCatetoryIdQuery command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        ///  Create product
        /// </summary>
        /// <param name="command">Create product</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        ///  Update category
        /// </summary>
        /// <param name="command">Update product</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        ///  Delete category
        /// </summary>
        /// <param name="command">Delete product</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
