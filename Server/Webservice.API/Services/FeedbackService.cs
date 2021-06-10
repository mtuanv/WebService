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
        FeedbackClient GetById(int PlaceId, int Id);
        FeedbackClient Create(int PlaceId, FeedbackClient model);
        FeedbackClient Update(int PlaceId, FeedbackClient model);
        bool Delete(int placeId, int feedback_Id);
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly WebserviceContext _context;
        public FeedbackService(WebserviceContext context)
        {
            _context = context;
        }
        public FeedbackClient Create(int PId, FeedbackClient model)
        {
            var feedback = new Feedback
            {
                Star = model.Star,
                Comment = model.Comment,
                AccountId = model.AccountId,
                PlaceId = PId,
                CreatedBy = model.AccountId,
                CreatedDate = DateTime.UtcNow,
            };
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            return model;
        }
        public bool Delete(int PId, int Id)
        {
            Feedback feedback = null;
            feedback = _context.Feedback.Where(x => x.PlaceId == PId && x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                feedback.DeletedDate = DateTime.UtcNow;
                _context.SaveChanges();
                return true;
            }
        }
        public List<FeedbackClient> GetAll(int PId)
        {
            var ListFeedback = _context.Feedback.Where(x => x.PlaceId == PId).ToList();
            if (ListFeedback.Count == 0)
                throw new Exception("There is no feedback for this place");
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
        public FeedbackClient GetById(int PId, int Id)
        {
            Feedback feedback = new Feedback();
            FeedbackClient result = new FeedbackClient();
            feedback = _context.Feedback.Where(x => x.PlaceId == PId && x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                result.Id = Id;
                result.Star = feedback.Star;
                result.Comment = feedback.Comment;
                result.PlaceId = PId;
                result.AccountId = feedback.AccountId;
            };
            return result;
        }
        public FeedbackClient Update(int PId, FeedbackClient model)
        {

            Feedback feedback = new Feedback();
            feedback = _context.Feedback.Where(x => x.Id == model.Id && x.PlaceId == PId && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                feedback.Star = model.Star;
                feedback.Comment = model.Comment;
                feedback.AccountId = model.AccountId;
                feedback.PlaceId = PId;
                feedback.ModifiedDate = DateTime.UtcNow;
                feedback.ModifiedBy = model.AccountId;
                _context.SaveChanges();
            }
            return model;
        }
    }
}
