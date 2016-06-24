using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;

namespace CJSoftware.Application.Translators
{
    public interface ICompanyTranslator : ITranslator, ITranslator<Company, CompanyDTO>
    {
    }
}