using System;

namespace AqaPortfolioProject.Models
{
    // Модель для отправки данных (Create / Update)
    public class UserRequestDto
    {
        public required string Name { get; set; }
        public required string Job { get; set; }
    }

    // Модель для ответа при создании пользователя (POST)
    public class CreateUserResponseDto
    {
        public required string Name { get; set; }
        public required string Job { get; set; }
        public required string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Модель для ответа при обновлении пользователя (PUT)
    public class UpdateUserResponseDto
    {
        public required string Name { get; set; }
        public required string Job { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}