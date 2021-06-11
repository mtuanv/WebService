using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
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
        bool Create(PlaceClient model);
        bool Update(PlaceClient model);
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
        public bool Create(PlaceClient model)
        {
            try
            {
                var place = new Place
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = model.UserId,
                    //CreatedBy = model.AccountId,
                    //CreatedDate = DateTime.UtcNow,
                };
                _context.Place.Add(place);
                _context.SaveChanges();
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
            //File.Delete(_environment.WebRootPath + place.Link);
            if (place == null)
                throw new Exception("Place not found");
            else
            {
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

        public bool Update(PlaceClient model)
        {
            Place place = null;
            place = _context.Place.Where(x => x.Id == model.Id).FirstOrDefault();
            //File.Delete(_environment.WebRootPath + place.Link);
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                place.Title = model.Title;
                place.Content = model.Content;
                place.UserId = model.UserId;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
