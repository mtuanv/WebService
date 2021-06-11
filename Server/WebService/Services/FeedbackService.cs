using System;
using System.Collections.Generic;
using System.Linq;
using WebService.ClientSide.Models;
using WebService.DataAccess.Models;

namespace WebService.Services
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
        private readonly WsContext _context;
        public FeedbackService(WsContext context)
        {
            _context = context;
        }
        public FeedbackClient Create(int PId, FeedbackClient model)
        {
            var feedback = new Feedback
            {
                Star = model.Star,
                Comment = model.Comment,
                UserId = model.UserId,
                PlaceId = PId,
            };
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            return model;
        }
        public bool Delete(int PId, int Id)
        {
            Feedback feedback = null;
            feedback = _context.Feedback.Where(x => x.PlaceId == PId && x.Id == Id).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                _context.Remove(feedback);
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
                Star = x.Star ?? 0,
                Comment = x.Comment,
                UserId = x.UserId,
                PlaceId = PId,
            }).ToList();
            return result;
        }
        public FeedbackClient GetById(int PId, int Id)
        {
            Feedback feedback = new Feedback();
            FeedbackClient result = new FeedbackClient();
            feedback = _context.Feedback.Where(x => x.PlaceId == PId && x.Id == Id).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                result.Id = Id;
                result.Star = feedback.Star ?? 0;
                result.Comment = feedback.Comment;
                result.PlaceId = PId;
                result.UserId = feedback.UserId;
            };
            return result;
        }
        public FeedbackClient Update(int PId, FeedbackClient model)
        {

            Feedback feedback = new Feedback();
            feedback = _context.Feedback.Where(x => x.Id == model.Id && x.PlaceId == PId).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                feedback.Star = model.Star;
                feedback.Comment = model.Comment;
                feedback.UserId = model.UserId;
                feedback.PlaceId = PId;

                _context.SaveChanges();
            }
            return model;
        }
    }
}
