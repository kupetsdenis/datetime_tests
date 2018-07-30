using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using datetime_tests.Helpers;
using datetime_tests.Models;
using datetime_tests.Settings.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace datetime_tests.Tests
{
    [TestClass]
    public class NBRB_API_Suite
    {
        private readonly CurrencyRequestHelper _currencyRequestHelper = new CurrencyRequestHelper();
        private const string NOT_FOUND_ERROR_MESSAGE = "The remote server returned an error: (404) Not Found.";
        private const string BAD_REQUEST_ERROR_MESSAGE = "The remote server returned an error: (400) Bad Request.";
        [DataTestMethod]
        [DataRow(
            @"{""Cur_ID"":2,""Cur_ParentID"":2,""Cur_Code"":""012"",""Cur_Abbreviation"":""DZD"",""Cur_Name"":""Алжирский динар"",""Cur_Name_Bel"":""Алжырскі дынар"",""Cur_Name_Eng"":""Algerian Dinar"",""Cur_QuotName"":""1 Алжирский динар"",""Cur_QuotName_Bel"":""1 Алжырскі дынар"",""Cur_QuotName_Eng"":""1 Algerian Dinar"",""Cur_NameMulti"":"""",""Cur_Name_BelMulti"":"""",""Cur_Name_EngMulti"":"""",""Cur_Scale"":1,""Cur_Periodicity"":1,""Cur_DateStart"":""1991-01-01T00:00:00"",""Cur_DateEnd"":""2016-06-30T00:00:00""}")]
        [DataRow(
            @"{""Cur_ID"":1, ""Cur_ParentID"":1, ""Cur_Code"":""008"", ""Cur_Abbreviation"":""ALL"", ""Cur_Name"":""Албанский лек"", ""Cur_Name_Bel"":""Албанскі лек"", ""Cur_Name_Eng"":""Albanian Lek"", ""Cur_QuotName"":""1 Албанский лек"", ""Cur_QuotName_Bel"":""1 Албанскі лек"", ""Cur_QuotName_Eng"":""1 Albanian Lek"", ""Cur_NameMulti"":"""", ""Cur_Name_BelMulti"":"""", ""Cur_Name_EngMulti"":"""", ""Cur_Scale"":1, ""Cur_Periodicity"":1, ""Cur_DateStart"":""1991-01-01T00:00:00"", ""Cur_DateEnd"":""2007-11-30T00:00:00""}")]

        public void HappyPathTests(string inputValue)
        {
            var expected = JsonConvert.DeserializeObject<CurrencyModel>(inputValue);
            var actual = _currencyRequestHelper.GetCurrecy(expected.Cur_ID.ToString());
            Assert.AreEqual(expected.Serialize(), actual.Serialize(), "Invalid Currency");
        }

        [TestMethod]
        [DataRow("0", NOT_FOUND_ERROR_MESSAGE)]
        [DataRow("-1", NOT_FOUND_ERROR_MESSAGE)]
        [DataRow("1111111", NOT_FOUND_ERROR_MESSAGE)]
        [DataRow("abc#$@#&*?", BAD_REQUEST_ERROR_MESSAGE)]
        [DataRow("1111111111111111111111111111111111111111111111111", BAD_REQUEST_ERROR_MESSAGE)]
        public void InvalidCurrencyId(string currencyId, string exceptionMessage)
        {
            try
            {
                _currencyRequestHelper.GetCurrencyResponse(currencyId);
                throw new Exception("The response should throw an Exception while getting invalid currencyId");
            }
            catch (WebException exception)
            {
                Assert.AreEqual(exceptionMessage, exception.Message, "Invalid exception message");
                Assert.AreEqual(WebExceptionStatus.ProtocolError, exception.Status, "Invalid status");
            }
        }

        [TestMethod]
        public void AllCurrencies()
        {
            var currencies = _currencyRequestHelper.GetCurrecies();
            foreach (var currencyModel in currencies)
            {
                var currencyId = currencyModel.Cur_ID;
                Assert.IsTrue(currencyId > 0,
                    $"Invalid {nameof(currencyModel.Cur_ID)}, it should be > 0, but it is '{currencyId}' '{currencyModel.Serialize()}'");
                //All other checks should be here
            }
        }

        [TestMethod]
        public void AllCurrenciesFromFile()
        {
            var path = Resources.AllCurrenciesJSONFilePath;
            if (!File.Exists(path))
                throw new FileNotFoundException(
                    $"Cannot locate the JSON Data file:\n{path}\nPlease check the path and try again.");
            var fileContent = File.ReadAllText(path);
            var currencies = JsonConvert.DeserializeObject<List<CurrencyModel>>(fileContent);
            foreach (var currencyModel in currencies)
            {
                var currencyId = currencyModel.Cur_ID;
                Assert.IsTrue(currencyId > 0,
                    $"Invalid {nameof(currencyModel.Cur_ID)}, it should be > 0, but it is '{currencyId}' '{currencyModel.Serialize()}'");
                //All other checks should be here
            }
        }
    }
}