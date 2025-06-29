using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPingPong.UserInterface;

/// <summary>
/// Handles score display and user interface rendering for the game.
/// </summary>
public class UserInterface
{
    private SpriteFont _font;
    private SpriteBatch _spriteBatch;
    private int _playerScore;
    private int _aiScore;

    public int PlayerScore => _playerScore;
    public int AiScore => _aiScore;

    public UserInterface(SpriteBatch spriteBatch, SpriteFont font)
    {
        _spriteBatch = spriteBatch;
        _font = font;
    }

    public void IncrementPlayerScore()
    {
        _playerScore++;
    }

    public void IncrementAiScore()
    {
        _aiScore++;
    }

    public void Draw(Vector2 playerScorePosition, Vector2 aiScorePosition)
    {
        // Draw scores without calling Begin/End as the main game class will handle this
        _spriteBatch.DrawString(_font, $"Player: {_playerScore}", playerScorePosition, Color.White);
        _spriteBatch.DrawString(_font, $"AI: {_aiScore}", aiScorePosition, Color.White);
    }
}