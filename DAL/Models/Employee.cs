using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum Roles 
    {
        User,
        Admin
    }
    public class Employee 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Middlename { get; set; }
        public string Address { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? AdditionalPassportData { get; set; }
        public List<EmployeePosition> Positions { get; set; }
        public int RegistrationNumber { get; set; }
        public byte[]? Photo { get; set; }
        public string Username { get; set; }
        public Roles Role { get; set; }
    }
}
