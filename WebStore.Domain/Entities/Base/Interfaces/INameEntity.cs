namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Именованая сущность
    /// </summary>
    public interface INameEntity : IBaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }
    }
}
