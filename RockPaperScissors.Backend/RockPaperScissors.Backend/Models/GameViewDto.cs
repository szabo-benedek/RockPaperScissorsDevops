namespace RockPaperScissors.Backend.Models
{
    public class GameViewDto
    {

        public string Id { get; set; } = string.Empty;
        public string PlayerChoice { get; set; } = string.Empty;
        public string ComputerChoice { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; }
    }
}
