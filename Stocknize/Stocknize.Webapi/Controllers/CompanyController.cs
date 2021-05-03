using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Domain.Models.Company;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Stocknize.Webapi.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            this.companyService = companyService;
            this.mapper = mapper;
        }

        [HttpPost]
        public Task<CompanyOutputModel> Post([FromBody] CompanyInputModel model, CancellationToken cancellationToken)
        {
            //var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = companyService.AddCompany(model, cancellationToken);
            //transaction.Complete();
            return result;
        }

        [HttpGet]
        public async Task<IList<CompanyOutputModel>> Get([FromServices] ICompanyRepository companyRepository, CancellationToken cancellationToken)
        {
            return mapper.Map<IList<CompanyOutputModel>>(await companyRepository.GetCompanies(cancellationToken));
        }
    }
}
