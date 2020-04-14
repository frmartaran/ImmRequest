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
        [ExpectedException(typeof(ValidationException))]
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
        [ExpectedException(typeof(ValidationException))]
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
        [ExpectedException(typeof(ValidationException))]
        public void NumberFieldNotInRange()
        {
            var numberToValidate = "13";
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void NumberFieldIsTooLarge()
        {
            var textToValidate = "5000000000000";
            textRange.Validate(textToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void NumberFieldIsInvalid()
        {
            var textToValidate = "invalid";
            textRange.Validate(textToValidate);
        }
    }
}
