using RestSharp;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AqaPortfolioProject.TestsApi
{
    public class UserApiTests
    {
        private readonly RestClient _client;

        public UserApiTests()
        {
            var options = new RestClientOptions("https://reqres.in");
            _client = new RestClient(options);
        }

        [Fact]
        [Trait("Category", "API-Positive")]
        public async Task Get_UsersList_Should_Return_200OK()
        {
            var request = new RestRequest("/api/users?page=2", Method.Get);
            RestResponse response = await _client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            Assert.Contains("Michael", response.Content);
        }

        [Fact]
        [Trait("category", "API_Negative")]
        public async Task Get_UsersList_Should_Return_400ER()
        {
            var request = new RestRequest("/NotFound111", Method.Get);
            RestResponse response = await _client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
       
    }
}