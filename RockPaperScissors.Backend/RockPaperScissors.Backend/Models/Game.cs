namespace RockPaperScissors.Backend.Models
{
    public class Game
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PlayerChoice { get; set; } = string.Empty;
        public string ComputerChoice { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

    }
}
