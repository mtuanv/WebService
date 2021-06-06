using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Webservice.API.ClientSide.Models;
using Webservice.API.Services;

namespace Webservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlacesController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public ActionResult<List<PlaceClient>> GetAllPlace()
        {
            return _placeService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<PlaceClient> GetPlaceById(int id)
        {
            return _placeService.GetPlaceById(id);
        }

        [HttpGet("search/{key}")]
        public ActionResult<List<PlaceClient>> SearchPlace(string key)
        {
            return _placeService.Search(key);
        }

        [HttpPut]
        public ActionResult<bool> PutPlace(PlaceClient model)
        {
            return _placeService.Update(model);
        }

        [HttpPost]
        public ActionResult<PlaceClient> PostPlace(PlaceClient model)
        {
            return _placeService.Create(model);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeletePlace(int id)
        {
            return _placeService.Delete(id);
        }
    }
}
