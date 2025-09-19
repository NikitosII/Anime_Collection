namespace DataAccess.Entities
{
    public enum Statuses
    {
            Planned,
            Watching,
            Completed,
            Dropped
        }
    public class Entity
    {
        
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public Statuses Status { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }
}
