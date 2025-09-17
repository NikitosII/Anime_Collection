
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities
{
    public enum Status
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

        [Column(TypeName = "decimal(3,1)")]
        public double Rating { get; set; }
        public Status Status { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }
}
