using System.ComponentModel.DataAnnotations;

public class DateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        DateTime d = Convert.ToDateTime(value);
        return DateOnly.FromDateTime(d) <= DateOnly.FromDateTime(DateTime.Now);
    }
}