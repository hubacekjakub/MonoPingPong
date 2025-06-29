# MonoPingPong 🏓

A classic Pong game implementation built with MonoGame framework in C#. This project demonstrates two key technologies: exploring the MonoGame engine for the first time and showcasing GitHub Copilot Agent's code generation capabilities.

## 🎯 Project Purpose

This demonstration project has two primary objectives:

1. **MonoGame Engine Exploration**: First-time experience with MonoGame framework for 2D game development
2. **GitHub Copilot Agent Testing**: Showcasing AI code generation and refactoring capabilities - the majority of this codebase was written by GitHub Copilot Agent

## 🎮 Features

- **Classic Pong Gameplay**: Two-paddle ball bouncing action with AI opponent
- **Difficulty Settings**: Easy, Medium, and Hard AI difficulty levels  
- **Game State Management**: Main menu, gameplay, pause, and game over states
- **Score Tracking**: First to 5 points wins
- **Clean Graphics**: Simple, focused visual design with color-coded paddles

## 🎯 Controls

- **W/S** - Move player paddle up/down
- **P** - Pause/unpause game
- **Enter** - Start game / play again
- **D** - Change difficulty (in main menu)
- **Escape** - Exit game

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Windows OS (MonoGame Windows platform)

### Quick Start
```bash
git clone [your-repo-url]
cd MonoPingPong
dotnet tool restore
dotnet build
dotnet run
```

### CI/CD
Includes GitHub Actions workflow for automated building, testing, and artifact generation.

## 🏗️ Architecture

Clean separation of concerns with:
- **Core**: Main game logic, AI controller, state management
- **Entities**: Ball and paddle physics with base entity class
- **Input**: Centralized keyboard input handling
- **Physics**: Collision detection system
- **UI**: Score display and interface rendering

## 🤖 AI Development

This project showcases AI-assisted development:
- **Code Generation**: GitHub Copilot Agent wrote most of the codebase
- **Architecture Design**: Clean patterns and separation of concerns
- **Documentation**: Comprehensive project documentation
- **CI/CD Setup**: Professional development workflow

## 📄 License

Open source. Feel free to use and modify as needed.

## 🙏 Acknowledgments

- Built with [MonoGame](https://www.monogame.net/) framework
- Inspired by classic Pong (1972)
- **Primary Development**: GitHub Copilot Agent
- **Icon Design**: ChatGPT model

---

**Enjoy playing MonoPingPong!** 🏓✨
