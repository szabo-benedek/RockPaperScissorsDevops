using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Backend.Data;
using RockPaperScissors.Backend.Models;

namespace RockPaperScissors.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameDbContext _context;
        private static readonly string[] Choices = { "Rock", "Paper", "Scissors" };
        private static readonly Random _random = new Random();


        public GameController(GameDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PlayGame([FromBody] GameCreateDto dto)
        {
            try
            {
               
                if (!Choices.Contains(dto.PlayerChoice))
                {
                    return BadRequest(new Error("Invalid choice. Must be Rock, Paper, or Scissors."));
                }

                
                var computerChoice = Choices[_random.Next(Choices.Length)];

                
                var result = DetermineWinner(dto.PlayerChoice, computerChoice);

                
                var game = new Game
                {
                    PlayerChoice = dto.PlayerChoice,
                    ComputerChoice = computerChoice,
                    Result = result,
                    PlayedAt = DateTime.UtcNow
                };

                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();

                
                return Ok(new GameViewDto
                {
                    Id = game.Id,
                    PlayerChoice = game.PlayerChoice,
                    ComputerChoice = game.ComputerChoice,
                    Result = game.Result,
                    PlayedAt = game.PlayedAt
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(ex.Message));
            }
        }

        [HttpGet]
        public async Task<List<GameViewDto>> GetGames()
        {
            return await _context.Games
                .AsNoTracking()
                .Select(g => new GameViewDto
                {
                    Id = g.Id,
                    PlayerChoice = g.PlayerChoice,
                    ComputerChoice = g.ComputerChoice,
                    Result = g.Result,
                    PlayedAt = g.PlayedAt
                })
                .OrderByDescending(g => g.PlayedAt)
                .ToListAsync();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllGames()
        {
            try
            {
                var games = await _context.Games.ToListAsync();
                _context.Games.RemoveRange(games);
                await _context.SaveChangesAsync();
                return Ok(new { message = "All games deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(ex.Message));
            }
        }



        private string DetermineWinner(string playerChoice, string computerChoice)
        {
            if (playerChoice == computerChoice)
                return "Draw";

            return (playerChoice, computerChoice) switch
            {
                ("Rock", "Scissors") => "Win",
                ("Paper", "Rock") => "Win",
                ("Scissors", "Paper") => "Win",
                _ => "Loss"
            };
        }
    }
}
