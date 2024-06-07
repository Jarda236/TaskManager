using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<RealLifeTask> RealLifeTasks { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
