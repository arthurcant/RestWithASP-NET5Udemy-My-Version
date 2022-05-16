using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")] // Specify the authorization role to access the controller.
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private IBookBusiness _iBookBusiness;
        private readonly ILogger<BookController> _iLogger;
        public BookController(ILogger<BookController> log, IBookBusiness iBookBusiness)
        {
            _iBookBusiness = iBookBusiness;
            _iLogger = log;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetAllBooks(){
            return Ok(_iBookBusiness.FindAll());
        }       

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetBookById(long id){
            var book = _iBookBusiness.FindById(id);
            
            if(book == null) return NotFound();
            
            return Ok(book);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult CreateBook([FromBody] Book book)
        {
            if(book == null) return BadRequest();

            return Ok(_iBookBusiness.Create(book));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult UpdateBook([FromBody] Book book)
        {
            if(book == null) return BadRequest();

            return Ok(_iBookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if(id <= 0) return BadRequest();

            _iBookBusiness.Delete(id);
            return NoContent();
        }
        

    }
}