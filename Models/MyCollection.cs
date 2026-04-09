using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace moomug_project.Models;

public class MyCollection
{
    public MyCollection(){}

    // Constructor here to add dummy data
    public MyCollection(string description, string mugDetails, bool hasLabel, bool isUsed)
    {
        MyDescription = description;
        MyMugDetails = mugDetails;
        MyHasLabel = hasLabel;
        MyIsUsed = isUsed;
    }
    
    [Key]
    public int Id { get; set; } = 0; 
    
    [Required]
    public string ApplicationUserId { get; set; }                      //Has removed nullability
    public ApplicationUser ApplicationUser { get; set; }               //Has removed nullability
    
    [Required]
    public int GlobalMugCollectionId { get; set; } = 0;
    public GlobalMugCollection? GlobalMugCollection { get; set; }
    
    [Required]
    [DisplayName("Description")]
    public string MyDescription { get; set; } = string.Empty;                      //Do we need this? I guess a sql-request can calculate #of mugs based of off GlobalId 
    
    [Required]
    [DisplayName("Details")]
    public string MyMugDetails { get; set; } = string.Empty;
    
    [Required]
    [DisplayName("Label Intact")]
    public bool MyHasLabel  { get; set; } = false;
    
    [Required]
    [DisplayName("Is Used")]
    public bool MyIsUsed { get; set; } = false;
}