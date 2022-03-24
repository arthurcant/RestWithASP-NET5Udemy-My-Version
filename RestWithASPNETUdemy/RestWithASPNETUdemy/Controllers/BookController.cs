using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
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
        public IActionResult GetAllBooks(){
            return Ok(_iBookBusiness.FindAll());
        }       

        [HttpGet("{id}")]
        public IActionResult GetBookById(long id){
            var book = _iBookBusiness.FindById(id);
            
            if(book == null) return NotFound();
            
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            if(book == null) return BadRequest();

            return Ok(_iBookBusiness.Create(book));
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] Book book)
        {
            if(book == null) return BadRequest();

            return Ok(_iBookBusiness.Update(book));
        }

        [HttpDelete("/{id}")]
        public IActionResult Delete(long id)
        {
            if(id <= 0) return BadRequest();

            _iBookBusiness.Delete(id);
            return NoContent();
        }
        

    }
}