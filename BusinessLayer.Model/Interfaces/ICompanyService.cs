using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        Task<bool> DeleteCompanyById(int SiteId);
        Task<bool> AddCompanies(CompanyInfo companyinfo);
        Task<bool> UpdateCompanies(string siteId, CompanyInfo companyinfo);
    }
}
