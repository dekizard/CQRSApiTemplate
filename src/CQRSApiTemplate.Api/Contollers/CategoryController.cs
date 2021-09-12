using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Aliasnet.ResponseModel;
using CQRSApiTemplate.Application.Categories.Commands.CreateCategory;
using CQRSApiTemplate.Application.Categories.Commands.DeleteCategory;
using CQRSApiTemplate.Application.Categories.Commands.UpdateCategory;
using CQRSApiTemplate.Application.Categories.Queries.GetCategoryWithProducts;

namespace CQRSApiTemplate.Api.Contollers
{
    public class CategoryController: BaseApiController
    {
        /// <summary>
        ///  Get category with products
        /// </summary>
        /// <param name="command">Category Id</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetCategoryWithProductsQuery command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        ///  Create category
        /// </summary>
        /// <param name="command">Create category</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        ///  Update category
        /// </summary>
        /// <param name="command">Update category</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        ///  Delete category
        /// </summary>
        /// <param name="command">Delete category</param>
        /// <returns>Result</returns>
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
