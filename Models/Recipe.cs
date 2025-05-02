using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeManagementSystem.Models
{
    public class Recipe
    {
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Recipe name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Ingredients are required")]
    public string Ingredients { get; set; }
    
    [Required(ErrorMessage = "Instructions are required")]
    public string Instructions { get; set; }
    
    [Required(ErrorMessage = "Preparation time is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Preparation time must be positive")]
    public int PreparationTime { get; set; } // in minutes
    
    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; }
    }
}