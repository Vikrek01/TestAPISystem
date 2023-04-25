using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAPISystem.Models;
using TestAPISystem.Repository;

namespace TestAPISystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Book> _genericRepository;
        /*private readonly IGenericRepository<Feedback> _feedback;*/

        private readonly IWebHostEnvironment _webHostEnvironment;
        //public EmployeeController(IGenericRepository<Book> employee, IGenericRepository<Feedback> feedback, IWebHostEnvironment webHostEnvironment)
        public EmployeeController(IGenericRepository<Book> employee, IWebHostEnvironment webHostEnvironment)
        {
            _genericRepository = employee;
            //_feedback = feedback;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("all")]
        public IActionResult GetAllEmployee()
        {
            var result = _genericRepository.GetAll();
            if (result.Result != null)
            {
                return Ok(result.Result);
            }
            return NotFound();
        }

        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var result = _genericRepository.Get(id);
            if (result.Result != null)
            {
                return new JsonResult((Book)result.Result);
            }
            return NotFound();
        }

        [HttpPost("add")]
        public IActionResult AddEmployee(Book model)
        {
            var employee = new Book
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Author = model.Author,
            };
            _genericRepository.Insert(employee);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> UpdateEmployee(Book model)
        {
            await _genericRepository.Update(model);
            return Ok();
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var res = await _genericRepository.Delete(id);
            return Ok(res);
        }

        /*[HttpPost("addFeedback")]
        public IActionResult AddFeedback(Feedback model)
        {
            _feedback.Insert(model);
            return Ok();
        }
        [HttpGet("feedbacks/{id}")]
        public IActionResult GetFeedbacks(int id)
        {
            var result = _feedback.GetAll();
            if (result.Result != null)
            {
                var Result = result.Result.Where(x => x.EmployeeId == id).ToList();
                if (Result.Count > 0)
                    return Ok(Result);
                return NotFound();
            }
            return NotFound();
        }*/
    }
}
