using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using ImmRequest.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Xml.Schema;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class FieldValidatorTest
    {
        public TextField textRange;

        public DateTimeField dateTimeRange;

        public NumberField numberRange;

        public BoolField boolRange;

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
            boolRange = new BoolField
            {
                Id = 4,
                Name = "Bool Range",
                ParentTypeId = 1,
            };
        }

        #region Validate() Tests
        [TestMethod]
        public void TextFieldIsValidTest()
        {
            var textToValidate = new List<string> { "Credencial" };
            var isValid = textRange.Validate(textToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void TextFieldNotInRangeTest()
        {
            var textToValidate = new List<string> { "Contribucion" };
            textRange.Validate(textToValidate);
        }

        [TestMethod]
        public void DateTimeFieldIsValidTest()
        {
            var dateToValidate = new List<string> { "2020-04-03T18:25:43.511-03:00" };
            var isValid = dateTimeRange.Validate(dateToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void DateTimeFieldNotInRangeAfterEndTest()
        {
            var dateToValidate = new List<string> { "2020-04-05T18:25:43.511-03:00" };
            dateTimeRange.Validate(dateToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void DateTimeFieldNotInRangeBeforeStartTest()
        {
            var dateToValidate = new List<string> { "2020-03-31T18:25:43.511-03:00" };
            dateTimeRange.Validate(dateToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void DateTimeFieldIsInvalidTest()
        {
            var dateToValidate = new List<string> { "invalid" };
            dateTimeRange.Validate(dateToValidate);
        }

        [TestMethod]
        public void DateTimeFieldFromDifferentTimeZoneIsValidTest()
        {
            var dateToValidate = new List<string> { "2020-04-03T18:25:43.511Z" };
            var isValid = dateTimeRange.Validate(dateToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void NumberFieldIsValidTest()
        {
            var numberToValidate = new List<string> { "3" };
            var isValid = numberRange.Validate(numberToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void NumberFieldGreaterThanEndTest()
        {
            var numberToValidate = new List<string> { "13" };
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void NumberFieldLowerThanStart()
        {
            var numberToValidate = new List<string> { "-2" };
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void NumberFieldIsTooLargeTest()
        {
            var numberToValidate = new List<string> { "5000000000000" };
            numberRange.Validate(numberToValidate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void NumberFieldIsInvalidTest()
        {
            var numberToValidate = new List<string> { "invalid" };
            numberRange.Validate(numberToValidate);
        }

        [DataTestMethod]
        [DataRow("true")]
        [DataRow("True")]
        [DataRow("false")]
        [DataRow("False")]
        public void BoolValuesAreValid(string value)
        {
            var values = new List<string> { value };
            var isValid = boolRange.Validate(values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]
        public void NonBooleanValuesAreNotValid()
        {
            var values = new List<string> { "non boolean" };
            boolRange.Validate(values);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void BoolFieldDoesNotAcceptMultipleSelection()
        {
            var values = new List<string> { "true", "false" };
            boolRange.Validate(values);
        }

        [TestMethod]
        public void NumberFieldAcceptsMultipleSelection()
        {
            numberRange.IsMultipleSelectEnabled = true;
            var values = new List<string> { "2", "3" };
            var isValid = numberRange.Validate(values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]

        public void NumberFieldSecondValuesIsInvalid()
        {
            numberRange.IsMultipleSelectEnabled = true;
            var values = new List<string> { "2", "30" };
            numberRange.Validate(values);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]

        public void NumberFieldDoesNotAcceptsMultipleSelection()
        {
            numberRange.IsMultipleSelectEnabled = false;
            var values = new List<string> { "2", "3" };
            numberRange.Validate(values);
        }

        [TestMethod]
        public void TextFieldAcceptsMultipleSelection()
        {
            textRange.IsMultipleSelectEnabled = true;
            var values = new List<string> { "Credencial", "Cedula" };
            var isValid = textRange.Validate(values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void TextFieldDoesNotAcceptsMultipleSelection()
        {
            textRange.IsMultipleSelectEnabled = false;
            var values = new List<string> { "Credencial", "Cedula" };
            var isValid = textRange.Validate(values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void TextFieldSecondValueIsInvalid()
        {
            textRange.IsMultipleSelectEnabled = true;
            var values = new List<string> { "Credencial", "Calle" };
            var isValid = textRange.Validate(values);
        }

        [TestMethod]
        public void DateTimeFieldAcceptsMultipleSelection()
        {
            dateTimeRange.IsMultipleSelectEnabled = true;
            var values = new List<string> { "2020-04-02T18:25:43.511Z", "2020-04-03T18:25:43.511Z" };
            var isValid = dateTimeRange.Validate(values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void DateTimeFieldDoesNotAcceptsMultipleSelection()
        {
            dateTimeRange.IsMultipleSelectEnabled = false;
            var values = new List<string> { "2020-04-02T18:25:43.511Z", "2020-04-03T18:25:43.511Z" };
            var isValid = dateTimeRange.Validate(values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void DateTimeFieldSecondValueIsInvalid()
        {
            dateTimeRange.IsMultipleSelectEnabled = false;
            var values = new List<string> { "2020-04-02T18:25:43.511Z", "2020-04-20T18:25:43.511Z" };
            dateTimeRange.Validate(values);
        }

        #endregion

        #region SetRange() Tests
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

        [TestMethod]
        public void SetRangeValuesDateTimeFieldTest()
        {
            var start = new DateTime(2014, 1, 16);
            var startToString = start.ToString();
            var end = new DateTime(2020, 4, 20);
            var endToString = end.ToString();
            var values = new List<string> { startToString, endToString };
            dateTimeRange.SetRange(values);

            Assert.AreEqual(start, dateTimeRange.Start);
            Assert.AreEqual(end, dateTimeRange.End);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]

        public void SetRangeValuesNoDatetimeTest()
        {
            var startToString = "Last summer";
            var endToString = "Next Summer";
            var values = new List<string> { startToString, endToString };
            dateTimeRange.SetRange(values);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]

        public void MoreThanTwoValuesTestDatesFieldTest()
        {
            var start = new DateTime(2014, 1, 16);
            var startToString = start.ToString();
            var end = new DateTime(2020, 4, 20);
            var endToString = end.ToString();
            var anotherDate = new DateTime().ToString();
            var values = new List<string> { startToString, endToString, anotherDate };
            dateTimeRange.SetRange(values);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgumentException))]

        public void TooManyValuesNumberFieldTest()
        {
            var values = new List<string> { "1", "10", "15" };
            numberRange.SetRange(values);

        }

        [TestMethod]
        public void FieldRangesAreValidTest()
        {
            var numberFieldIsValid = numberRange.ValidateRangeValues();
            var textFieldIsValid = textRange.ValidateRangeValues();
            var datesFieldsValid = dateTimeRange.ValidateRangeValues();

            Assert.IsTrue(numberFieldIsValid);
            Assert.IsTrue(textFieldIsValid);
            Assert.IsTrue(datesFieldsValid);

        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void NumberFieldStartGreaterThanEndTest()
        {
            numberRange.RangeStart = 7;
            numberRange.RangeEnd = 1;
            numberRange.ValidateRangeValues();
        }

        [TestMethod]
        [ExpectedException(typeof(DomainValidationException))]
        public void DatesFieldStartGreaterThanEndTest()
        {
            dateTimeRange.End = new DateTime(2020, 9, 17);
            dateTimeRange.Start = new DateTime(2020, 11, 17);
            dateTimeRange.ValidateRangeValues();
        }
        #endregion


    }
}
