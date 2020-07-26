namespace CleanArchitecture.Core.Entities.Base {
    public abstract class EntityBase<T> : IEntityBase<T> {
        public virtual T Id { get; protected set; }
    }
}
