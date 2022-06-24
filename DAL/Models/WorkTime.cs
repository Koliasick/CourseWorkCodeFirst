using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WorkTime
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
        public DateTime WorkDuration { get; set; }
    }
}
