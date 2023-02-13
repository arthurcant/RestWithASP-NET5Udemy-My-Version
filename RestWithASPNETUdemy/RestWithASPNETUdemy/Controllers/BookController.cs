using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using System.Collections.Generic;

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

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<BookVO>))]
        [ProducesResponseType(203)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string title,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_iBookBusiness.FindWithPagedSearch(title, sortDirection, pageSize, page));
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
        public IActionResult CreateBook([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();

            return Ok(_iBookBusiness.Create(book));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult UpdateBook([FromBody] BookVO book)
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