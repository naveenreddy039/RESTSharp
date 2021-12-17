using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharpTestcase
{
    public class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string salary { get; set; }
    }

    [TestClass]
    public class RestSharpTest1
    {
        RestClient clint;

        [TestInitialize]
        public void Setup()
        {
            clint = new RestClient("http://localhost:4000");
        }

        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);
            IRestResponse response = clint.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnCallingListGETApi_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            Assert.AreEqual(2, dataResponse.Count);

            foreach (Employee e in dataResponse)
            {
                System.Console.WriteLine("Id:" + e.id + "Name:" + e.Name + "Salary:" + e.salary);
            }
        }
        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("Name", "clerk");
            jObjectbody.Add("Salary", "35000");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            IRestResponse response = clint.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("clerk", dataResponse.Name);
            Assert.AreEqual("35000", dataResponse.salary);

            System.Console.WriteLine(response.Content);

        }
        [TestMethod]
        public void givenEmployee_OnPut_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees/6", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("Name", "clerk");
            jObjectbody.Add("Salary", "40000");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            IRestResponse response = clint.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("clerk", dataResponse.Name);
            Assert.AreEqual("40000", dataResponse.salary);

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
