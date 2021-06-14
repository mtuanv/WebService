using System;
using System.Collections.Generic;
using System.Linq;
using Travel.WebApi.ClientSide.Models;
using Travel.WebApi.DataAccess.Extensions;
using Travel.WebApi.DataAccess.Models;

namespace Travel.WebApi.Services
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
        private readonly IRepository<Feedbacks> _feedbackRepository;
        public FeedbackService(IRepository<Feedbacks> feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public List<FeedbackClient> GetAll(int PId)
        {
            var ListFeedback = _feedbackRepository.GetAll().Where(x => x.PlaceId == PId).ToList();
            if (ListFeedback.Count == 0)
                throw new Exception("There is no feedback for this place");
            var result = ListFeedback.Select(x => new FeedbackClient
            {
                Id = x.Id,
                RateStar = x.RateStar ?? 0,
                Comment = x.Comment,
                UserId = x.UserId,
                PlaceId = PId,
            }).ToList();
            return result;
        }
        public FeedbackClient GetById(int PId, int Id)
        {
            FeedbackClient result = new FeedbackClient();
            Feedbacks feedback = _feedbackRepository.GetAll().Where(x => x.PlaceId == PId && x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                result.Id = Id;
                result.RateStar = feedback.RateStar ?? 0;
                result.Comment = feedback.Comment;
                result.PlaceId = PId;
                result.UserId = feedback.UserId;
            };
            return result;
        }
        public FeedbackClient Create(int PId, FeedbackClient model)
        {
            var feedback = new Feedbacks
            {
                RateStar = model.RateStar,
                Comment = model.Comment,
                UserId = model.UserId,
                PlaceId = PId,
            };
            _feedbackRepository.Insert(feedback);
            return model;
        }
        public FeedbackClient Update(int PId, FeedbackClient model)
        {

            Feedbacks feedback = _feedbackRepository.GetAll().Where(x => x.PlaceId == PId && x.Id == model.Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                feedback.RateStar = model.RateStar;
                feedback.Comment = model.Comment;
                feedback.UserId = model.UserId;
                feedback.PlaceId = PId;

                _feedbackRepository.Update(feedback);
            }
            return model;
        }
        public bool Delete(int PId, int Id)
        {
            Feedbacks feedback = _feedbackRepository.GetAll().Where(x => x.PlaceId == PId && x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (feedback == null)
                throw new Exception("Feedback not found");
            else
            {
                _feedbackRepository.Delete(feedback);
                return true;
            }
        }
    }
}
