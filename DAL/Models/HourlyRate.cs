using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HourlyRate
    {
        public int Id{ get; set; }
        public float HourlyPayment { get; set; }
        public DateTime? ValidTill { get; set; }
        public Position Position { get; set; }
    }
}
