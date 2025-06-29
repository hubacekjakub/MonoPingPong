using Microsoft.Xna.Framework;
using MonoPingPong.Entities;
using System;

namespace MonoPingPong.Physics;

/// <summary>
/// Manages collision detection between ball, paddles, and screen boundaries.
/// </summary>
public class CollisionManager
{
    private readonly Ball _ball;
    private readonly Rectangle _screenBounds;

    public CollisionManager(Ball ball, Rectangle screenBounds)
    {
        _ball = ball;
        _screenBounds = screenBounds;
    }

    public bool CheckWallCollisions()
    {
        bool collided = false;
        
        // Top wall collision
        if (_ball.Position.Y <= _screenBounds.Top)
        {
            _ball.Velocity = new Vector2(_ball.Velocity.X, Math.Abs(_ball.Velocity.Y));
            _ball.Position = new Vector2(_ball.Position.X, _screenBounds.Top + 1);
            collided = true;
        }
        // Bottom wall collision
        else if (_ball.Position.Y + _ball.Texture.Height >= _screenBounds.Bottom)
        {
            _ball.Velocity = new Vector2(_ball.Velocity.X, -Math.Abs(_ball.Velocity.Y));
            _ball.Position = new Vector2(_ball.Position.X, _screenBounds.Bottom - _ball.Texture.Height - 1);
            collided = true;
        }

        return collided;
    }

    public bool CheckPaddleCollision(Paddle paddle)
    {
        // Create collision rectangles
        Rectangle ballRect = new Rectangle(
            (int)_ball.Position.X, 
            (int)_ball.Position.Y, 
            _ball.Texture.Width, 
            _ball.Texture.Height);
            
        Rectangle paddleRect = new Rectangle(
            (int)paddle.Position.X, 
            (int)paddle.Position.Y, 
            paddle.Texture.Width, 
            paddle.Texture.Height);
            
        if (ballRect.Intersects(paddleRect))
        {
            // Determine if this is the left or right paddle based on position
            bool isLeftPaddle = paddle.Position.X < _screenBounds.Width / 2;
            
            // Store original X velocity direction before changing it
            float originalXDirection = Math.Sign(_ball.Velocity.X);
            
            // Reverse X velocity to bounce off the paddle
            _ball.Velocity = new Vector2(-_ball.Velocity.X, _ball.Velocity.Y);
            
            // Adjust Y velocity based on where ball hits paddle
            float paddleCenter = paddle.Position.Y + paddleRect.Height / 2;
            float ballCenter = _ball.Position.Y + ballRect.Height / 2;
            float offsetFactor = (ballCenter - paddleCenter) / (paddleRect.Height / 2);
            
            // Apply offset to Y velocity
            _ball.Velocity = new Vector2(_ball.Velocity.X, _ball.Velocity.Y + offsetFactor * 5f);
            
            // Slightly increase ball speed
            _ball.Velocity *= 1.05f;
            
            // Prevent ball from getting stuck in paddle
            // For left paddle (player), ball should be to the right of paddle
            // For right paddle (AI), ball should be to the left of paddle
            if (isLeftPaddle)
            {
                _ball.Position = new Vector2(paddleRect.Right + 1, _ball.Position.Y);
            }
            else
            {
                _ball.Position = new Vector2(paddleRect.Left - ballRect.Width - 1, _ball.Position.Y);
            }
            
            return true;
        }
        
        return false;
    }

    public bool CheckScoringCollision(out bool leftScore)
    {
        leftScore = false;
        
        // Left wall (right player scores)
        if (_ball.Position.X <= _screenBounds.Left)
        {
            leftScore = false;
            return true;
        }
        // Right wall (left player scores)
        else if (_ball.Position.X + _ball.Texture.Width >= _screenBounds.Right)
        {
            leftScore = true;
            return true;
        }
        
        return false;
    }
}