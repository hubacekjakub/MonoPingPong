using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPingPong.Core;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameStateManager
{
    private GameState _currentState;
    
    public GameState CurrentState => _currentState;
    
    public GameStateManager(GameState initialState = GameState.MainMenu)
    {
        _currentState = initialState;
    }
    
    public void ChangeState(GameState newState)
    {
        _currentState = newState;
    }
    
    public bool IsState(GameState state)
    {
        return _currentState == state;
    }
}