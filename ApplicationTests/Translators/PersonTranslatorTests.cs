using CJSoftware.Application.Translators;
using CJSoftware.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationTests.Translators
{
    [TestClass]
    public class PersonTranslatorTests
    {
        [TestMethod]
        public void Translator_ActivePerson_CorrectDTO()
        {
            var person = new People
            {
                Id = 1233,
                EmployeeReference = "ABC987",
                Name = "A Test Person",
                IsActive = true
            };

            var translator = new PersonTranslator();

            var result = translator.Translate(person);

            Assert.AreEqual(1233, result.Id);
            Assert.AreEqual("ABC987", result.EmployeeReference);
            Assert.AreEqual("A Test Person", result.Name);
            Assert.IsTrue(result.IsActive);
            Assert.IsNotNull(result.Addresses);
            Assert.AreEqual(0, result.Addresses.Count);
        }

        [TestMethod]
        public void Translator_InactivePerson_CorrectDTO()
        {
            var person = new People
            {
                Id = 9876,
                EmployeeReference = "XYZ655",
                Name = "Mr John Smith",
                IsActive = false
            };

            var translator = new PersonTranslator();

            var result = translator.Translate(person);

            Assert.AreEqual(9876, result.Id);
            Assert.AreEqual("XYZ655", result.EmployeeReference);
            Assert.AreEqual("Mr John Smith", result.Name);
            Assert.IsFalse(result.IsActive);
        }
    }
}