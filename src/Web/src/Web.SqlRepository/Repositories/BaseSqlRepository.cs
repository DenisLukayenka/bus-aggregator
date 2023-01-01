using Web.Infrastructure.Configuration;
using Web.Infrastructure.Repository;

namespace Web.SqlRepository.Repositories;

public abstract class BaseSqlRepository<T> : BaseRepository<T>
{
    protected BaseSqlRepository(AppConfiguration config) : base(config)
    {
    }
}