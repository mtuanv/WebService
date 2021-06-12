using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebService.ClientSide.Models;
using WebService.DataAccess.Models;

namespace WebService.Services
{
    public interface IPlaceService
    {
        List<PlaceClient> GetAll();
        List<PlaceClient> Search(string search_Key);
        PlaceClient GetPlaceById(int place_Id);
        bool Create([FromForm] PlaceClient model);
        bool Update([FromForm] PlaceClient model);
        bool Delete(int place_Id);
    }
    public class PlaceService : IPlaceService
    {
        private readonly WsContext _context;
        public static IWebHostEnvironment _environment;
        public PlaceService(WsContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public bool Create([FromForm] PlaceClient model)
        {
            try
            {
                var place = new Place
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = model.UserId,
                };
                _context.Place.Add(place);
                _context.SaveChanges();
                foreach (var img in model.Files)
                {
                    string fileName = string.Concat(Path.GetFileNameWithoutExtension(img.FileName),
                                      DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                                      Path.GetExtension(img.FileName));
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
                    {
                        img.CopyTo(fileStream);
                        fileStream.Flush();
                        string path = "\\Upload\\" + fileName;
                        int placeid = _context.Place.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                        var image = new Image
                        {
                            Path = path,
                            PlaceId = placeid
                        };
                        _context.Image.Add(image);
                        _context.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            Place place = null;
            place = _context.Place.Where(x => x.Id == Id).FirstOrDefault();
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                List<Feedback> lstFeedback = _context.Feedback.Where(x => x.PlaceId == Id).Select(x => x).ToList();
                List<Image> lstImage = _context.Image.Where(x => x.PlaceId == Id).Select(x => x).ToList();
                foreach (var fb in lstFeedback)
                {
                    _context.Remove(fb);
                    _context.SaveChanges();
                }
                foreach (var img in lstImage)
                {
                    File.Delete(_environment.WebRootPath + img.Path);
                    _context.Remove(img);
                }
                _context.Remove(place);
                _context.SaveChanges();
                return true;
            }
        }

        public List<PlaceClient> GetAll()
        {
            var ListPlace = _context.Place.ToList();
            var result = ListPlace.Select(x => new PlaceClient
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                UserId = x.UserId,
            }).ToList();
            return result;
        }

        public PlaceClient GetPlaceById(int Id)
        {
            Place place = null;
            PlaceClient model = new PlaceClient();
            place = _context.Place.Where(x => x.Id == Id).FirstOrDefault();
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                model.Id = place.Id;
                model.Title = place.Title;
                model.Content = place.Content;
                model.UserId = place.UserId;
            }
            return model;
        }

        public List<PlaceClient> Search(string search_Key)
        {
            var ListPlace = _context.Place.Join(_context.AspNetUsers, p => p.UserId, a => a.Id, (p, a) => new { p.Id, p.Title, p.Content, p.UserId, a.UserName })
                                  .Where(x => x.Title.Contains(search_Key) || x.Content.Contains(search_Key) || x.UserName.Contains(search_Key))
                                  .ToList();
            if (ListPlace.Count() == 0)
                throw new Exception("There is no result");
            else
            {
                var result = ListPlace.Select(x => new PlaceClient
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    UserId = x.UserId,
                }).ToList();
                return result;
            }
        }

        public bool Update([FromForm] PlaceClient model)
        {
            try
            {
                Place place = new Place();
                place = _context.Place.Where(x => x.Id == model.Id).FirstOrDefault();
                if (place == null)
                    throw new Exception("Place not found");
                else
                {
                    place.Title = model.Title;
                    place.Content = model.Content;
                    place.UserId = model.UserId;
                    _context.SaveChanges();
                    List<Image> lstImage = _context.Image.Where(x => x.PlaceId == model.Id).Select(x=>x).ToList();
                    foreach(var img in lstImage)
                    {
                        File.Delete(_environment.WebRootPath + img.Path);
                        _context.Remove(img);
                        _context.SaveChanges();
                    }
                    foreach (var img in model.Files)
                    {
                        string fileName = string.Concat(Path.GetFileNameWithoutExtension(img.FileName),
                                          DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                                          Path.GetExtension(img.FileName));
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
                        {
                            img.CopyTo(fileStream);
                            fileStream.Flush();
                            string path = "\\Upload\\" + fileName;
                            int placeid = _context.Place.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                            var image = new Image
                            {
                                Path = path,
                                PlaceId = placeid
                            };
                            _context.Image.Add(image);
                            _context.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
