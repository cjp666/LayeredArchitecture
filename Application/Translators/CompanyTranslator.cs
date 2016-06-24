using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;

namespace CJSoftware.Application.Translators
{
    public class CompanyTranslator : ICompanyTranslator
    {
        public CompanyDTO Translate(Company company)
        {
            var dto = new CompanyDTO
            {
                Id = company.Id,
                Code = company.Code,
                Name = company.Name,
                EmailAddress = company.EmailAddress
            };

            return dto;
        }
    }
}
