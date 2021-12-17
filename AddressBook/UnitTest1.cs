using DocuSign.eSign.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace AddressBook
{
    public class AdddressBook1
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String lastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public double PhoneNumber { get; set; }
        public String Email { get; set; }
    }
    
    [TestClass]
    public class UnitTest1
    {
        RestClient clint;

        public void Setup()
        {
            clint = new RestClient("http://localhost:3000");
        }
        private IRestResponse getAddressBookList()
        {
            RestRequest request = new RestRequest("/addressbook", Method.GET);
            IRestResponse response = clint.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnCallingListGETApi_ReturnAddressBookList()
        {
            IRestResponse response = getAddressBookList();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            List<AdddressBook1> dataResponse = JsonConvert.DeserializeObject<List<AdddressBook1>>(response.Content);

            Assert.AreEqual(2, dataResponse.Count);

            foreach (AdddressBook1 e in dataResponse)
            {
                System.Console.WriteLine("Id:" + e.Id + "firstName:" + e.FirstName +"lastName:"+ e.lastName + "address:" + e.Address + "City:" + e.City + "State:" + e.State + "Phonenumber:" + e.PhoneNumber + "Email:" + e.Email );
            }
        }
        public void givenEmployee_OnPost_ShouldReturnAddedAddressBook()
        {
            RestRequest request = new RestRequest("/addressbook", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("firstName", "clerk");
            jObjectbody.Add("PhoneNumber", "8526935000");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            IRestResponse response = clint.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            AdddressBook1 dataResponse = JsonConvert.DeserializeObject<AdddressBook1>(response.Content);
            Assert.AreEqual("clerk", dataResponse.FirstName);
            Assert.AreEqual("35000", dataResponse.PhoneNumber);

            System.Console.WriteLine(response.Content);

        }
        [TestMethod]
        public void givenEmployee_OnPut_ShouldReturnAddedAddressBook()
        {
            RestRequest request = new RestRequest("/addressbook/6", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("FirstName", "clerk");
            jObjectbody.Add("PhoneNumber", "8695240000");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            IRestResponse response = clint.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            AdddressBook1 dataResponse = JsonConvert.DeserializeObject<AdddressBook1>(response.Content);
            Assert.AreEqual("clerk", dataResponse.FirstName);
            Assert.AreEqual("40000", dataResponse.PhoneNumber);

            System.Console.WriteLine(response.Content);

        }
        [TestMethod]
        public void givenEmployee_OnDlete_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees/6", Method.DELETE);

            IRestResponse response = clint.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            System.Console.WriteLine(response.Content);

        }
    }
}
