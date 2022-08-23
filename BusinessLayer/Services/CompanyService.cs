using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        Company GetSetData(CompanyInfo companyinfo)
        {
            Company company = new Company();
            company.SiteId = companyinfo.SiteId;
            company.CompanyCode = companyinfo.CompanyCode;
            company.CompanyName = companyinfo.CompanyName;
            company.AddressLine1 = companyinfo.AddressLine1;
            company.AddressLine2 = companyinfo.AddressLine2;
            company.AddressLine3 = companyinfo.AddressLine3;
            company.PostalZipCode = companyinfo.PostalZipCode;
            company.PhoneNumber = companyinfo.PhoneNumber;
            company.FaxNumber = companyinfo.FaxNumber;
            company.EquipmentCompanyCode = companyinfo.EquipmentCompanyCode;
            company.Country = companyinfo.Country;
            company.LastModified = companyinfo.LastModified;

            List<ArSubledger> lstdalSubledger = new List<ArSubledger>();
            foreach (var item in companyinfo.ArSubledgers)
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

                lstdalSubledger.Add(subledger);
            }

            company.ArSubledgers = lstdalSubledger;
            return company;
        }

        Task<bool> ICompanyService.DeleteCompanyById(int SiteId)
        {
            return _companyRepository.DeleteById(SiteId);
        }

        Task<bool> ICompanyService.AddCompanies(CompanyInfo companyinfo)
        {
            var company = GetSetData(companyinfo);
            return _companyRepository.SaveCompanies(company);
        }

        Task<bool> ICompanyService.UpdateCompanies(string siteId, CompanyInfo companyinfo)
        {
            var company = GetSetData(companyinfo);
            return _companyRepository.UpdateCompanies(siteId, company);
        }
    }
}
