

namespace Core.Entity
{
    public abstract class CommonEntity : EntityBase, IDeletedEntity, ICreatedDateEntity, IUpdatedDateEntity
    {
        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
