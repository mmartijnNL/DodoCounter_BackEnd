using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    [Serializable]
    public class EntryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Amount { get; set; }
        public String Location { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }

        public EntryModel() { }

        public EntryModel(int amount, String location, DateTime dateTime):this()
        {
            Amount = amount;
            Location = location;
            DateTime = dateTime;
        }
        public EntryModel(int amount, String location, DateTime dateTime, UserModel user) : this(amount, location, dateTime)
        {
            User = user;
        }
    }
}
