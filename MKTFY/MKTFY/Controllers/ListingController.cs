using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    // If the 'Route' is declared at the class level it will apply to all the methods below. In this case all api endpoints 
    // will start with '/api/listing'.
    // The ApiController annotation will make sure the api endpoint returns JSON
    [Route("api/[controller]")]
    [ApiController]
    // This is to make sure only API accesses are allowed for users that have an access token (bearer) and have a role of 'member'
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "member")]
    public class ListingController : ControllerBase  // ControllerBase is a parent class of 'Controller' with less functions.
    {
        private readonly IListingRepository _listingRepository;
        private readonly IListingService _listingService;

        // The constructor and the property (field) above are for Dependency Injection to handle the instance of this 
        // ListingController. The constructor needs an interface as a parameter in order for DI to work.
        public ListingController(IListingRepository listingRepository, IListingService listingService)
        {
            _listingRepository = listingRepository;
            _listingService = listingService;
        }

        // The post annotation requires that we define where the data is coming from in the method signature and add the 
        // appropriate object (in this case ListingAddVM) to map the JSON data coming from the front-end.
        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingAddVM data)
        {
            var entity = new Listing(data);

            var result = await _listingRepository.Create(entity);
            return Ok(new ListingVM(result));
        }

        [HttpGet]
        public async Task<ActionResult<ListingVM>> GetAll()
        {
            var results = await _listingRepository.GetAll();
            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }

        // This HTTP request will use the class defined annotation of '/api/listing' and then add the 'id' part that needs to
        // come from the client so it's '/api/listing/{id}'. The method needs to take that Id in through the [FromRoute] part.
        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> Get([FromRoute] Guid id)
        {
            var result = await _listingRepository.Get(id);
            return Ok(new ListingVM(result));
        }


        [HttpPut("edit/{id}")]
        public async Task<ActionResult<ListingVM>> Edit([FromRoute] Guid id, [FromBody] ListingEditVM listingEdit)
        {
            var updatedListing = new Listing(listingEdit);
            var result = await _listingService.EditListing(id, updatedListing);
            return Ok(new ListingVM(result));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _listingRepository.Delete(id);
            return Ok();
        }


        [HttpGet("category/{id}")]
        public async Task<ActionResult<ListingVM>> GetListingsByCategory([FromRoute] Guid id, [FromHeader] string userId, [FromQuery] string searchTerm)
        {
            var results = await _listingService.GetListingsByCategory(id, userId, searchTerm);

            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }


        [HttpGet("deals")]
        public async Task<ActionResult<ListingVM>> GetDealsWithLastThreeSearches([FromQuery] string searchTerm, [FromHeader] string userId)
        {
            var results = await _listingService.GetDealsWithLastThreeSearches(searchTerm, userId);

            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }
    }
}
