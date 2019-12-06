﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GreenWorldApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Email { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return Email + ", " + Password;
        }
    }
}
