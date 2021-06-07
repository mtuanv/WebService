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
        List<FeedbackClient> GetAll(int PlaceId);
        FeedbackClient Create(FeedbackClient model);
        FeedbackClient Update(FeedbackClient model);
        bool Delete(int feedback_Id);
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
                CreatedBy = model.AccountId,
                CreatedDate = DateTime.UtcNow,
            };
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            return model;
        }
        public bool Delete(int Id)
        {
            Feedback feedback = null;
            feedback = _context.Feedback.Where(x => x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                _context.Feedback.Remove(feedback);
                _context.SaveChanges();
                return true;
            }
        }
        public List<FeedbackClient> GetAll(int PId)
        {
            var ListFeedback = _context.Feedback.Where(x => x.PlaceId == PId).ToList();
            var result = ListFeedback.Select(x => new FeedbackClient
            {
                Id = x.Id,
                Star = x.Star,
                Comment = x.Comment,
                AccountId = x.AccountId,
                PlaceId = PId,
            }).ToList();
            return result;
        }
        public FeedbackClient Update(FeedbackClient model)
        {
            Feedback feedback = null;
            feedback = _context.Feedback.Where(x => x.Id == model.Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                feedback.Star = model.Star;
                feedback.Comment = model.Comment;
                feedback.AccountId = model.AccountId;
                feedback.PlaceId = model.PlaceId;
                feedback.ModifiedDate = DateTime.UtcNow;
                feedback.ModifiedBy = model.AccountId;
                _context.SaveChanges();
            }
            return model;
        }
    }
}
