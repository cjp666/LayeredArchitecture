using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;

namespace CJSoftware.Application.Translators
{
    public interface IPersonTranslator : ITranslator, ITranslator<Person, PersonDTO>
    {
    }
}