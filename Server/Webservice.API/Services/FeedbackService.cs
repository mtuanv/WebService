using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webservice.API.ClientSide.Models;
using Webservice.API.DataAccess.Models;

namespace Webservice.API.Services
{
    public interface IFeedbackService
    {
        List<FeedbackClient> GetAll();
        FeedbackClient Create(FeedbackClient model);
        FeedbackClient Update(FeedbackClient model);
        bool Delete(FeedbackClient model);
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly WebserviceContext _context;
        public FeedbackService(WebserviceContext context)
        {
            _context = context;
        }
        public FeedbackClient Create(FeedbackClient model)
        {
            var feedback = new Feedback
            {
                Star = model.Star,
                Comment = model.Comment,
                AccountId = model.AccountId,
                PlaceId = model.PlaceId,
            };
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            return model;
        }
        public bool Delete(FeedbackClient model)
        {
            throw new NotImplementedException();
        }
        public List<FeedbackClient> GetAll()
        {
            var ListFeedback = _context.Feedback.ToList();
            var result = ListFeedback.Select(x => new FeedbackClient
            {
                Id = x.Id,
                Star = x.Star,
                Comment = x.Comment,
                AccountId = x.Id,
                PlaceId = x.Id,
            }).ToList();
            return result;
        }
        public FeedbackClient Update(FeedbackClient model)
        {
            Feedback feedback = null;
            feedback = _context.Feedback.Where(x => x.Id == model.Id).FirstOrDefault();

            //{
            //    Star = model.Star,
            //    Comment = model.Comment,
            //    AccountId = model.AccountId,
            //    PlaceId = model.PlaceId,
            //};
            return null;
        }
    }
}
