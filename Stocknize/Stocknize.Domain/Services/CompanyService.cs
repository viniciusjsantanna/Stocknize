using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Company;
using System.Threading;
using System.Threading.Tasks;

namespace Stocknize.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            this.mapper = mapper;
        }

        public async Task<CompanyOutputModel> AddCompany(CompanyInputModel model, CancellationToken cancellationToken)
        {
            var existCompany = await companyRepository.Any(e => e.Name.Equals(model.Name), cancellationToken);

            if (existCompany)
            {
                throw new System.Exception("");
            }

            var company = mapper.Map<Company>(model);

            return mapper.Map<CompanyOutputModel>(await companyRepository.Add(company, cancellationToken));
        }
    }
}
