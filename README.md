# Windows Presentation Foundation .NET Core 3.0 Tetris

Tetris&trade; clone based around .NET Core 3.0 C# and WPF utilising MVVM architecture, ReactiveProperties and Prism.MVVM.
Application was built as a semester project.

## Core features of the game

Game follows basic mechanics of the game Tetris&trade; and was built in complience with the rules of the game.
All of terms and definitions used in the project are derived from the Tetris&trade; naming convention (i.e. Tetrimino).

### Basic features include:
* Automatic movement of Tetriminos with gradual speed up of the pieces as the game progresses
* Ability to manually rotate Tetriminos around their respective rotation axis
* Ability to manually move Tetrimons to the left, right and force them to instantly be placed below their current position
* Automatic Tetrimino generation with implemented randomizer of Tetrimino kind
* Score tracking with number of cleared rows distinction

## Game controls

| Key | Function |
| --- | --- |
| Right arrow | Move Tetrimino to the right |
| Left arrow | Move Tetrimino to the left |
| Down arrow | Force Tetrimino to go to the bottom |
| Up arrow | Rotate Tetrimino |
| Escape | Start new game (on gameover only) |

## Built with

* [Prism](https://prismlibrary.com/) - Prism library used for easier building and maintaining MVVM architecture
* [ReactiveProperty](https://github.com/runceel/ReactiveProperty) - ReactiveProperty provides asynchronous MVVM properties

## Authors

* **Filip Kosmala** - *Game mechanics and Team leader* - [FrostyShadow](https://github.com/FrostyShadow)
* **Natalia Machowska** - *Project documentation and UI design* - [nataliaamachowskaa](https://github.com/nataliaamachowskaa)
* **Wojciech Kolarski** - *Project documentation and UI design* - [WojciechKolarski](https://github.com/WojciechKolarski)

## License

This project is licensed under the MIT License

## Acknowledgments

* **Taaki Suzuki** - Creator of the WpfTetris game built around .NET Framework. His work inspired many aspects of, and was the general inspiration for creating, this project - [xin9le](https://github.com/xin9le)
