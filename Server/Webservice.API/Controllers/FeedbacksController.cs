using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webservice.API;
using Webservice.API.ClientSide.Models;
using Webservice.API.DataAccess.Models;
using Webservice.API.Services;

namespace Webservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly WebserviceContext _context;
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(WebserviceContext context, IFeedbackService feedbackService)
        {
            _context = context;
            _feedbackService = feedbackService;
        }

        // GET: api/Feedbacks
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
        //{
        //    return await _context.Feedback.ToListAsync();
        //}
        [HttpGet]
        public ActionResult<List<FeedbackClient>> GetAllFeedback()
        {
            return _feedbackService.GetAll();
        }
        // GET: api/Feedbacks/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Feedback>> GetFeedback(int id)
        //{
        //    var feedback = await _context.Feedback.FindAsync(id);

        //    if (feedback == null)
        //    {
        //        return NotFound();
        //    }

        //    return feedback;
        //}

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        //public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
        //{
        //    if (id != feedback.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(feedback).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FeedbackExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        public ActionResult<FeedbackClient> PutFeedback(FeedbackClient model)
        {
            return null;
        };
        // POST: api/Feedbacks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        //{
        //    _context.Feedback.Add(feedback);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetFeedback", new { id = feedback.Id }, feedback);
        //}
        [HttpPost]
        public ActionResult<FeedbackClient> PostFeedback(FeedbackClient model)
        {
            return _feedbackService.Create(model);
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feedback>> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedback.Remove(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedback.Any(e => e.Id == id);
        }
    }
}
