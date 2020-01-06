using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GreenWorldApp
{
    public class Voucher
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string Name => "Voucher Number: " + Id.ToString();

        //public DateTime ExpiryDate => new DateTime().Date.AddMonths(12).AddDays(-1);
    }
}
