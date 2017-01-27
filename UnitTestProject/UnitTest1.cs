using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private const string BaseUrl = "http://localhost:63816";

        private readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };

        [TestMethod]
        public void Metadata()
        {
            var response = _client.GetAsync("odata").Result;
            var res = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine($"response: {res}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(res));
        }

        [TestMethod]
        public void GetPeople()
        {
            var response = _client.GetAsync("odata/People").Result;
            var res = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine($"response: {res}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(res));
        }

        [TestMethod]
        public void GetTrips()
        {
            var response = _client.GetAsync("odata/Trips").Result;
            var res = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine($"response: {res}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(res));
        }

        [TestMethod]
        public void GetPerson()
        {
            var response = _client.GetAsync("odata/People('001')").Result;
            var res = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine($"response: {res}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(res));
        }

        [TestMethod]
        public void GetPersonTrips()
        {
            var response = _client.GetAsync("odata/People('001')/Trips").Result;
            var res = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine($"response: {res}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(res));
        }
    }
}
