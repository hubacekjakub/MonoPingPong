using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoPingPong.Entities;

/// <summary>
/// Game ball entity with physics, collision detection, and speed management.
/// </summary>
public class Ball : GameEntity
{
    private const int BallSize = 20;
    private const float MaxSpeed = 400f;

    public Ball(GraphicsDevice graphicsDevice, Vector2 startPosition, Vector2 startVelocity)
        : base(CreateBallTexture(graphicsDevice), startPosition, startVelocity)
    {
    }

    public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
    {
        base.Update(gameTime, graphicsDevice);
        
        // Clamp velocity to prevent ball from moving too fast
        ClampVelocity();
    }

    public void ClampVelocity()
    {
        float speed = Velocity.Length();
        if (speed > MaxSpeed)
        {
            Velocity = Vector2.Normalize(Velocity) * MaxSpeed;
        }
    }

    private static Texture2D CreateBallTexture(GraphicsDevice graphicsDevice)
    {
        if (graphicsDevice == null) throw new ArgumentNullException(nameof(graphicsDevice));

        Texture2D ballTexture = new Texture2D(graphicsDevice, BallSize, BallSize);
        Color[] ballData = new Color[BallSize * BallSize];
        for (int i = 0; i < ballData.Length; ++i) ballData[i] = Color.White;
        ballTexture.SetData(ballData);
        return ballTexture;
    }
}