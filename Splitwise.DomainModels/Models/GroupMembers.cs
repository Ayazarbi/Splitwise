using System.ComponentModel.DataAnnotations.Schema;

public class GroupMembers{

    
    
    public int GroupMembersId { get; set; }
    [ForeignKey("Group")]
    public int GroupId { get; set; }
    public Group Group { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }

    public Applicationuser User{get; set;}
    
}