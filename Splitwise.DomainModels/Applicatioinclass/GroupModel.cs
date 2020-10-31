using System.Collections.Generic;

public class GroupModel{

     public Group Group { get; set; }

    public List<Expense> Expenses{get;set;}

    public List<Applicationuser> Members { get; set; }

    public List<string> MembersId{get;set;}

    
}