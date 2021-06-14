using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Travel.WebApi.ClientSide.Models;
using Travel.WebApi.DataAccess.Extensions;
using Travel.WebApi.DataAccess.Models;

namespace Travel.WebApi.Services
{
    public interface IPlaceService
    {
        List<PlaceClient> GetAll(string searchText);
        PlaceClient GetPlaceById(int place_Id);
        bool Create(PlaceClient model);
        bool Update(PlaceClient model);
        bool Delete(int place_Id);
        bool ConvertBase64ToImage(string fileBase64, string filePath);
    }
    public class PlaceService : IPlaceService
    {
        private readonly IRepository<Places> _placeRepository;
        private readonly IRepository<Feedbacks> _feedbackRepository;

        public static IWebHostEnvironment _environment;
        public PlaceService(IRepository<Places> placeRepository, IRepository<Feedbacks> feedbackRepository, IWebHostEnvironment environment)
        {
            _placeRepository = placeRepository;
            _feedbackRepository = feedbackRepository;
            _environment = environment;
        }
        public List<PlaceClient> GetAll(string searchText)
        {
            var ListPlace = _placeRepository.GetAll().ToList();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                ListPlace = ListPlace.Where(t => t.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                                        || t.Content.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (ListPlace.Count == 0)
                throw new Exception("There is no result");
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
            PlaceClient model = new PlaceClient();
            Places place = _placeRepository.Get(Id);
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                model.Id = place.Id;
                model.Title = place.Title;
                model.Content = place.Content;
                model.UserId = place.UserId;
                model.Link = place.Link;
            }
            return model;
        }
        public bool Create(PlaceClient model)
        {
            try
            {
                var place = new Places
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = model.UserId,
                    Link = model.Link,
                };
                _placeRepository.Insert(place);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(PlaceClient model)
        {
            try
            {
                Places place = _placeRepository.Get(model.Id);//_context.Place.Where(x => x.Id == model.Id).FirstOrDefault();
                if (place == null)
                    throw new Exception("Place not found");
                else
                {
                    place.Title = model.Title;
                    place.Content = model.Content;
                    place.UserId = model.UserId;
                    place.Link = model.Link;
                    _placeRepository.Update(place);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int Id)
        {
            Places place = null;
            place = _placeRepository.Get(Id);
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                _placeRepository.Delete(place);
                return true;
            }
        }

        public bool ConvertBase64ToImage(string fileBase64, string filePath)
        {
            try
            {
                string base64 = fileBase64.Substring(fileBase64.IndexOf(',') + 1);
                byte[] bytes = Convert.FromBase64String(base64);
                File.WriteAllBytes(filePath, bytes.ToArray());
                using (var imageFile = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
