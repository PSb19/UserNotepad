using System.ComponentModel.DataAnnotations;

namespace UserNotepad.DTOs;
public class UserDTO
{
    [Required]
    [StringLength(50)]
    [RegularExpression(@"^[A-Z]+[a-z]*$")]
    public string? Name {get; set;}
    [Required]
    [StringLength(50)]
    [RegularExpression(@"^[A-Z]+[a-z]*$")]
    public string? Surname {get; set;}
    [DateValidation(ErrorMessage = "Date is invalid")]
    public string? BirthDate {get; set;}
    [Required]
    public string? Gender {get; set;}
}

public class ExtraDTO
{
    public int UserId {get; set;}
    public string? Name {get; set;}
    public string? Content {get; set;}
}
public class ExtraUpdateDTO
{
    public string? Name {get; set;}
    public string? Content {get; set;}
}