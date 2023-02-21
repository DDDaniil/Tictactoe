using Microsoft.EntityFrameworkCore;

namespace Tictactoe.API.Models;

[Keyless]
public class Room
{
    public string RoomName { get; set; }
}