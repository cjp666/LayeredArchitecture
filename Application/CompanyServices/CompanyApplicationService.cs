using System;
using System.Collections.Generic;
using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain;
using CJSoftware.Domain.Repositories;
using CJSoftware.Application.Translators;

namespace CJSoftware.Application.CompanyServices
{
	public class CompanyApplicationService : ApplicationService, ICompanyApplicationService
	{
		private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyTranslator _companyTranslator;

		public CompanyApplicationService(IUnitOfWork unitOfWork, 
            ICompanyRepository companyRepository,
            ICompanyTranslator companyTranslator)
			: base(unitOfWork)
		{
			_companyRepository = companyRepository;
            _companyTranslator = companyTranslator;
        }

		public IEnumerable<CompanyDTO> GetAll()
		{
			var companies = _companyRepository.GetAll();

			var results = new List<CompanyDTO>();

			foreach (var company in companies)
			{
                var dto = _companyTranslator.Translate(company);
				results.Add(dto);
			}

			return results;
		}
	}
}