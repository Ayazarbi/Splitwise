using System.ComponentModel.DataAnnotations.Schema;

public class Friend
{
    public int FriendId { get; set; }
    
    [ForeignKey("User")]
    public string UserId { get; set; }

    public Applicationuser User{get; set;}

    [ForeignKey("Frnd")]
    public string FrndId { get; set; }

    public Applicationuser Frnd{get; set;}
}