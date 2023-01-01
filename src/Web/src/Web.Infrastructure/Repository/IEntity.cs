namespace Web.Infrastructure.Repository;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}