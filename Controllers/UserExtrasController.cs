using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserNotepad.Models;
using UserNotepad.DTOs;

namespace UserNotepad.Controllers;
public class UserExtrasController : Controller
{
    private readonly Context.AppContext _context;
    
    public UserExtrasController(Context.AppContext context){
        _context = context;
    }
    
    public async Task<IActionResult> Details(int? id){
        var user = await _context.Users.FindAsync(id);
        if(user == null)
        {
            return NotFound();
        }
        var extras = await _context.ExtraDatas.Where(d=> d.UserId == id).ToListAsync();
        ExtraDataViewModel transfer = new(user, extras);
        return View(transfer);
    }
    [HttpPost]
    public async Task<IActionResult> CreateExtra(ExtraDTO extraDTO){
        var user = await _context.Users.FindAsync(extraDTO.UserId);
        if(user == null)
        {
            return NotFound();
        }
        ExtraData extraData = new(extraDTO);
        _context.Add(extraData);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", new {id=extraDTO.UserId});
    }
    public async Task<IActionResult> EditExtra(int id){
        var extra = await _context.ExtraDatas.FindAsync(id);
        if(extra == null)
        {
            return NotFound();
        }
        return View(extra);
    }
    [HttpPost]
    public async Task<IActionResult> EditExtra(int id, ExtraUpdateDTO extraDTO){
        var extra = await _context.ExtraDatas.FindAsync(id);
        if(extra == null)
        {
            return NotFound();
        }
        extra.UpdateExtraData(extraDTO);
        _context.Update(extra);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", new { id=extra.UserId });
    }
    [HttpPost, ActionName("DeleteExtra")]
    public async Task<IActionResult> DeleteExtra(int id, int extraId){
        var extra = await _context.ExtraDatas.FindAsync(extraId);
        if(extra == null)
        {
            return NotFound();
        }
        _context.Remove(extra);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", new { id });
    }
}