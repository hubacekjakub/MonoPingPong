using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPingPong.Entities;

public abstract class GameEntity
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    protected internal Texture2D Texture { get; private set; }

    protected GameEntity(Texture2D texture, Vector2 position, Vector2 velocity)
    {
        Texture = texture ?? throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
        Position = position;
        Velocity = velocity;
    }

    public virtual void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(SpriteBatch spriteBatch, Color color)
    {
        if (spriteBatch == null) throw new ArgumentNullException(nameof(spriteBatch), "SpriteBatch cannot be null.");
        spriteBatch.Draw(Texture, Position, color);
    }

    public void Reset(Vector2 startPosition, Vector2 startVelocity)
    {
        Position = startPosition;
        Velocity = startVelocity;
    }
}