using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace GreenWorldApp
{
    public class UserTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CompletionDate { get; set; }

        public int CompletedByUserId { get; set; }

        public UserTask()
        {
            
        }
    }
}
