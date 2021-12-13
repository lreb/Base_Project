using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProject.API.Contract.DbContext
{
    public interface IApplicationWriteDbConnection : IApplicationReadDbConnection
    {
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}
