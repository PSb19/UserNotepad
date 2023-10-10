using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserNotepad.Models;
using ClosedXML.Excel;
using System.Data;

namespace UserNotepad.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly Context.AppContext _context;
        
        public StatisticsController(Context.AppContext context){
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            DataTable dt = new("Grid");
            dt.Columns.AddRange(
                new DataColumn[6] { 
                    new("Name"),
                    new("Surname"),
                    new("Title"),
                    new("Gender"),
                    new("Birth Date"),
                    new("Age") 
                }
            );
            List<User> users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                dt.Rows.Add(user.Name, user.Surname, GiveUserTitle(user.Gender), user.Gender, user.BirthDate, GiveUserAge(user.BirthDate));
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", DateTime.Now.ToString()+".xlsx");
        }

        private string GiveUserTitle(string? userGender){
            if(userGender == null){
                return "null";
            }
            var userGenderUppercase = userGender.ToUpper();
            if(userGenderUppercase.Equals("FEMALE")){
                return "Pani";
            }
            if(userGenderUppercase.Equals("MALE")){
                return "Pan";
            }
            return "Pax";
        }
        private int GiveUserAge(DateOnly birthDate){
            int age = DateTime.Now.Year - birthDate.Year;
            if (birthDate > DateOnly.FromDateTime(DateTime.Now.AddYears(-age))) 
                age--;
            return age;
        }
    }
}