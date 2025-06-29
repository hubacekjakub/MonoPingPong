using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPingPong.Entities;

public class Paddle : GameEntity
{
    private const int PaddleWidth = 20;
    private const int PaddleHeight = 100;

    public Paddle(GraphicsDevice graphicsDevice, Vector2 startPosition)
        : base(CreatePaddleTexture(graphicsDevice), startPosition, Vector2.Zero)
    {
    }

    public void Move(float direction, float speed, float elapsedTime, float boundaryMin, float boundaryMax)
    {
        var newY = Math.Clamp(Position.Y + direction * speed * elapsedTime, boundaryMin, boundaryMax);
        Position = new Vector2(Position.X, newY);
    }

    private static Texture2D CreatePaddleTexture(GraphicsDevice graphicsDevice)
    {
        if (graphicsDevice == null) throw new ArgumentNullException(nameof(graphicsDevice));

        Texture2D paddleTexture = new Texture2D(graphicsDevice, PaddleWidth, PaddleHeight);
        Color[] paddleData = new Color[PaddleWidth * PaddleHeight];
        for (int i = 0; i < paddleData.Length; ++i) paddleData[i] = Color.White;
        paddleTexture.SetData(paddleData);
        return paddleTexture;
    }
}