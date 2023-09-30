using System.ComponentModel.DataAnnotations;

namespace AppspaceChallenge.API.DTO.Input.Managers
{
  public class IntelligentBillboardRequest : ManagerBaseRequest, IValidatableObject
  {
    [Required(ErrorMessage = "The 'From' date is required.")]
    public DateTime From { get; set; }

    [Required(ErrorMessage = "The 'To' date is required.")]
    public DateTime To { get; set; }

    [Required(ErrorMessage = "The number of screens in big rooms is required.")]
    public int ScreensInBigRooms { get; set; }

    [Required(ErrorMessage = "The number of screens in small rooms is required.")]
    public int ScreensInSmallRooms { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (To < From)
      {
        yield return new ValidationResult(
            errorMessage: "'To' date  must be greater than 'From' date",
            memberNames: new[] { "To" }
       );
      }
    }
  }
}
