using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static IWebHostEnvironment _environment;

        public PlaceController(IPlaceService placeService, IWebHostEnvironment environment)
        {
            _placeService = placeService;
            _environment = environment;
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
        public async Task<bool> PutPlace(PlaceClient model)
        {
            string contentRootPath = _environment.ContentRootPath;
            string fileName = string.Format("{0:YYYY-mm-dd hhmmss}", DateTime.UtcNow) + ".jpg";
            var uploads = Path.Combine(contentRootPath, "Upload");
            string filePath = Path.Combine(uploads, fileName);
            bool writeImage = _placeService.ConvertBase64ToImage(model.FileBase64, filePath);
            model.Link = fileName;
            return _placeService.Update(model);
        }

        [HttpPost]
        public async Task<bool> PostPlace(PlaceClient model)
        {
            string contentRootPath = _environment.ContentRootPath;
            string fileName = string.Format("{0:YYYY-mm-dd hhmmss}", DateTime.UtcNow) + ".jpg";
            var uploads = Path.Combine(contentRootPath, "Upload");
            string filePath = Path.Combine(uploads, fileName);
            bool writeImage = _placeService.ConvertBase64ToImage(model.FileBase64, filePath);
            model.Link = fileName;
            return _placeService.Create(model);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeletePlace(int id)
        {
            return _placeService.Delete(id);
        }
    }
}
