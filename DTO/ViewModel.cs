using UserNotepad.Models;
namespace UserNotepad.DTOs;

public class ExtraDataViewModel{
    public User User {get; set;}
    public List<ExtraData>? Extras {get; set;}
    public ExtraDTO ExtraDTO {get; set;}

    public ExtraDataViewModel(User user){ User = user; ExtraDTO = new();}

    public ExtraDataViewModel(User user, List<ExtraData>? extras){
        User = user;
        Extras = extras;
        ExtraDTO = new();
    }

}