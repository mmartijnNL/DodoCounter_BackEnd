using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public enum Role
    {
        Watcher, Administrator, Manager
    }

    public class UserModel : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String Password { get; set; }

        public Role Role { get; set; }

        public UserModel() { }

        public UserModel(String firstName, String lastName):this()
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public UserModel(String firstName, String lastName, String emailAddress, String passWord, Role role) :this(firstName,lastName)
        {
            EmailAddress = emailAddress;
            Password = passWord;
            Role = role;
        }
    }
}
