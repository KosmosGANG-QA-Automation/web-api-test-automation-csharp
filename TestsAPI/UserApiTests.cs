using Allure.Net.Commons;
using Allure.Net.Commons.Attributes;
using AqaPortfolioProject.Models;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using Xunit;

// Регистрируем Allure как кастомный репортер xUnit
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace AqaPortfolioProject.TestsApi
{
    [AllureEpic("API Testing")]
    [AllureFeature("ReqRes User Management")]
    public class UserApiTests
    {
        private readonly RestClient _client;

        public UserApiTests()
        {
            var options = new RestClientOptions("https://reqres.in");
            _client = new RestClient(options);
        }

        [Fact]
        [AllureStory("GET /api/users/2 - Get Single User")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureIssue("3")]
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
        [AllureStory("GET /NotFound111 - Should Return 404 For Invalid Endpoint")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureIssue("4")]
        [Trait("Category", "API_Negative")]
        public async Task Get_UsersList_Should_Return_404ER()
        {
            var request = new RestRequest("/NotFound111", Method.Get);
            RestResponse response = await _client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        [AllureStory("POST /api/users - Create User")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("1")] // Связываем с Issue #1 на GitHub
        [Trait("Category", "API-Positive")]
        public async Task Post_CreateUser_Should_Return_201Created_And_ValidDto()
        {
            // 1. Arrange: создаем DTO объект запроса
            var newUser = new UserRequestDto
            {
                Name = "morpheus",
                Job = "leader"
            };

            var request = new RestRequest("/api/users", Method.Post);
            // RestSharp сам сериализует объект newUser в JSON и подставит Content-Type application/json
            request.AddJsonBody(newUser);

            // 2. Act: отправляем запрос с десериализацией ответа в CreateUserResponseDto
            RestResponse<CreateUserResponseDto> response = await _client.ExecuteAsync<CreateUserResponseDto>(request);

            // 3. Assert: проверяем статус и валидируем поля DTO ответа
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(newUser.Name, response.Data.Name);
            Assert.Equal(newUser.Job, response.Data.Job);
            Assert.False(string.IsNullOrEmpty(response.Data.Id), "ID пользователя не должен быть пустым");
        }

        [Fact]
        [AllureStory("PUT /api/users/2 - Update User")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureIssue("2")] // Связываем с Issue #2 на GitHub (например, таска на обновление)
        [Trait("Category", "API-Positive")]
        public async Task Put_UpdateUser_Should_Return_200OK_And_UpdatedData()
        {
            // 1. Arrange: обновляемые данные
            var updatedUser = new UserRequestDto
            {
                Name = "morpheus",
                Job = "zion resident"
            };

            var request = new RestRequest("/api/users/2", Method.Put);
            request.AddJsonBody(updatedUser);

            // 2. Act
            RestResponse<UpdateUserResponseDto> response = await _client.ExecuteAsync<UpdateUserResponseDto>(request);

            // 3. Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(updatedUser.Name, response.Data.Name);
            Assert.Equal(updatedUser.Job, response.Data.Job);
        }
    }
}