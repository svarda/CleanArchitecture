namespace CleanArchitecture.Core.Entities.Base {
    public interface IEntityBase<T> {
        T Id { get; }
    }
}
