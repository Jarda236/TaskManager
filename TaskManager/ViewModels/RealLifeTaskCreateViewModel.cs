using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class RealLifeTaskCreateViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
    }
}
