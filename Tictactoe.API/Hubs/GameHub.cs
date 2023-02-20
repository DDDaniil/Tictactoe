using Microsoft.AspNetCore.SignalR;
using Tictactoe.API.Game;
using Tictactoe.API.Models;

namespace Tictactoe.API.Hubs;

public class GameHub : Hub
{
    private readonly Game.Game _game;

    public GameHub(Game.Game game)
    {
        _game = game;
    }
    
    private string Username => Context!.UserIdentifier!;
    
    public void JoinGame(string roomName)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        _game.JoinGame(
            username: Username,
            connectionId: Context.ConnectionId,
            roomName: roomName);
        
        Clients.Group(roomName)
            .SendAsync("OnTurn",
                _game.GetTable(roomName),
                (int) _game.GetTurn(roomName));
    }
    
    public void CreateGame()
    {
        Groups.AddToGroupAsync(Context.ConnectionId, Username);
        _game.CreateGame(Username);
        GetGames();
    }
    
    public void GetGames()
    {
        Clients.All.SendAsync("Games", _game.GetGames());
    }
    
    public void MakeTurn(CellType cellType, int index, string roomName)
    {
        _game.MakeMove(cellType, index, roomName);

        var status = _game.GetGameStatus(roomName);
        if (status is GameInfo.GameStatus.End or GameInfo.GameStatus.Tie)
        {
            Clients.Group(roomName)
                .SendAsync("OnTurn",
                    _game.GetTable(roomName)
                    , _game.GetTurn(roomName));
            Clients.Group(roomName).SendAsync("GameEnded", status, cellType);
        }

        Clients.Group(roomName)
            .SendAsync("OnTurn",
                _game.GetTable(roomName)
                , _game.GetTurn(roomName));
    }
}