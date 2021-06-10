using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webservice.API.ClientSide.Models;
using Webservice.API.DataAccess.Models;

namespace Webservice.API.Services
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
        private readonly WebserviceContext _context;
        public static IWebHostEnvironment _environment;
        public PlaceService(WebserviceContext context, IWebHostEnvironment environment)
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
                    Name = model.Name,
                    Description = model.Description,
                    Link = model.Link,
                    AccountId = model.AccountId,
                    CreatedBy = model.AccountId,
                    CreatedDate = DateTime.UtcNow,
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
            place = _context.Place.Where(x => x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            File.Delete(_environment.WebRootPath + place.Link);
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                place.DeletedDate = DateTime.UtcNow;
                _context.SaveChanges();
                return true;
            }
        }

        public List<PlaceClient> GetAll()
        {
            var ListPlace = _context.Place.Where(x => !x.DeletedDate.HasValue).ToList();
            var result = ListPlace.Select(x => new PlaceClient
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                AccountId = x.AccountId,
                Link = x.Link,
            }).ToList();
            return result;
        }

        public PlaceClient GetPlaceById(int Id)
        {
            Place place = null;
            PlaceClient model = null;
            place = _context.Place.Where(x => x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                model.Id = place.Id;
                model.Name = place.Name;
                model.Description = place.Description;
                model.AccountId = place.AccountId;
                model.Link = place.Link;
            }
            return model;
        }

        public List<PlaceClient> Search(string search_Key)
        {
            var ListPlace = _context.Place.Join(_context.Account, p => p.AccountId, a => a.Id, (p, a) => new { Id = p.Id, Name = p.Name, Description = p.Description, Link = p.Link, AccountId = p.AccountId, DeletedDate = p.DeletedDate, UserName = a.UserName })
                                  .Where(x => x.Name.Contains(search_Key) || x.Description.Contains(search_Key) || x.UserName.Contains(search_Key) && !x.DeletedDate.HasValue)
                                  .ToList();
            if (ListPlace.Count() == 0)
                throw new Exception("There is no result");
            else
            {
                var result = ListPlace.Select(x => new PlaceClient
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    AccountId = x.AccountId,
                    Link = x.Link,
                }).ToList();
                return result;
            }

        }

        public bool Update(PlaceClient model)
        {
            Place place = null;
            place = _context.Place.Where(x => x.Id == model.Id && !x.DeletedDate.HasValue).FirstOrDefault();
            File.Delete(_environment.WebRootPath + place.Link);
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                place.Name = model.Name;
                place.Description = model.Description;
                place.AccountId = model.AccountId;
                place.Link = model.Link;
                place.ModifiedDate = DateTime.UtcNow;
                place.ModifiedBy = model.AccountId;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
