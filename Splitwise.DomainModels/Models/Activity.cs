using System.ComponentModel.DataAnnotations.Schema;

public class Activity{

    public int ActivityId { get; set; }
    
    [ForeignKey("User")]
    public string UserId { get; set; }

    public Applicationuser User{get; set;}
    public string Activitydata { get; set; }
    public string Date { get; set; }
}

