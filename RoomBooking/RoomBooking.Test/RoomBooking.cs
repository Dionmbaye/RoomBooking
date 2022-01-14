using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooking.Api;
using System.Net;
using System.Threading.Tasks;

namespace RoomBooking.Test
{
    [TestClass]
    public class RoomBooking
    {
        private static WebApplicationFactory<Startup> _factory;
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task Should_User_Exist_When_Call_GetAsync()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("https://localhost:7028/Users");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var json = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(json);
        }
    }
}