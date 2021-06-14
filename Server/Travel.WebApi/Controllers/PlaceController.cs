using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.WebApi.ClientSide.Models;
using Travel.WebApi.Services;

namespace Travel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public ActionResult<List<PlaceClient>> GetAllPlace([FromQuery] string searchText)
        {
            return _placeService.GetAll(searchText);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PlaceClient> GetPlaceById(int id)
        {
            return _placeService.GetPlaceById(id);
        }

        [HttpPut]
        public async Task<bool> PutPlace([FromForm] PlaceClient model)
        {
            return _placeService.Update(model);
        }

        [HttpPost]
        public async Task<bool> PostPlace([FromForm] PlaceClient model)
        {
            return _placeService.Create(model);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeletePlace(int id)
        {
            return _placeService.Delete(id);
        }
    }
}
