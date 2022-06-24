using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkCodeFirst
{
    public class WorkDayOnPosition
    {
        public Position Position { get; set; }
        public DateTime Date { get; set; }
        public DateTime Duration { get; set; }
        public float HourlyRate { get; set; }
    }

    public class ComboboxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name + $"   (Id:{Id})";
        }
    }

    public class Business
    {
        public (int day, int month)[] holidays { get; set; } = { (1, 1), (1, 7) };
        public const int NormalWorkDuration = 8;

        private ApplicationDataContext _context;

        public Business(ApplicationDataContext context)
        {
            _context = context;
        }

        public List<WorkDayOnPosition> GetWorkDays(int employeeId)
        {
            var hres = _context.WorkTimes
                .AsNoTracking()
                .Where((wt) => wt.Employee.Id == employeeId)
                .Join(_context.EmployeePositions,
                    (wt) => wt.Employee.Id,
                    (ep) => ep.Employee.Id,
                    (wt, ep) =>
                    new
                    {
                        Id = ep.Id,
                        EmployeeId = ep.Employee.Id,
                        Position = ep.Position,
                        Date = wt.Date,
                        Duration = wt.WorkDuration,
                        HourlyRate = ep.Position.HourlyRates
                            .Select((hr) =>
                                new HourlyRate
                                {
                                    Id = hr.Id,
                                    HourlyPayment = hr.HourlyPayment,
                                    Position = hr.Position,
                                    ValidTill = hr.ValidTill ?? DateTime.Now
                                })
                            .Where((hr) => hr.ValidTill > wt.Date)
                            .OrderBy((hr) => hr.ValidTill)
                            .Select((hr) => hr.HourlyPayment)
                            .First()
                    })
                .Where((a) => a.Id == _context.EmployeePositions
                    .Select((ep) =>
                        new EmployeePosition
                        {
                            Id = ep.Id,
                            Employee = ep.Employee,
                            Position = ep.Position,
                            ValidTill = ep.ValidTill ?? DateTime.Now
                        })
                    .Where((ep) => ep.Employee.Id == a.EmployeeId)
                    .Where((ep) => ep.ValidTill > a.Date)
                    .OrderBy((ep) => ep.ValidTill)
                    .First()
                    .Id
                 );

            return hres.Select((pr) => 
                new WorkDayOnPosition {
                    Position = pr.Position,
                    Date = pr.Date,
                    Duration = pr.Duration,
                    HourlyRate = pr.HourlyRate
                })
                .ToList();
        }

        public Employee GetEmployeeInfo(string username)
        {
            return _context.Employees
                .AsNoTracking()
                .Where((e) => e.Username == username)
                .Include((e) => e.Positions.Where((position) => position.ValidTill == null))
                .ThenInclude((ep) => ep.Position)
                .ThenInclude((p) => p.Department)
                .Include((e) => e.Positions.Where((position) => position.ValidTill == null))
                .ThenInclude((ep) => ep.Position)
                .ThenInclude((p) => p.Department)
                .ThenInclude((d) => d.Head)
                .Include((e) => e.Positions.Where((position) => position.ValidTill == null))
                .ThenInclude((ep) => ep.Position)
                .ThenInclude((p) => p.HourlyRates.Where((hr) => hr.ValidTill == null))
                .First();
        }

        public List<ComboboxItem> GetWorkingEployeesList()
        {
            return _context.Employees
                .AsNoTracking()
                .Where((e) => e.Positions.Any((p) => p.ValidTill == null))
                .Select((e) => new ComboboxItem { Id = e.Id, Name = e.Name })
                .ToList();
        }

        public List<ComboboxItem> GetPositionsList()
        {
            return _context.Positions
                .AsNoTracking()
                .Select((p) => new ComboboxItem { Id = p.Id, Name = p.Name })
                .ToList();
        }

        public double GetPaymentsToEmployee(int employeeId)
        {
            return _context.Payments
                .Where((p) => p.Reciever.Id == employeeId)
                .Select((p) => p.Ammount)
                .Sum();
        }

        public double CalculateAmountOfMoneyEarnedDuringWorkDays(List<WorkDayOnPosition> workDays)
        {
            return workDays
                .Where(wd => wd.Position.Id != -1)
                .Aggregate(0d,
                    (accumulator, element) =>
                    {
                        float numberOfHours = (float)element.Duration.Hour + element.Duration.Minute / 60f;
                        float numberOfDoublePaidHours = 0;
                        if (numberOfHours > NormalWorkDuration)
                        {
                            numberOfDoublePaidHours = numberOfHours - NormalWorkDuration;
                            numberOfHours -= NormalWorkDuration;
                        }
                        (int, int) date = (element.Date.Day, element.Date.Month);
                        double earnedMoney = (numberOfHours + numberOfDoublePaidHours * 2) * element.HourlyRate;
                        if (holidays.Contains(date) || element.Date.DayOfWeek == DayOfWeek.Sunday || element.Date.DayOfWeek == DayOfWeek.Saturday)
                        {
                            return accumulator + earnedMoney * 3;
                        }
                        else
                        {
                            return accumulator + earnedMoney;
                        }
                    });
        }

        public bool ChangeMyBasicData(string name, string surname, string middleName, string address, DateTime birthday, string placeOfBirths, string additionalData, byte[] photo)
        {
            try
            {
                _context.ModifyBasicUserData(name, surname, middleName, address, birthday, placeOfBirths, additionalData, photo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeUserData(string username, string name, string surname, string middleName, string address, DateTime birthday, string placeOfBirdth, string additionalData, int registrationNumber, int positionId, byte[] photo, Roles role)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Employee _employeeDisplayed = _context.Employees
                        .Where((e) => e.Username == username)
                        .First();

                    _employeeDisplayed.Name = name;
                    _employeeDisplayed.Surname = surname;
                    _employeeDisplayed.Middlename = middleName;
                    _employeeDisplayed.Address = address;
                    _employeeDisplayed.BirthdayDate = birthday;
                    _employeeDisplayed.PlaceOfBirth = placeOfBirdth;
                    _employeeDisplayed.AdditionalPassportData = additionalData;
                    _employeeDisplayed.RegistrationNumber = registrationNumber;

                    var currentPosition = _context.EmployeePositions
                        .Where((ep) => ep.Employee.Id == _employeeDisplayed.Id && ep.ValidTill == null)
                        .Include((ep) => ep.Position)
                        .First();

                    var selectedPosition = _context.Positions
                        .Where((p) => p.Id == positionId)
                        .First();

                    if (selectedPosition.Id != currentPosition.Position.Id)
                    {
                        currentPosition.ValidTill = DateTime.Now;
                        EmployeePosition employeePosition =
                            new EmployeePosition
                            {
                                Employee = _employeeDisplayed,
                                Position = selectedPosition
                            };
                        _context.EmployeePositions.Add(employeePosition);
                    }

                    _employeeDisplayed.Photo = photo;

                    if (_employeeDisplayed.Role != role)
                    {
                        if (role == Roles.User)
                        {
                            _context.RemoveAdminRights(username);
                            _employeeDisplayed.Role = Roles.User;
                        }
                        else 
                        {
                            _context.AddAdminRights(username);
                            _employeeDisplayed.Role = Roles.Admin;
                        }
                    }

                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch 
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool ReportMyHours(DateTime date, DateTime duration)
        {
            return _context.ReportHours(date, duration);
        }
    }
}
