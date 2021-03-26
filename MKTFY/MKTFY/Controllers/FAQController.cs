using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    /// <summary>
    /// FAQ Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;
        /// <summary>
        /// FAQ Controller Constructor which takes in a IFaqService for DI
        /// </summary>
        /// <param name="faqService"></param>
        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        /// <summary>
        /// Get all FAQs
        /// </summary>  
        /// <param name="id">Requires the FAQ Id in URL</param>
        /// <returns>Returns a FaqVM as JSON data with a FaqId, title and a description</returns>
        /// <response code="200">Specific FaqVM is returned</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<FaqVM>> GetFaq([FromRoute] Guid id)
        {
            var result = await _faqService.GetFaq(id);
            return Ok(new FaqVM(result));
        }

        /// <summary>
        /// Get all FAQs
        /// </summary>         
        /// <returns>Returns aall FaqVMs as JSON data with a FaqId, title and a description</returns>
        /// <response code="200">All FaqVMs are returned</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<FaqVM>> GetAllFaq()
        {
            var results = await _faqService.GetAllFaq();
            var models = results.Select(item => new FaqVM(item));
            
            return Ok(models);
        }

        /// <summary>
        /// Get all FAQs
        /// </summary>  
        /// <param name="data">Requires a FaqAddVM as JSON data with a title and description </param>
        /// <returns>Returns a FaqVM as JSON data with a FaqId, title and a description</returns>
        /// <response code="200">Added FaqVM is returned</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        public async Task<ActionResult<FaqVM>> AddFaq([FromBody] FaqAddVM data)
        {
            var result = await _faqService.AddFaq(new Faq(data));
            return Ok(new FaqVM(result));
        }

        /// <summary>
        /// Edit FAQ
        /// </summary>
        /// <param name="id">Requires FaqId in URL</param>
        /// <param name="data">Requires FaqAddVM as JSON data with updated title and description</param>        
        /// <returns>Returns a FaqVM as JSON data with a FaqId, title and a description</returns>
        /// <response code="200">Specific FaqVM is returned</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpPut("edit/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        public async Task<ActionResult<FaqVM>> EditFaq([FromRoute] Guid id, [FromBody] FaqAddVM data)
        {
            var result = await _faqService.EditFaq(id, new Faq(data));
            return Ok(new FaqVM(result));
        }

        /// <summary>
        /// Delete a specific FAQ
        /// </summary>  
        /// <param name="id">Requires the FAQ Id in URL</param>
        /// <returns>Returns an ActionResult</returns>
        /// <response code="200">FAQ has been successfully deleted</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        public async Task<ActionResult> DeleteFaq([FromRoute] Guid id)
        {
            await _faqService.DeleteFaq(id);
            return Ok();
        }
    }
}
