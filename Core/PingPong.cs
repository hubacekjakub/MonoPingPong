using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoPingPong.Entities;
using MonoPingPong.Input;
using MonoPingPong.Physics;
using System;

namespace MonoPingPong.Core;

/// <summary>
/// Main game class that manages the MonoPingPong game loop, states, and rendering.
/// </summary>
public class PingPong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Paddle _playerPaddle;
    private Paddle _aiPaddle;
    private Ball _ball;
    private MonoPingPong.UserInterface.UserInterface _userInterface;
    private Random _random;
    
    // New components
    private InputManager _inputManager;
    private CollisionManager _collisionManager;
    private AIController _aiController;
    private GameStateManager _gameStateManager;
    
    // Game configuration constants
    private const float PlayerPaddleSpeed = 300f;
    private const float AiPaddleSpeed = 260f;
    private const float InitialBallSpeed = 200f;
    private const int ScoreToWin = 5; // Score needed to win the game

    // Difficulty settings
    private float _aiDifficulty = 0.5f; // Default to medium difficulty

    // UI Strings
    private readonly string[] _difficultyLevels = { "Easy", "Medium", "Hard" };
    private int _currentDifficultyIndex = 1; // Default to Medium

    public PingPong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _random = new Random();
    }

    protected override void Initialize()
    {
        // Set window size for better gameplay
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Initialize entities with simplified constructors
        _playerPaddle = new Paddle(GraphicsDevice, new Vector2(40, GraphicsDevice.Viewport.Height / 2 - 50));
        _aiPaddle = new Paddle(GraphicsDevice, new Vector2(GraphicsDevice.Viewport.Width - 60, GraphicsDevice.Viewport.Height / 2 - 50));
        
        // Create ball with random direction
        Vector2 ballVelocity = GetRandomBallVelocity();
        _ball = new Ball(GraphicsDevice, 
            new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 
            ballVelocity);

        // Initialize UserInterface
        _userInterface = new MonoPingPong.UserInterface.UserInterface(_spriteBatch, Content.Load<SpriteFont>("DefaultFont"));
        
        // Initialize new components
        _inputManager = new InputManager();
        _collisionManager = new CollisionManager(_ball, GraphicsDevice.Viewport.Bounds);
        _aiController = new AIController(_aiPaddle, _ball, AiPaddleSpeed, _aiDifficulty); // Use difficulty setting
        _gameStateManager = new GameStateManager(GameState.MainMenu); // Start in main menu state
    }

    private Vector2 GetRandomBallVelocity()
    {
        // Generate a random angle between -45 and 45 degrees (avoiding too horizontal angles)
        float angle = MathHelper.ToRadians((float)(_random.NextDouble() * 90 - 45));
        
        // Randomly choose left or right direction
        if (_random.Next(2) == 0)
            angle = MathHelper.Pi - angle; // Flip angle for left direction
            
        return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * InitialBallSpeed;
    }

    protected override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Update input state
        _inputManager.Update();
        
        // Check for exit
        if (_inputManager.IsExitPressed())
            Exit();
            
        // Process based on current game state
        switch (_gameStateManager.CurrentState)
        {
            case GameState.Playing:
                UpdatePlaying(gameTime, deltaTime);
                break;
                
            case GameState.Paused:
                // Handle pause state
                if (_inputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
                    _gameStateManager.ChangeState(GameState.Playing);
                break;
                
            case GameState.GameOver:
                // Handle game over state
                if (_inputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    ResetGame();
                break;
                
            case GameState.MainMenu:
                // Handle main menu state
                if (_inputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    // Start the game
                    _gameStateManager.ChangeState(GameState.Playing);
                }
                
                // Handle difficulty selection
                if (_inputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D))
                {
                    // Cycle through difficulty levels
                    _currentDifficultyIndex = (_currentDifficultyIndex + 1) % _difficultyLevels.Length;
                    
                    // Update AI difficulty based on selection
                    switch (_currentDifficultyIndex)
                    {
                        case 0: // Easy
                            _aiDifficulty = 0.3f;
                            break;
                        case 1: // Medium
                            _aiDifficulty = 0.5f;
                            break;
                        case 2: // Hard
                            _aiDifficulty = 0.8f;
                            break;
                    }
                    
                    // Update AI controller with new difficulty
                    _aiController = new AIController(_aiPaddle, _ball, AiPaddleSpeed, _aiDifficulty);
                }
                break;
        }

        base.Update(gameTime);
    }
    
    private void UpdatePlaying(GameTime gameTime, float deltaTime)
    {
        float lowerBound = GraphicsDevice.Viewport.Height - _playerPaddle.Texture.Height;

        // Handle player input
        _inputManager.HandlePlayerPaddleInput(_playerPaddle, PlayerPaddleSpeed, deltaTime, 0, lowerBound);
        
        // Update AI paddle
        _aiController.Update(deltaTime, 0, lowerBound);

        // Update ball position
        _ball.Update(gameTime, GraphicsDevice);

        // Check for collisions
        _collisionManager.CheckWallCollisions();
        _collisionManager.CheckPaddleCollision(_playerPaddle);
        _collisionManager.CheckPaddleCollision(_aiPaddle);
        
        // Check for scoring
        bool leftPlayerScored;
        if (_collisionManager.CheckScoringCollision(out leftPlayerScored))
        {
            if (leftPlayerScored)
                _userInterface.IncrementPlayerScore();
            else
                _userInterface.IncrementAiScore();
                
            ResetBall();
            
            // Check for win condition
            if (_userInterface.PlayerScore >= ScoreToWin || _userInterface.AiScore >= ScoreToWin)
                _gameStateManager.ChangeState(GameState.GameOver);
        }
        
        // Check for pause
        if (_inputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
            _gameStateManager.ChangeState(GameState.Paused);
    }

    private void ResetGame()
    {
        // Reset scores
        _userInterface = new MonoPingPong.UserInterface.UserInterface(_spriteBatch, Content.Load<SpriteFont>("DefaultFont"));
        
        // Reset entities
        ResetBall();
        
        // Change state to playing
        _gameStateManager.ChangeState(GameState.Playing);
    }

    private void ResetBall()
    {
        Vector2 centerPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        Vector2 randomVelocity = GetRandomBallVelocity();
        _ball.Reset(centerPosition, randomVelocity);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        // Draw different elements based on game state
        switch (_gameStateManager.CurrentState)
        {
            case GameState.Playing:
            case GameState.Paused:
                // Draw game elements
                _playerPaddle.Draw(_spriteBatch, Color.Green);
                _aiPaddle.Draw(_spriteBatch, Color.Blue);
                _ball.Draw(_spriteBatch, Color.Red);
                
                // Draw UI elements
                _userInterface.Draw(new Vector2(10, 10), new Vector2(GraphicsDevice.Viewport.Width - 100, 10));
                
                // Draw pause message if paused
                if (_gameStateManager.CurrentState == GameState.Paused)
                {
                    string pauseText = "PAUSED - Press P to continue";
                    Vector2 textSize = Content.Load<SpriteFont>("DefaultFont").MeasureString(pauseText);
                    Vector2 position = new Vector2(
                        GraphicsDevice.Viewport.Width / 2 - textSize.X / 2,
                        GraphicsDevice.Viewport.Height / 2 - textSize.Y / 2);
                    _spriteBatch.DrawString(Content.Load<SpriteFont>("DefaultFont"), pauseText, position, Color.White);
                }
                break;
                
            case GameState.GameOver:
                // Draw game over screen
                string winnerText = _userInterface.PlayerScore >= ScoreToWin 
                    ? "You Win!" 
                    : "AI Wins!";
                string scoreText = $"Final Score - Player: {_userInterface.PlayerScore} | AI: {_userInterface.AiScore}";
                string gameOverText = "Press Enter to play again";
                
                SpriteFont gameOverFont = Content.Load<SpriteFont>("DefaultFont");
                Vector2 winnerSize = gameOverFont.MeasureString(winnerText);
                Vector2 scoreSize = gameOverFont.MeasureString(scoreText);
                Vector2 gameOverSize = gameOverFont.MeasureString(gameOverText);
                
                _spriteBatch.DrawString(gameOverFont, winnerText, 
                    new Vector2(GraphicsDevice.Viewport.Width / 2 - winnerSize.X / 2, 200), Color.Yellow);
                _spriteBatch.DrawString(gameOverFont, scoreText, 
                    new Vector2(GraphicsDevice.Viewport.Width / 2 - scoreSize.X / 2, 250), Color.White);
                _spriteBatch.DrawString(gameOverFont, gameOverText, 
                    new Vector2(GraphicsDevice.Viewport.Width / 2 - gameOverSize.X / 2, 350), Color.White);
                break;
                
            case GameState.MainMenu:
                // Draw main menu
                string titleText = "PING PONG";
                string startText = "Press Enter to Start";
                
                SpriteFont font = Content.Load<SpriteFont>("DefaultFont");
                Vector2 titleSize = font.MeasureString(titleText);
                Vector2 startSize = font.MeasureString(startText);
                
                _spriteBatch.DrawString(font, titleText, 
                    new Vector2(GraphicsDevice.Viewport.Width / 2 - titleSize.X / 2, 200), Color.White);
                _spriteBatch.DrawString(font, startText, 
                    new Vector2(GraphicsDevice.Viewport.Width / 2 - startSize.X / 2, 300), Color.White);
                
                // Draw difficulty selection info
                string difficultyText = $"Difficulty: {_difficultyLevels[_currentDifficultyIndex]} (Press D to change)";
                Vector2 difficultySize = font.MeasureString(difficultyText);
                _spriteBatch.DrawString(font, difficultyText, 
                    new Vector2(GraphicsDevice.Viewport.Width / 2 - difficultySize.X / 2, 350), Color.Yellow);
                break;
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        // Dispose managed resources
        _spriteBatch?.Dispose();
        _playerPaddle?.Texture?.Dispose();
        _aiPaddle?.Texture?.Dispose();
        _ball?.Texture?.Dispose();
        
        base.UnloadContent();
    }
}
