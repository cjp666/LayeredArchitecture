using CJSoftware.Application.Translators;
using CJSoftware.CrossCutting.Enumerations;
using CJSoftware.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationTests.Translators
{
    [TestClass]
    public class PersonAddressTranslatorTests
    {
        [TestMethod]
        public void Translate_PersonAddress_CorrectDTO()
        {
            var address = new PersonsAddress
            {
                Id = 5623,
                PersonId = 9515,
                Location = AddressLocation.Home,
                Line1 = "Address Line 1",
                Line2 = "Address Line 2",
                Town = "Knutsford",
                County = "Cheshire",
                Postcode = "WA16 8GS",
                Telephone = "01256 645865"
            };

            var translator = new PersonAddressTranslator();

            var result = translator.Translate(address);

            Assert.AreEqual(5623, address.Id);
            Assert.AreEqual(9515, address.PersonId);
            Assert.AreEqual(AddressLocation.Home, address.Location);
            Assert.AreEqual("Address Line 1", address.Line1);
            Assert.AreEqual("Address Line 2", address.Line2);
            Assert.AreEqual("Knutsford", address.Town);
            Assert.AreEqual("Cheshire", address.County);
            Assert.AreEqual("WA16 8GS", address.Postcode);
            Assert.AreEqual("01256 645865", address.Telephone);
        }
    }
}
