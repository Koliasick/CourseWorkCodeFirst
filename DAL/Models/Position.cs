using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department? Department { get; set; }
        public List<HourlyRate> HourlyRates { get; set; }
    }
}
