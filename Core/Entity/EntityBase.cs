using System.ComponentModel.DataAnnotations;

namespace Core.Entity
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
