using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>
    /// Именованая сущность
    /// </summary>
    public abstract class NameEntity : BaseEntity, INameEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
