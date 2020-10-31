using System.ComponentModel.DataAnnotations.Schema;

public class Group
{
   
    public int GroupId { get; set; }
    public string Title { get; set; }
   [ForeignKey("Createdby")]
    public string CreatorIdId { get; set; }

    public Applicationuser Createdby{get; set;}
     public string Date { get; set; }
    public double Amount { get; set; }
}
