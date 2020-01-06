using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SQLite;

namespace GreenWorldApp
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsCurrent { get; set; }

        public User()
        {
            
        }

        public override string ToString()
        {
            return Email + ", " + Password;
        }
    }
}
