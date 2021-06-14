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
        bool Create([FromForm] PlaceClient model);
        bool Update([FromForm] PlaceClient model);
        bool Delete(int place_Id);
    }
    public class PlaceService : IPlaceService
    {
        private readonly IRepository<Places> _placeRepository;
        private readonly IRepository<Images> _imageRepository;
        private readonly IRepository<Feedbacks> _feedbackRepository;

        public static IWebHostEnvironment _environment;
        public PlaceService(IRepository<Places> placeRepository, IRepository<Images> imageRepository, IRepository<Feedbacks> feedbackRepository, IWebHostEnvironment environment)
        {
            _placeRepository = placeRepository;
            _imageRepository = imageRepository;
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
            //place = _context.Place.Where(x => x.Id == Id).FirstOrDefault();
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
        public bool Create([FromForm] PlaceClient model)
        {
            try
            {
                var place = new Places
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = model.UserId,
                };
                _placeRepository.Insert(place);

                foreach (var img in model.Files)
                {
                    string fileName = string.Concat(Path.GetFileNameWithoutExtension(img.FileName),
                                      DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                                      Path.GetExtension(img.FileName));
                    using (FileStream fileStream = File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
                    {
                        img.CopyTo(fileStream);
                        fileStream.Flush();
                        string path = "\\Upload\\" + fileName;
                        int placeid = _placeRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;//_context.Place.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                        var image = new Images
                        {
                            Path = path,
                            PlaceId = placeid
                        };
                        _imageRepository.Insert(image);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update([FromForm] PlaceClient model)
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
                    _placeRepository.Update(place);
                    List<Images> lstImage = _imageRepository.GetAll().Where(x => x.PlaceId == model.Id).ToList();//_context.Image.Where(x => x.PlaceId == model.Id).Select(x => x).ToList();
                    foreach (var img in lstImage)
                    {
                        File.Delete(_environment.WebRootPath + img.Path);
                        _imageRepository.Delete(img);
                        //_context.Remove(img);
                        //_context.SaveChanges();
                    }
                    foreach (var img in model.Files)
                    {
                        string fileName = string.Concat(Path.GetFileNameWithoutExtension(img.FileName),
                                          DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                                          Path.GetExtension(img.FileName));
                        using (FileStream fileStream = File.Create(_environment.WebRootPath + "\\Upload\\" + fileName))
                        {
                            img.CopyTo(fileStream);
                            fileStream.Flush();
                            string path = "\\Upload\\" + fileName;
                            int placeid = _imageRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;//_context.Place.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                            var image = new Images
                            {
                                Path = path,
                                PlaceId = placeid
                            };
                            _imageRepository.Insert(image);
                            //_context.Image.Add(image);
                            //_context.SaveChanges();
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
        public bool Delete(int Id)
        {
            Places place = null;
            place = _placeRepository.Get(Id);//_context.Place.Where(x => x.Id == Id).FirstOrDefault();
            if (place == null)
                throw new Exception("Place not found");
            else
            {
                List<Feedbacks> lstFeedback = _feedbackRepository.GetAll().Where(x => x.PlaceId == Id).ToList();//_context.Feedback.Where(x => x.PlaceId == Id).Select(x => x).ToList();
                List<Images> lstImage = _imageRepository.GetAll().Where(x => x.PlaceId == Id).ToList();//_context.Image.Where(x => x.PlaceId == Id).Select(x => x).ToList();
                foreach (var fb in lstFeedback)
                {
                    //_context.Remove(fb);
                    //_context.SaveChanges();
                    _feedbackRepository.Delete(fb);
                }
                foreach (var img in lstImage)
                {
                    File.Delete(_environment.WebRootPath + img.Path);
                    //_context.Remove(img);
                    _imageRepository.Delete(img);
                }
                //_context.Remove(place);
                //_context.SaveChanges();
                _placeRepository.Delete(place);
                return true;
            }
        }
    }
}
