namespace AppspaceChallenge.API.Model.BeezyCinema
{
  public class NewsletterSignUp
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool Advert { get; set; }
    public bool WordOfMouth { get; set; }
    public bool Other { get; set; }
    public string? Reason { get; set; }
    public DateTime SignUpDate { get; set; }
  }
}
