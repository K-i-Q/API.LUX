using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IHealthCheckRepository
    {
        Task Ready();
    }
}
