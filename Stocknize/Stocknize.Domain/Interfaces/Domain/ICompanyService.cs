using Stocknize.Domain.Models.Company;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Domain
{
    public interface ICompanyService
    {
        Task<CompanyOutputModel> AddCompany(CompanyInputModel model, CancellationToken cancellationToken);
    }
}
