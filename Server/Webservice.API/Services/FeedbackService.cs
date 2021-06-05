using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webservice.API.ClientSide.Models;

namespace Webservice.API.Services
{
    public interface IFeedbackService
    {
        List<FeedbackClient> GetAll();
        FeedbackClient GetById(int FeedbackId);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
