using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext _db;

        public LibraryController(LibraryContext context) 
        {
            _db = context;
        }

        // localhost:44358/api/Library


        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        [HttpGet]
        public IActionResult GetBook(int id)
        {
            Book bookFromDb = _db.Books.SingleOrDefault(b => b.Id == id);

            if(bookFromDb == null)
            {
                return NotFound();
            }

            return Ok(bookFromDb);
        }

        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks()
        {
            var allBooks = _db.Books.ToList();

            if(allBooks.Count == 0)
            {
                return Ok("No books in database");
            }



            return Ok(allBooks);
        }

        [HttpPut]
        public IActionResult UpdateBook(Book book)
        {
            Book bookFromDb = _db.Books.SingleOrDefault(b => b.Id == book.Id);

            if(bookFromDb == null)
            {
                return NotFound();
            }

            bookFromDb.Title = book.Title;
            bookFromDb.PageCount = book.PageCount;

            _db.SaveChanges();

            return Ok("Updated book successfully");
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            Book bookFromDb = _db.Books.SingleOrDefault(b => b.Id == id);

            if (bookFromDb == null)
            {
                return NotFound();
            }

            _db.Remove(bookFromDb);

            _db.SaveChanges();

            return Ok("Deleted Book");
        }
    }
}
