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
        public static IWebHostEnvironment _environment;

        public PlaceController(IPlaceService placeService, IWebHostEnvironment environment)
        {
            _placeService = placeService;
            _environment = environment;
        }

        public class FileUploadAPI
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public IFormFile Files { get; set; }
            public string UserId { get; set; }
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
        public async Task<bool> PutPlace(PlaceClient model/*[FromForm] FileUploadAPI objFile*/)
        {
            //string fileName = string.Concat(Path.GetFileNameWithoutExtension(objFile.Files.FileName),
            //                      DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
            //                      Path.GetExtension(objFile.Files.FileName));
            //try
            //{
            //    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
            //    {
            //        objFile.Files.CopyTo(fileStream);
            //        fileStream.Flush();
            //        string Link = "\\Upload\\" + fileName;
            //        PlaceClient model = new PlaceClient(objFile.Id, objFile.Title, objFile.Content, objFile.UserId);
            //        return _placeService.Update(model);
            //    }
            //}
            //catch
            //{
            //    System.IO.File.Delete(_environment.WebRootPath + "\\Upload\\" + fileName);
            //    return false;
            //}
            return _placeService.Update(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<bool> PostPlace(PlaceClient model/*[FromForm] FileUploadAPI objFile*/)
        {
            //string fileName = string.Concat(Path.GetFileNameWithoutExtension(objFile.Files.FileName),
            //                      DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
            //                      Path.GetExtension(objFile.Files.FileName));
            //try
            //{
            //    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
            //    {
            //        objFile.Files.CopyTo(fileStream);
            //        fileStream.Flush();
            //        string Link = "\\Upload\\" + fileName;
            //        PlaceClient model = new PlaceClient(objFile.Title, objFile.Content, objFile.UserId);
            //        return _placeService.Create(model);
            //    }
            //}
            //catch
            //{
            //    System.IO.File.Delete(_environment.WebRootPath + "\\Upload\\" + fileName);
            //    return false;
            //}
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
