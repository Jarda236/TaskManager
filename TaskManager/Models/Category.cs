using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Category
    {
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<RealLifeTask> RealLifeTasks { get; set; } = new List<RealLifeTask>();
     

        public override string ToString()
        {
            return Name;
        }
    }
}
