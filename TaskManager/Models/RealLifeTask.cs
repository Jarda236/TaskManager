namespace TaskManager.Models
{
    public class RealLifeTask
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
