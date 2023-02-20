using System.Collections.Concurrent;
using Tictactoe.API.Models;


namespace Tictactoe.API.Game;

public class GameInfo
{
    public enum GameStatus
    {
        InProgress,
        Tie,
        End
    }

    public enum Turn
    {
        X,
        O
    }
    
    public string Owner { get; set; }
    public Status Status { get; set; }
    public Turn WhoWalk { get; set; }
    public List<UserInfo> UserList { get; set; }

    public CellType[] Table { get; set; } = new[]
    {
        CellType.Empty, CellType.Empty, CellType.Empty,
        CellType.Empty, CellType.Empty, CellType.Empty,
        CellType.Empty, CellType.Empty, CellType.Empty,
    };

    public GameStatus GetStatus()
    {
        var isWin =
            CheckRow(new CellType[] {Table[0], Table[1], Table[2]}) || 
            CheckRow(new CellType[] {Table[2], Table[5], Table[6]}) ||
            CheckRow(new CellType[] {Table[8], Table[7], Table[6]}) ||
            CheckRow(new CellType[] {Table[6], Table[3], Table[0]}) ||
            CheckRow(new CellType[] {Table[3], Table[4], Table[5]}) || 
            CheckRow(new CellType[] {Table[1], Table[4], Table[7]}) ||
            CheckRow(new CellType[] {Table[0], Table[4], Table[8]}) || 
            CheckRow(new CellType[] {Table[2], Table[4], Table[6]});

        var isTie = !isWin && Table.All(cell => cell != CellType.Empty);

        return isWin
            ? GameStatus.End
            : isTie
                ? GameStatus.Tie
                : GameStatus.InProgress;
    }

    private bool CheckRow(CellType[] cells)
    {
        return cells.All(cell => cells[0] == cell && cell != CellType.Empty);
    }
};

public class Game
{
    private static readonly ConcurrentDictionary<string, GameInfo> _games = new();
    

    public CellType[] GetTable(string roomName) => _games[roomName].Table;

    public void CreateGame(string ownerName)
    {
        _games[ownerName] = new GameInfo
        {
            Owner = ownerName,
            WhoWalk = GameInfo.Turn.X,
            Status = Status.NotStarted,
            UserList = new List<UserInfo>()
        };
    }

    public void JoinGame(string username, string connectionId, string roomName)
    {
        _games[roomName].UserList.Add(new UserInfo
        {
            Username = username,
            ConnectionId = connectionId,
            Type = PlayerType.Player 
        });
    }
        
    public void MakeMove(CellType cellType, int index, string roomName)
    {
        _games[roomName].Table[index] = cellType;
        _games[roomName].WhoWalk =
            _games[roomName]
                .WhoWalk == GameInfo.Turn.X ?
                GameInfo.Turn.O : 
                GameInfo.Turn.X;
    }

    public GameInfo.GameStatus GetGameStatus(string roomName)
    {
        return _games[roomName].GetStatus();
    }

    public GameInfo.Turn GetTurn(string roomName)
    {
        return _games[roomName].WhoWalk;
    }
    
    public List<GameInfo> GetGames()
    {
        return _games.Values.ToList();
    }

}