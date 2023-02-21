using Microsoft.EntityFrameworkCore;

namespace Tictactoe.API.Models;

[Keyless]
public class PlayerInfo
{
    public string Username { get; set; }
}