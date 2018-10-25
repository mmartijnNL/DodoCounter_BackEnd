using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class EntriesController : Controller
    {
        DataContext _context;

        public EntriesController(DataContext context)
        {
            _context = context;
        }

    // GET: api/<controller>
    [HttpGet]
        public IEnumerable<EntryModel> Get()
        {
            //TODO make include work. 
            //var e1 = _context.Entries.Include("User").ToList();
            //var u1 = e1.First().User;
            //var e2 = _context.Entries.Include("Users").Where(e=>e.User.FirstName=="Martijn").ToList();
            //var u2 = e2.First().User;
            //var e3 = _context.Entries.Include(e => e.User).ToList();
            //var u3 = e3.First().User;
            //var e4 = (from e in _context.Entries select e).Include(b => b.User).ToList();
            //var u4 = e4.First().User;
            return _context.Entries.Include("User").ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public EntryModel Get(int id)
        {
            return _context.Entries.Include(e => e.User).SingleOrDefault(e => e.Id == id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]EntryModel value)
        {
            if (value != null)
            {
                _context.Entries.Add(value);
                _context.SaveChanges();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]EntryModel value)
        {
            _context.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Entries.Remove(_context.Entries.FirstOrDefault(e => e.Id ==id));
            _context.SaveChanges();
        }
    }
}
