using BookLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private DataBaseContext db;
        public BooksController(DataBaseContext context)
        {
            db = context;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            return db.Books.ToList();
        }

        [HttpPost("Post")]
        public void PostBook([FromBody] Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }
    }
}
