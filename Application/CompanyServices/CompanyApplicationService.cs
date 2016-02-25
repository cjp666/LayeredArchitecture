using System;
using System.Collections.Generic;
using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain;
using CJSoftware.Domain.Repositories;

namespace CJSoftware.Application.CompanyServices
{
	public class CompanyApplicationService : ApplicationService, ICompanyApplicationService
	{
		private readonly ICompanyRepository _companyRepository;

		public CompanyApplicationService(IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
			: base(unitOfWork)
		{
			_companyRepository = companyRepository;
		}

		public IEnumerable<CompanyDTO> GetAll()
		{
			var companies = _companyRepository.GetAll();

			// TODO some form of translator DO to DTO
			var results = new List<CompanyDTO>();

			foreach (var company in companies)
			{
				results.Add(new CompanyDTO
				{
					Id = company.Id,
					Code = company.Code,
					Name = company.Name,
					EmailAddress = company.EmailAddress
				});
			}

			return results;
		}
	}
}