namespace Tictactoe.API.Models;

public class UserInfo
{
    public string Username { get; set; }
    public string ConnectionId { get; set; }
    public PlayerType Type { get; set; }
}

public enum PlayerType
{
    Player
}

public enum Status
{
    Started,
    NotStarted
}

public enum CellType
{
    Empty,
    X,
    O
}
