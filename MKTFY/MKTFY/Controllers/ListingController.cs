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
    /// <summary>
    /// Listing Controller
    /// </summary>
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
        /// <summary>
        /// Listing Controler Costructor which takes in an IListingRepository and IListingService
        /// </summary>
        /// <param name="listingRepository"></param>
        /// <param name="listingService"></param>
        public ListingController(IListingRepository listingRepository, IListingService listingService)
        {
            _listingRepository = listingRepository;
            _listingService = listingService;
        }

        // The post annotation requires that we define where the data is coming from in the method signature and add the 
        // appropriate object (in this case ListingAddVM) to map the JSON data coming from the front-end.
        /// <summary>
        /// Create a new listing from user
        /// </summary>
        /// <param name="data">Needs a ListingAddVM</param>
        /// <returns>Returns a ListingVM with the listing that was created</returns>
        /// <response code="200">Listing that was created</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingAddVM data)
        {
            var entity = new Listing(data);

            var result = await _listingRepository.Create(entity);
            return Ok(new ListingVM(result));
        }

        /// <summary>
        /// Get all listings
        /// </summary>
        /// <returns>
        /// Array of listings, not including deleted listings
        /// </returns>
        /// <response code="200">Listing found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet]
        public async Task<ActionResult<ListingVM>> GetAll()
        {
            var results = await _listingRepository.GetAll();
            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }

        // This HTTP request will use the class defined annotation of '/api/listing' and then add the 'id' part that needs to
        // come from the client so it's '/api/listing/{id}'. The method needs to take that Id in through the [FromRoute] part.
        /// <summary>
        /// Get specific listing by listingId
        /// </summary>
        /// <param name="id">needs the listingId in the URL</param>
        /// <returns>Returns a ListingVM with the specific listing</returns>
        /// <response code="200">Listing found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> Get([FromRoute] Guid id)
        {
            var result = await _listingRepository.Get(id);
            return Ok(new ListingVM(result));
        }

        /// <summary>
        /// Edit specific listing by listingId
        /// </summary>
        /// <param name="id">needs the listingId in the URL</param>
        /// <param name="listingEdit"></param>
        /// <remarks>Needs a ListingEditVM in the body of the HTTP request with json data</remarks>
        /// <returns>Returns a ListingVM with the updated listing</returns>
        /// <response code="200">Listing found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<ListingVM>> Edit([FromRoute] Guid id, [FromBody] ListingEditVM listingEdit)
        {
            var updatedListing = new Listing(listingEdit);
            var result = await _listingService.EditListing(id, updatedListing);
            return Ok(new ListingVM(result));
        }

        /// <summary>
        /// Delete a specific listing
        /// </summary>
        /// <param name="id">needs the listingId in the URL</param>
        /// <returns>returns a 200 code if listing was deleted successfully on the server</returns>
        /// <response code="200">Listing found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _listingRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Get all Listings by CategoryId. Optional: add a search term to search for specific keywords in that category
        /// </summary>
        /// <param name="id">needs the categoryId in the URL</param>
        /// <param name="userId"></param>
        /// <param name="searchTerm"></param>
        /// <remarks>Needs the userId in the HTTP Header as well as the optional searchTerm as a Querry in the URL</remarks>
        /// <returns>returns a list of listings based on the categoryId and an optional searchTerm</returns>
        /// <response code="200">Listings found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet("category/{id}")]
        public async Task<ActionResult<ListingVM>> GetListingsByCategory([FromRoute] Guid id, [FromHeader] string userId, [FromQuery] string searchTerm)
        {
            var results = await _listingService.GetListingsByCategory(id, userId, searchTerm);

            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }

        /// <summary>
        /// Get all Listings from the past three searchTerms (can be from different categories) this user has used. Will return nothing if user hasen't used a search before
        /// </summary>       
        /// <remarks>Needs the userId in the HTTP Header as well as the optional searchTerm as a Querry in the URL</remarks>
        /// <returns>returns a list of listings based on the last three searches the user has done limited to the listings if an optional searchTerm has been provided</returns>
        /// <response code="200">Listings found</response>
        /// <response code="401">Not currently logged in</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Server failure, unknown reason</response>
        [HttpGet("deals")]
        public async Task<ActionResult<ListingVM>> GetDealsWithLastThreeSearches([FromQuery] string searchTerm, [FromHeader] string userId)
        {
            var results = await _listingService.GetDealsWithLastThreeSearches(searchTerm, userId);

            var models = results.Select(item => new ListingVM(item));
            return Ok(models);
        }
    }
}
