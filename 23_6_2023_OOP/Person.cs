namespace _23_6_2023_OOP;

public class Person
{

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }

    public Decimal salary;
    public Decimal Salary
    {
        get { return salary; }
        set
        {
            if (value > 0)
            {
                salary = value;
            }
            else throw ;
        }
    }
    


    public Person(string firstName, string middleName, string lastName): this(firstName,lastName)
    {
        MiddleName = middleName;
    }
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Person(Person p):this(p.FirstName, p.MiddleName, p.LastName) { }
    public Person()
    {
    }
    
    public string getFullName()
    {
        return $"{FirstName} {MiddleName} {LastName}";
    }
}

