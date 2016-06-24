using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;

namespace CJSoftware.Application.Translators
{
    public interface IPersonAddressTranslator : ITranslator, ITranslator<PersonsAddress, PersonAddressDTO>
    {
    }
}
