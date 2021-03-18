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
    [Route("api/[controller]")]
    [ApiController]    
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;
        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
                
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<FaqVM>> GetFaq([FromRoute] Guid id)
        {
            var result = await _faqService.GetFaq(id);
            return Ok(new FaqVM(result));
        }

        [HttpGet("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
        public async Task<ActionResult<FaqVM>> GetAllFaq()
        {
            var results = await _faqService.GetAllFaq();
            var models = results.Select(item => new FaqVM(item));
            
            return Ok(models);
        }

        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        public async Task<ActionResult<FaqVM>> AddFaq([FromBody] FaqAddVM data)
        {
            var result = await _faqService.AddFaq(new Faq(data));
            return Ok(new FaqVM(result));
        }

        [HttpPut("edit/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        public async Task<ActionResult<FaqVM>> EditFaq([FromRoute] Guid id, [FromBody] FaqAddVM data)
        {
            var result = await _faqService.EditFaq(id, new Faq(data));
            return Ok(new FaqVM(result));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        public async Task<ActionResult> DeleteFaq([FromRoute] Guid id)
        {
            await _faqService.DeleteFaq(id);
            return Ok();
        }
    }
}
