# MedievalDefenders

## Overview
MedievalDefenders is a 2D Tower Defense game developed as part of the Interdisciplinary Work IV course in the Computer Science program at PUC Minas. The game combines strategic gameplay with medieval-themed visuals, offering players an engaging and challenging experience.

## Features
- **2D Graphics**: The game features pixel-art graphics, providing a nostalgic and visually appealing experience.
- **Tower Defense Mechanics**: Players must strategically place towers to defend against waves of enemies.
- **Medieval Theme**: The game is set in a medieval world, with characters, enemies, and assets designed to reflect this theme.
- **Custom Animations**: Includes unique animations for characters, projectiles, and enemies.
- **Resource Management**: Players must manage resources effectively to build and upgrade towers.

## Project Structure
The project is organized into the following main directories:

- **Assets/**: Contains all game assets, including scripts, animations, prefabs, sprites, and tiles.
  - **_Scripts/**: Houses the core scripts for game mechanics, including managers, systems, and utilities.
  - **Animations/**: Includes animation files for characters and projectiles.
  - **Prefabs/**: Contains reusable game objects such as buttons, enemies, and projectiles.
  - **Scenes/**: Stores the game scenes.
  - **Sprites/**: Includes sprite assets for the game.
  - **Tiles/**: Contains tile assets for building the game environment.

- **Packages/**: Manages external dependencies and Unity packages.
- **ProjectSettings/**: Stores Unity project settings, such as input configurations and quality settings.

## Development Tools
- **Unity**: The game is developed using the Unity game engine.
- **C#**: Scripts are written in C# to implement game mechanics and logic.

## How to Run the Project
1. Clone the repository to your local machine.
2. Open the project in Unity (ensure you have the correct version installed).
3. Load the main scene from the `Scenes/` directory.
4. Press the Play button in the Unity editor to start the game.

## Contribution
Contributions to the project are welcome. If you wish to contribute, please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes with clear and descriptive messages.
4. Submit a pull request for review.

## License
This project is developed for educational purposes and may not be used for commercial purposes without permission.

## Acknowledgments
- **PUC Minas**: For providing the opportunity to develop this project as part of the Computer Science curriculum.
- **Team Members**: For their dedication and collaboration in creating this game.

## Technical Details

### Game Mechanics
- **Pathfinding**: The game uses algorithms to guide enemies along predefined paths towards the player's base.
- **Wave System**: Enemies spawn in waves, with increasing difficulty as the game progresses.
- **Tower Upgrades**: Towers can be upgraded to improve their damage, range, and special abilities.
- **Collision Detection**: Implemented to handle interactions between projectiles, enemies, and towers.

### Included Features
- **Enemy Types**: Multiple enemy types with unique behaviors and attributes.
- **Tower Types**: Various towers, each with distinct attack styles and upgrade paths.
- **Projectiles**: Includes animations and mechanics for different projectile types.
- **Resource System**: Players earn resources by defeating enemies, which can be used to build and upgrade towers.
- **Game Levels**: Multiple levels with unique layouts and challenges.

### Tools and Frameworks
- **Unity Physics**: Used for collision detection and movement.
- **Animation System**: Unity's Animator is used to create smooth transitions and animations for characters and objects.
- **Scriptable Objects**: Utilized for defining reusable data structures, such as enemy stats and tower configurations.
- **Tilemap System**: Used to design and render the game environment efficiently.

### Code Structure
- **Managers**: Handle core game systems such as wave spawning, resource management, and game state transitions.
- **Systems**: Implement specific mechanics like pathfinding and collision handling.
- **Utilities**: Provide helper functions and reusable components to streamline development.

### Performance Optimizations
- **Object Pooling**: Reduces runtime overhead by reusing objects like projectiles and enemies instead of instantiating and destroying them repeatedly.
- **Batch Rendering**: Combines multiple draw calls to improve rendering performance.

### Testing and Debugging
- **Debug Tools**: Includes in-game tools for testing wave progression, tower placement, and resource generation.
- **Unit Tests**: Core mechanics are tested to ensure reliability and prevent regressions.
