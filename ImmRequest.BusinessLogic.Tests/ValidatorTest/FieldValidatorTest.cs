using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using ImmRequest.Domain.Exceptions;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class FieldValidatorTest
    {
        public TextField textRange;

        public DateTimeField dateTimeRange;

        public NumberField numberRange;

        [TestInitialize]
        public void Setup()
        {
            textRange = new TextField
            {
                Id = 1,
                Name = "TextRange",
                RangeValues = new List<string>
                {
                    "Credencial", "Cedula"
                },
                ParentTypeId = 1
            };
            dateTimeRange = new DateTimeField
            {
                Id = 1,
                Name = "DatetimeRange",
                Start = new DateTime(2020, 4, 1),
                End = new DateTime(2020, 4, 4),
                ParentTypeId = 1
            };
            numberRange = new NumberField
            {
                Id = 2,
                Name = "NumberRange",
                RangeStart = 1,
                RangeEnd = 4,
                ParentTypeId = 1
            };
        }

        [TestMethod]
        public void TextFieldIsValid()
        {
            var textToValidate = "Credencial";
            var isValid = textRange.Validate(textToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void TextFieldNotInRange()
        {
            var textToValidate = "Contribucion";
            textRange.Validate(textToValidate);
        }

        [TestMethod]
        public void DateTimeFieldIsValid()
        {
            var dateToValidate = JsonConvert.SerializeObject(new DateTime(2020, 4, 2));
            var isValid = dateTimeRange.Validate(dateToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void DateTimeFieldNotInRange()
        {
            var dateToValidate = JsonConvert.SerializeObject(new DateTime(2020, 4, 5));
            dateTimeRange.Validate(dateToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void DateTimeFieldIsInvalid()
        {
            var dateToValidate = "invalid";
            dateTimeRange.Validate(dateToValidate);
        }

        [TestMethod]
        public void DateTimeFieldFromDifferentTimeZoneIsValid()
        {
            var dateToValidate = JsonConvert.SerializeObject(DateTime.SpecifyKind(new DateTime(2020, 4, 4, 2, 0, 0), DateTimeKind.Utc));
            var isValid = dateTimeRange.Validate(dateToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NumberFieldIsValid()
        {
            var numberToValidate = "3";
            var isValid = numberRange.Validate(numberToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void NumberFieldNotInRange()
        {
            var numberToValidate = "13";
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void NumberFieldIsTooLarge()
        {
            var numberToValidate = "5000000000000";
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void NumberFieldIsInvalid()
        {
            var numberToValidate = "invalid";
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        public void SetRangeNumberFieldTest()
        {
            var values = new List<string> { "1", "10" };
            numberRange.SetRange(values);

            Assert.AreEqual(1, numberRange.RangeStart);
            Assert.AreEqual(10, numberRange.RangeEnd);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void SetRangeEmptyValuesNumberFieldTest()
        {
            var values = new List<string>();
            numberRange.SetRange(values);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void SetNumberRangeWtihNonNumvericalValues()
        {
            var values = new List<string> { "A", "B" };
            numberRange.SetRange(values);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void SetRangeEmptyValuesTextFieldTest()
        {
            var values = new List<string>();
            textRange.SetRange(values);
        }

        [TestMethod]
        public void SetRangeTestFieldTest()
        {
            var values = new List<string> { "telefono", "escolaridad" };
            textRange.SetRange(values);
            var containsFirstValue = textRange.RangeValues.Contains("telefono");
            var containsSecondValue = textRange.RangeValues.Contains("escolaridad");

            Assert.IsTrue(containsFirstValue);
            Assert.IsTrue(containsSecondValue);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void SetRangeEmptyValuesDatesFieldTest()
        {
            var values = new List<string>();
            dateTimeRange.SetRange(values);
        }
    }
}
