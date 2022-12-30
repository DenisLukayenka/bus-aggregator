using Web.Infrastructure.Configuration;

namespace Web.Infrastructure.Models.Repository;

public interface IRepository<T>
{
    void Init();
}

public abstract class BaseRepository<T> : IRepository<T>
{
    protected readonly AppConfiguration AppConfiguration;

    public BaseRepository(AppConfiguration config)
    {
        this.AppConfiguration = config;
    }

    public abstract void Init();
}