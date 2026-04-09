using System;
using System.ComponentModel.DataAnnotations;
using moomug_project.Models;

namespace moomug_project.Models
{
    public class GlobalMugCollection
    {
        [Key] public int Id { get; set; } = 0;     
        
        [Required]
        [MaxLength(255)]
        public string? MugImage { get; set; }
                
        [Required]
        [MaxLength(100)]
        public string? MugName { get; set; }
        
        [Required]
        public int StartYear { get; set; }
         
        public int? EndYear { get; set; }               // Nullable
        
    }
}