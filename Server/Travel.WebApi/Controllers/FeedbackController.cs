using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Travel.WebApi.ClientSide.Models;
using Travel.WebApi.Services;

namespace Travel.WebApi.Controllers
{
    [Route("api/place/{placeId:int}/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult<List<FeedbackClient>> GetAllFeedback(int placeId)
        {
            return _feedbackService.GetAll(placeId);
        }

        [HttpGet("{id:int}")]
        public ActionResult<FeedbackClient> GetFeedbackById(int placeId, int id)
        {
            return _feedbackService.GetById(placeId, id);
        }
        [HttpPut]
        public ActionResult<FeedbackClient> PutFeedback(int placeId, FeedbackClient model)
        {
            return _feedbackService.Update(placeId, model);
        }

        [HttpPost]
        public ActionResult<FeedbackClient> PostFeedback(int placeId, FeedbackClient model)
        {
            return _feedbackService.Create(placeId, model);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteFeedback(int placeId, int id)
        {
            return _feedbackService.Delete(placeId, id);
        }
    }
}
