using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoPingPong.Entities;

namespace MonoPingPong.Input;

public class InputManager
{
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;
    private GamePadState _currentGamePadState;
    private GamePadState _previousGamePadState;

    public void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
        
        _previousGamePadState = _currentGamePadState;
        _currentGamePadState = GamePad.GetState(PlayerIndex.One);
    }

    public bool IsExitPressed()
    {
        return _currentGamePadState.Buttons.Back == ButtonState.Pressed || 
               _currentKeyboardState.IsKeyDown(Keys.Escape);
    }

    public void HandlePlayerPaddleInput(Paddle playerPaddle, float paddleSpeed, float deltaTime, float minBound, float maxBound)
    {
        if (_currentKeyboardState.IsKeyDown(Keys.W))
        {
            playerPaddle.Move(-1, paddleSpeed, deltaTime, minBound, maxBound);
        }
        
        if (_currentKeyboardState.IsKeyDown(Keys.S))
        {
            playerPaddle.Move(1, paddleSpeed, deltaTime, minBound, maxBound);
        }
    }

    public bool IsKeyPressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
    }
}