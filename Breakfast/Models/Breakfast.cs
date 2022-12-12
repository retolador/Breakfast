namespace Breakfast.Models;
using ErrorOr;
using global::Breakfast.ServiceErrors;

public class Breakfast
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 3;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

   
    public Guid Id {get;}
    public string Name {get;}
    public string Description {get;}

    public DateTime StartDateTime {get;}
    public DateTime EndDateTime {get;}
    public DateTime LastModifiedDateTime {get;}
    public List<string> Savory {get;}

    public List<string> Sweet {get; }

    private Breakfast(
        Guid id, 
        string name, 
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        DateTime lastModifiedDateTime, 
        List<string> savory, 
        List<string> sweet)
    {
        //enforce invariants
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.StartDateTime = startDateTime;
        this.EndDateTime = endDateTime;
        this.LastModifiedDateTime = lastModifiedDateTime;
        this.Savory = savory;
        this.Sweet = sweet;
       
    }

    public static ErrorOr<Breakfast> Create(
         string name, 
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        List<string> savory, 
        List<string> sweet)
        {
            List<Error> errors = new();
            if (name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(Errors.Breakfast.InvalidName);
            }

            if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.Breakfast.InvalidDescription);
            }

            if(errors.Count > 0)
            {
                return errors;
            }

            return new Breakfast(
                Guid.NewGuid(),
                name,
                description,
                startDateTime,
                endDateTime,
                DateTime.UtcNow,
                savory,
                sweet);

        }

    
    
}