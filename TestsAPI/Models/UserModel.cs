using System;

namespace AqaPortfolioProject.Models
{
    // Модель для отправки данных (Create / Update)
    public class UserRequestDto
    {
        public string Name { get; set; }
        public string Job { get; set; }
    }

    // Модель для ответа при создании пользователя (POST)
    public class CreateUserResponseDto
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Модель для ответа при обновлении пользователя (PUT)
    public class UpdateUserResponseDto
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
