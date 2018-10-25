using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _context.Users;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return _context.Users.SingleOrDefault(e => e.Id == id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]UserModel value)
        {
            if (value != null)
            {
                _context.Users.Add(value);
                _context.SaveChanges();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UserModel value)
        {
            _context.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.FirstOrDefault(e => e.Id == id));
            _context.SaveChanges();
        }
    }
}
