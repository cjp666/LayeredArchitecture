using CJSoftware.Domain.Model;

namespace CJSoftware.Application.Translators
{
    public interface ITranslator
    {
    }

    /// <summary>
    ///     Translate <see cref="DomainObject" /> to DTO
    /// </summary>
    /// <typeparam name="F">DomainObject</typeparam>
    /// <typeparam name="T">DTO</typeparam>
    public interface  ITranslator<F, T> where F : IDomainObject
    {
        /// <summary>
        ///     Performs the translate
        /// </summary>
        /// <param name="domainObject">the <see cref="IDomainObject"/></param>
        /// <returns>the created DTO</returns>
        T Translate(F domainObject);
    }
}