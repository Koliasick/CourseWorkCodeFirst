using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime VacationStart { get; set; }
        public DateTime VacationEnd { get; set; }
    }
}
