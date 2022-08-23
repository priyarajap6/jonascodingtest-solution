using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
        {
            _companyDbWrapper = companyDbWrapper;
        }

        public IEnumerable<Company> GetAll()
        {
            return _companyDbWrapper.FindAll();
        }

        public Company GetByCode(string companyCode)
        {
            return _companyDbWrapper.Find(t => t.CompanyCode.Equals(companyCode))?.FirstOrDefault();
        }

        public bool SaveCompany(Company company)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return _companyDbWrapper.Update(itemRepo);
            }

            return _companyDbWrapper.Insert(company);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await _companyDbWrapper.DeleteAsync(t => t.CompanyCode.Equals(id));
        }

        public async Task<bool> SaveCompanies(Company company)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }

            return await _companyDbWrapper.InsertAsync(company);
        }

        public async Task<bool> UpdateCompanies(string siteId, Company company)
        {
            bool isUpdate = false;
            var itemRepo = _companyDbWrapper.Find(t => t.SiteId.Equals(siteId))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.SiteId = company.SiteId;
                itemRepo.CompanyCode = company.CompanyCode;
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.Country = company.Country;
                itemRepo.LastModified = company.LastModified;

                List<ArSubledger> lstSubledger = new List<ArSubledger>();
                foreach (var item in company.ArSubledgers)
                {
                    ArSubledger subledger = new ArSubledger();
                    subledger.Description = item.Description;
                    subledger.CustomerName = item.CustomerName;
                    subledger.AddressLine1 = item.AddressLine1;
                    subledger.AddressLine2 = item.AddressLine2;
                    subledger.AddressLine3 = item.AddressLine3;
                    subledger.PostalZipCode = item.PostalZipCode;
                    subledger.PhoneNumber = item.PhoneNumber;
                    subledger.FaxNumber = item.FaxNumber;
                    subledger.LastModified = item.LastModified;
                    subledger.Active = item.Active;
                    subledger.Inactive = item.Inactive;
                    subledger.Active = item.Active;
                    subledger.Excellent = item.Excellent;
                    subledger.Good = item.Good;
                    subledger.Fair = item.Fair;
                    subledger.Poor = item.Poor;
                    subledger.Condemned = item.Condemned;

                    lstSubledger.Add(subledger);
                }
                company.ArSubledgers = lstSubledger;

                isUpdate = await _companyDbWrapper.UpdateAsync(itemRepo);
            }
            return isUpdate;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _companyDbWrapper.FindAllAsync();
        }
    }
}
