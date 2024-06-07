using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class RealLifeTaskFilterViewModel
    {
        public IEnumerable<RealLifeTask> Tasks { get; set; }
        public int? CategoryId { get; set; }
        public Priority? Priority { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
