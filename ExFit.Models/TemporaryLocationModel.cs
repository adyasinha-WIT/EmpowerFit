using System.ComponentModel.DataAnnotations;

namespace Exfit.Models;

public class TemporaryLocationModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    [Required]
    public string UserId { get; set; }
}
