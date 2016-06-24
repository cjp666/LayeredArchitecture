using CJSoftware.Application.Translators;
using CJSoftware.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationTests.Translators
{
    [TestClass]
    public class CompanyTranslatorTests
    {
        [TestMethod]
        public void Translate_Company_CorrectDTO()
        {
            var company = new Company
            {
                Id = 48765,
                Code = "C1",
                Name = "A Test Company",
                EmailAddress = "test@company.com"
            };

            var translator = new CompanyTranslator();

            var result = translator.Translate(company);

            Assert.AreEqual(48765, result.Id);
            Assert.AreEqual("C1", result.Code);
            Assert.AreEqual("A Test Company", result.Name);
            Assert.AreEqual("test@company.com", result.EmailAddress);
        }
    }
}
