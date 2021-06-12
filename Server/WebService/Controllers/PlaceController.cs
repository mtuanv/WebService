using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebService.ClientSide.Models;
using WebService.Services;

namespace WebService.Controllers
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
        public ActionResult<List<PlaceClient>> GetAllPlace()
        {
            return _placeService.GetAll();
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<PlaceClient> GetPlaceById(int id)
        {
            return _placeService.GetPlaceById(id);
        }

        [Authorize]
        [HttpGet("search/{key}")]
        public ActionResult<List<PlaceClient>> SearchPlace(string key)
        {
            return _placeService.Search(key);
        }

        [Authorize]
        [HttpPut]
        public async Task<bool> PutPlace([FromForm] PlaceClient model)
        {
            return _placeService.Update(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> PostPlace([FromForm] PlaceClient model)
        {
            return _placeService.Create(model);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<bool> DeletePlace(int id)
        {
            return _placeService.Delete(id);
        }
    }
}
