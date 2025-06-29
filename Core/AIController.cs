using Microsoft.Xna.Framework;
using MonoPingPong.Entities;
using System;

namespace MonoPingPong.Core;

/// <summary>
/// Controls the AI paddle behavior with configurable difficulty levels.
/// </summary>
public class AIController
{
    private readonly Paddle _aiPaddle;
    private readonly Ball _ball;
    private readonly float _speed;
    private readonly Random _random;
    
    // Difficulty settings
    private readonly float _reactionDelay;
    private readonly float _accuracyFactor;
    
    public AIController(Paddle aiPaddle, Ball ball, float speed, float difficulty = 0.5f)
    {
        _aiPaddle = aiPaddle;
        _ball = ball;
        _speed = speed;
        _random = new Random();
        
        // Higher difficulty means lower reaction delay and higher accuracy
        _reactionDelay = 0.5f - (difficulty * 0.4f); // 0.1 to 0.5 seconds
        _accuracyFactor = difficulty;                // 0 to 1 accuracy factor
    }
    
    public void Update(float deltaTime, float minBound, float maxBound)
    {
        // Simple AI paddle logic - follow the ball's Y position
        float ballCenter = _ball.Position.Y + _ball.Texture.Height / 2;
        
        // Add some "imperfection" based on difficulty
        float imperfection = (float)_random.NextDouble() * 30 * (1 - _accuracyFactor);
        float targetPosition = ballCenter + imperfection;
        
        float paddleCenter = _aiPaddle.Position.Y + _aiPaddle.Texture.Height / 2;
        
        // Only move if there's a significant difference
        if (Math.Abs(paddleCenter - targetPosition) > 10)
        {
            // Calculate direction (up or down)
            float direction = paddleCenter < targetPosition ? 1 : -1;
            
            // Move the paddle
            _aiPaddle.Move(direction, _speed, deltaTime, minBound, maxBound);
        }
    }
}