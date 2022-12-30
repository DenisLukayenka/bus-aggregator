namespace Web.Infrastructure.Models.Repository;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}