using HeroesDatabase.Model;

namespace HeroesDatabase.Services.InterfaceService
{
    public interface ICompanyService
    {
        Task<List<Company>> GetCompany();
        Task<Company> GetCompany(Guid companyId);
        Task PutCompany(Guid id, CompanyPersist companyPersist);
        Task<Company> PostCompany(CompanyPersist CompanyPersist);
        Task DeleteCompany(Guid id);


    }
}
