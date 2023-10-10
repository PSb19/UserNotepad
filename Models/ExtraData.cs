using UserNotepad.DTOs;

namespace UserNotepad.Models;

public class ExtraData
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public string? Name {get; set;}
    public string? Content {get; set;}

    public ExtraData(){}
    public ExtraData(ExtraDTO extraDTO)
    {
        Name = extraDTO.Name;
        Content = extraDTO.Content;
        UserId = extraDTO.UserId;
    }
    public void UpdateExtraData(ExtraUpdateDTO dTO){
        Name = dTO.Name;
        Content = dTO.Content;
    }
}