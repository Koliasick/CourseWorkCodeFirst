using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Employee Reciever { get; set; }
        public float Ammount { get; set; }
        public DateTime Date { get; set; }
    }
}
