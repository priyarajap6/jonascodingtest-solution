using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company GetByCode(string companyCode);
        bool SaveCompany(Company company);
        Task<bool> DeleteById(int SiteId);
        Task<bool> SaveCompanies(Company company);
        Task<bool> UpdateCompanies(string siteId, Company company);
        Task<IEnumerable<Company>> GetAllCompanies();
    }
}
