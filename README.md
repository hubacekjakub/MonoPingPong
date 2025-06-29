# MonoPingPong 🏓

A classic Pong game implementation built with MonoGame framework in C#. This project serves as a demonstration of two key technologies: exploring the MonoGame engine for the first time and showcasing the capabilities of GitHub Copilot Agent for automated code generation and refactoring.

Features AI opponents with adjustable difficulty levels, smooth gameplay, and modern game state management.

## � Project Purpose

This MonoPingPong implementation was created as a demonstration project with two primary objectives:

1. **MonoGame Engine Exploration**: First-time experience with the MonoGame framework, exploring its capabilities for 2D game development, sprite rendering, input handling, and game loop management.

2. **GitHub Copilot Agent Testing**: Showcasing the code generation and refactoring capabilities of GitHub Copilot Agent, which wrote and refactored the majority of the codebase through iterative improvements and code reviews.

The project demonstrates how AI-assisted development can rapidly produce a functional game with clean architecture, proper separation of concerns, and modern C# programming patterns.

## �🎮 Features

- **Classic Pong Gameplay**: Two-paddle ball bouncing action
- **AI Opponent**: Intelligent computer opponent with three difficulty levels
- **Difficulty Settings**: Easy, Medium, and Hard AI difficulty options
- **Game State Management**: Main menu, gameplay, pause, and game over states
- **Score Tracking**: First to 5 points wins
- **Smooth Controls**: Responsive paddle movement with W/S keys
- **Pause Functionality**: Press P to pause/unpause during gameplay
- **Clean Graphics**: Simple, focused visual design with color-coded elements

## 🎯 How to Play

### Controls
- **W** - Move player paddle up
- **S** - Move player paddle down  
- **P** - Pause/unpause game
- **Enter** - Start game (from main menu) or play again (from game over)
- **D** - Change difficulty level (in main menu)
- **Escape** - Exit game

### Gameplay
1. Start the game and you'll see the main menu
2. Use **D** to cycle through difficulty levels (Easy/Medium/Hard)
3. Press **Enter** to start playing
4. Control the green paddle (left side) with W/S keys
5. Try to hit the ball past the blue AI paddle (right side)
6. First player to reach 5 points wins!

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Windows OS (due to MonoGame Windows platform targeting)

### Installation & Running

1. **Clone or download the project**
   ```bash
   git clone [your-repo-url]
   cd MonoPingPong
   ```

2. **Build the project**
   ```bash
   dotnet build
   ```

3. **Run the game**
   ```bash
   dotnet run
   ```

### Alternative: Using Visual Studio Code
1. Open the project folder in VS Code
2. Use `Ctrl+Shift+P` and run "Tasks: Run Task"
3. Select "Build MonoPingPong" to build
4. Select "Run MonoPingPong" to start the game

## 🏗️ Project Structure

```
MonoPingPong/
├── Core/
│   ├── PingPong.cs          # Main game class and logic
│   ├── GameStateManager.cs  # Handles game states (menu, playing, etc.)
│   └── AIController.cs      # AI opponent logic
├── Entities/
│   ├── Entity.cs           # Base game entity class
│   ├── Ball.cs             # Ball physics and behavior
│   └── Paddle.cs           # Paddle entity
├── Input/
│   └── InputManager.cs     # Keyboard input handling
├── Physics/
│   └── CollisionManager.cs # Collision detection system
├── UserInterface/
│   └── UserInterface.cs    # Score display and UI elements
├── Content/
│   └── DefaultFont.spritefont # Font resource
└── Program.cs              # Application entry point
```

## 🎨 Game Architecture

The game follows a clean architecture pattern with separated concerns:

- **Game States**: Managed through `GameStateManager` (MainMenu, Playing, Paused, GameOver)
- **Entity System**: Base `GameEntity` class with specialized `Ball` and `Paddle` implementations
- **Input Handling**: Centralized `InputManager` for keyboard input processing
- **Physics**: Dedicated `CollisionManager` for ball-paddle and ball-wall collisions
- **AI**: Configurable `AIController` with difficulty-based behavior modifications

## 🤖 AI Difficulty Levels

- **Easy** (0.3): Slower reaction time, less accurate positioning
- **Medium** (0.5): Balanced reaction and accuracy
- **Hard** (0.8): Fast reactions, highly accurate positioning

## 🤖 Development Process

This project showcases AI-assisted development using GitHub Copilot Agent:

- **Initial Setup**: Agent created the basic MonoGame project structure
- **Architecture Design**: Implemented clean separation of concerns with dedicated managers
- **Feature Implementation**: Iteratively added game states, AI difficulty, collision detection
- **Code Review & Refactoring**: Multiple iterations of code improvements and optimizations
- **Testing & Debugging**: Agent identified and fixed compilation and runtime issues

The development process demonstrates how AI can assist in:
- Rapid prototyping and initial implementation
- Code organization and architectural decisions
- Bug identification and resolution
- Documentation and best practices

## 🛠️ Technical Details

- **Framework**: MonoGame 3.8+
- **Target**: .NET 8.0 Windows
- **Graphics**: 2D sprite-based rendering
- **Resolution**: 800x600 windowed mode
- **Frame Rate**: 60 FPS target

## 📝 Development Notes

### Key Game Constants
```csharp
PlayerPaddleSpeed = 300f;     // Player paddle movement speed
AiPaddleSpeed = 260f;         // AI paddle movement speed  
InitialBallSpeed = 200f;      // Starting ball velocity
ScoreToWin = 5;              // Points needed to win
```

### Game Physics
- Ball speed increases by 5% on each paddle hit
- Ball is clamped to maximum speed to prevent excessive velocity
- Collision detection uses rectangle intersection
- Ball bounce angle varies based on paddle hit location

## 🐛 Known Issues & Limitations

- Windows-only due to MonoGame platform targeting
- Fixed window size (800x600)
- Single-player only (vs AI)

## 🤝 Contributing

Feel free to contribute improvements! Some ideas:
- Add sound effects and background music
- Implement multiplayer (two-player) mode
- Add particle effects for ball collisions
- Create different ball types or power-ups
- Add high score persistence
- Implement gamepad support

## 📄 License

This project is open source. Feel free to use and modify as needed.

## 🙏 Acknowledgments

- Built with [MonoGame](https://www.monogame.net/) framework
- Inspired by the classic Pong arcade game (1972)
- **Primary Development**: GitHub Copilot Agent wrote and refactored the majority of the codebase
- **Icon Design**: Application icon generated by ChatGPT model

### 🤖 AI Development Notes
This project serves as a case study in AI-assisted development, showing how GitHub Copilot Agent can:
- Generate clean, maintainable code architecture
- Implement game development patterns and best practices
- Perform iterative code reviews and improvements
- Create comprehensive documentation and project structure

---

**Enjoy playing MonoPingPong!** 🏓✨
