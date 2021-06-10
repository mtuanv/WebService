using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Webservice.API.ClientSide.Models;
using Webservice.API.Services;

namespace Webservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        public static IWebHostEnvironment _environment;

        public PlacesController(IPlaceService placeService, IWebHostEnvironment environment)
        {
            _placeService = placeService;
            _environment = environment;
        }

        public class FileUploadAPI
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public IFormFile Files { get; set; }
            public int AccountId { get; set; }
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
        public async Task<bool> PutPlace([FromForm] FileUploadAPI objFile)
        {
            string fileName = string.Concat(Path.GetFileNameWithoutExtension(objFile.Files.FileName),
                                  DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                                  Path.GetExtension(objFile.Files.FileName));
            try
            {
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
                {
                    objFile.Files.CopyTo(fileStream);
                    fileStream.Flush();
                    string Link = "\\Upload\\" + fileName;
                    PlaceClient model = new PlaceClient(objFile.Id, objFile.Name, objFile.Description, Link, objFile.AccountId);
                    return _placeService.Update(model);
                }
            }
            catch
            {
                System.IO.File.Delete(_environment.WebRootPath + "\\Upload\\" + fileName);
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> PostPlace([FromForm] FileUploadAPI objFile)
        {
            string fileName = string.Concat(Path.GetFileNameWithoutExtension(objFile.Files.FileName),
                                  DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                                  Path.GetExtension(objFile.Files.FileName));
            try
            {
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
                {
                    objFile.Files.CopyTo(fileStream);
                    fileStream.Flush();
                    string Link = "\\Upload\\" + fileName;
                    PlaceClient model = new PlaceClient(objFile.Name, objFile.Description, Link, objFile.AccountId);
                    return _placeService.Create(model);
                }
            }
            catch
            {
                System.IO.File.Delete(_environment.WebRootPath + "\\Upload\\" + fileName);
                return false;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeletePlace(int id)
        {
            return _placeService.Delete(id);
        }
    }
}
