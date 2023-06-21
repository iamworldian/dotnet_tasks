namespace bank_teller.User;

public class UserClass
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }
    

    public UserClass(string email,string name,string contact)
    {
        this.Email = email;
        this.Name = name;
        this.Contact = contact;
    }
    
    override 
    public string ToString()
    {
        return $"Name : {Name}\n" +
               $"Email: {Email}\n" +
               $"Contact: {Contact}";
    }
    
    
}


