using System.ComponentModel.DataAnnotations;
using UserNotepad.DTOs;

namespace UserNotepad.Models;

public class User
{
    public int Id {get; set;}
    [Required]
    [StringLength(50)]
    [RegularExpression(@"^[A-Z]+[a-z]*$")]
    public string? Name {get; set;}
    [Required]
    [StringLength(150)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public string? Surname {get; set;}
    [Required]
    [DisplayFormat( DataFormatString = "{0:dd.MM.yyyy}")]
    [Display(Name = "Birth Date")]
    [DateValidation(ErrorMessage = "Date is invalid")]
    public DateOnly BirthDate {get; set;}
    [Required]
    public string? Gender {get; set;}

    public User(){}
    public User(UserDTO transfer){
        Name = transfer.Name;
        Surname = transfer.Surname;
        Gender = transfer.Gender;
        if(transfer.BirthDate != null)
        BirthDate = DateOnly.Parse(transfer.BirthDate);
    }
    public void UpdateUserData(UserDTO transfer){
        Name = transfer.Name;
        Surname = transfer.Surname;
        Gender = transfer.Gender;
        if(transfer.BirthDate != null)
        BirthDate = DateOnly.Parse(transfer.BirthDate);
    }
}