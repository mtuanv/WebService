using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webservice.API.ClientSide.Models;
using Webservice.API.DataAccess.Models;
using Webservice.API.Services;

namespace Webservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("{id}")]
        public ActionResult<List<FeedbackClient>> GetAllFeedback(int PlaceId)
        {
            return _feedbackService.GetAll(PlaceId);
        }

        [HttpPut]
        public ActionResult<FeedbackClient> PutFeedback(FeedbackClient model)
        {
            return _feedbackService.Update(model);
        }

        [HttpPost]
        public ActionResult<FeedbackClient> PostFeedback(FeedbackClient model)
        {
            return _feedbackService.Create(model);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteFeedback(int id)
        {
            return _feedbackService.Delete(id);
        }
    }
}
