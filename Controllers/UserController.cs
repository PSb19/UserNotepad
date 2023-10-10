using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserNotepad.Models;
using UserNotepad.DTOs;

namespace UserNotepad.Controllers
{
    public class UserController : Controller
    {
        private readonly Context.AppContext _context;
        
        public UserController(Context.AppContext context){
            _context = context;
        }

        public async Task<IActionResult> ListUsers()
        {
            await InsertTestUser();
            List<User> users = await _context.Users.ToListAsync();
            ViewData["users"] = users;
            return View(users);
        }
        [HttpGet]
        public IActionResult CreateNewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser(UserDTO userDTO)
        {
            var user = new User(userDTO);
            if(user.BirthDate <= DateOnly.FromDateTime(DateTime.Today)){
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ListUsers");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(int? id, UserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            user.UpdateUserData(userDTO);
            _context.Update(user);
            await _context.SaveChangesAsync();
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var extras = await _context.ExtraDatas.Where(e => e.UserId == id).ToListAsync();
            _context.ExtraDatas.RemoveRange(extras);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListUsers");
        }


        private async Task InsertTestUser(){
            UserDTO transfer = new()
            {
                Name = "Aaron",
                Surname = "Stone",
                BirthDate = DateOnly.FromDateTime(DateTime.Now).ToString(),
                Gender = "male"
            };
            User user = new(transfer);
            _context.Users.Add(user);
            ExtraDTO dto = new()
            {
                Name = "aaa",
                Content = "aaa",
                UserId = 1
            };
            ExtraData extraData = new(dto);
            _context.ExtraDatas.Add(extraData);
            await _context.SaveChangesAsync();
        }
    }
}