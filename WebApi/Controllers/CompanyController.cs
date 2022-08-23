using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public IEnumerable<CompanyDto> GetAll()
        {
            var items = _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public CompanyDto Get(string companyCode)
        {
            var item = _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async void Post([FromBody]CompanyDto value)
        {
            var companyInfo = GetSetData(value);
            await _companyService.AddCompanies(companyInfo);
        }

        // PUT api/<controller>/5
        public async void Put(string SiteId, [FromBody]CompanyDto value)
        {
            var companyInfo = GetSetData(value);
            await _companyService.AddCompanies(companyInfo);
        }

        // DELETE api/<controller>/5
        public void Delete(int SiteId)
        {
            _companyService.DeleteCompanyById(SiteId);
        }

        CompanyInfo GetSetData(CompanyDto value)
        {
            CompanyInfo company = new CompanyInfo();
            company.SiteId = value.SiteId;
            company.CompanyCode = value.CompanyCode;
            company.CompanyName = value.CompanyName;
            company.AddressLine1 = value.AddressLine1;
            company.AddressLine2 = value.AddressLine2;
            company.AddressLine3 = value.AddressLine3;
            company.PostalZipCode = value.PostalZipCode;
            company.PhoneNumber = value.PhoneNumber;
            company.FaxNumber = value.FaxNumber;
            company.EquipmentCompanyCode = value.EquipmentCompanyCode;
            company.Country = value.Country;
            company.LastModified = value.LastModified;

            List<ArSubledgerInfo> lstbalSubledger = new List<ArSubledgerInfo>();
            foreach (var item in value.ArSubledgers)
            {
                ArSubledgerInfo subledger = new ArSubledgerInfo();
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

                lstbalSubledger.Add(subledger);
            }
            company.ArSubledgers = lstbalSubledger;

            return company;
        }
    }
}