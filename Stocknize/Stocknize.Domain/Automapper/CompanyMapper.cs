using AutoMapper;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Models.Company;

namespace Stocknize.Domain.Automapper
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<CompanyInputModel, Company>();
            CreateMap<Company, CompanyOutputModel>();
        }
    }
}
