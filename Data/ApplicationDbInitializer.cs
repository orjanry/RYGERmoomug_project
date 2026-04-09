using Microsoft.AspNetCore.Identity;
using moomug_project.Models;
using System.Text.Json;

namespace moomug_project.Data;

public class ApplicationDbInitializer
{
     public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        
        // Read JSON file to populate GlobalMugCollections
        string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "JsonData", "GlobalCollection.json");
        var mugsFromJson = ReadGlobalMugCollectionFromJson(jsonFilePath);
                
        // Insert data from JSON file
        db.GlobalMugCollections.AddRange(mugsFromJson);
        db.SaveChangesAsync();
        
        //-------------------------------------------------------------------//
        
        var user1 = new ApplicationUser
            { 
                UserName = "user1@uia.no", 
                Email = "user1@uia.no", 
                Nickname = "user1", 
                EmailConfirmed = true 
            };
        um.CreateAsync(user1, "Password1.").Wait();
        
        var user2 = new ApplicationUser
        {
            UserName = "user2@uia.no", 
            Email = "user2@uia.no", 
            Nickname = "user2", 
            EmailConfirmed = true
        };
        um.CreateAsync(user2, "Password1.").Wait();
        
        var user3 = new ApplicationUser
        {
            UserName = "user3@uia.no", 
            Email = "user3@uia.no", 
            Nickname = "user3", 
            EmailConfirmed = true
        };
        um.CreateAsync(user3, "Password1.").Wait();
        
        //-------------------------------------------------------------------//

        /*var mug1 = new GlobalMugCollection
        {
            MugImage = "mug1.jpg",
            MugName = "Love",
            StartYear = 1996,
            EndYear = null
        };
        var mug2 = new GlobalMugCollection
        {
            MugImage = "mug2.jpg",
            MugName = "Stinky",
            StartYear = 2001,
            EndYear = 2021
        };
       var mug3 = new GlobalMugCollection
        {
            MugImage = "mug3.jpg",
            MugName = "Hattifatteners",
            StartYear = 2007,
            EndYear = null
        };
        var mug4 = new GlobalMugCollection
        {
            MugImage = "mug4.jpg",
            MugName = "Moominmamma",
            StartYear = 2014,
            EndYear = 2020
        };
        var mug5 = new GlobalMugCollection
        {
            MugImage = "mug5.jpg",
            MugName = "Inspector",
            StartYear = 2009,
            EndYear = 2020
        };
        
        db.GlobalMugCollections.AddRange(mug1, mug2, mug3, mug4, mug5);
        db.SaveChanges();
        
        //-------------------------------------------------------------------//
        
        var Collection1 = new MyCollection
        (
            description: "This is my favorite Love mug",
            mugDetails: "Limited edition with unique color",
            hasLabel: true,
            isUsed: false
        )
        {
            ApplicationUserId = user1.Id,
            GlobalMugCollectionId = mug1.Id
        };
        
        var Collection2 = new MyCollection
        (
            description: "Rare Stinky mug from 2001",
            mugDetails: "Good condition, but used",
            hasLabel: false,
            isUsed: true
        )
        {
            ApplicationUserId = user2.Id,
            GlobalMugCollectionId = mug2.Id
        };

        var Collection3 = new MyCollection
        (
            description: "Collector's item - Hattifatteners mug",
            mugDetails: "Mint condition",
            hasLabel: true,
            isUsed: false
        )
        {
            ApplicationUserId = user1.Id,
            GlobalMugCollectionId = mug3.Id
        };
        
        var Collection4 = new MyCollection
        (
            description: "Collector's item - Hattifatteners mug",
            mugDetails: "Mint condition",
            hasLabel: true,
            isUsed: false
        )
        {
            ApplicationUserId = user1.Id,
            GlobalMugCollectionId = mug5.Id
        };
        
        db.MyCollections.AddRange(Collection1, Collection2, Collection3, Collection4);
        db.SaveChanges();*/
      
    }
    
    // Helper method to read JSON file and deserialize to GlobalMugCollection objects
    private static List<GlobalMugCollection> ReadGlobalMugCollectionFromJson(string jsonFilePath)
    {
        try
        {
            var jsonData = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<GlobalMugCollection>>(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            return new List<GlobalMugCollection>();
        }
    }
}