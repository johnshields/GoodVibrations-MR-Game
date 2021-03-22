<h2 align="center">Voice & Grammar Project | John Shields - G00348436</h4>

## Overview
Good Vibrations is a Mixed Reality Game that allows the user to control the game using voice commands, keyboard, and mouse. The player plays as a dog in the game and collects bones around the vibrant park setting filled with other dogs and their owners. The game has upbeat music that fits the setting and is credited in the game's GitHub Repository. Once all the bones are collected, the game ends and loads back to the Main Menu.

The inspiration for the game's title came from the back story of how the Beach Boy's song titled 'Good Vibrations' was written. The song was inspired by how dogs pick up vibrations from humans around them. With that being said, the title provides two means for the game, one for the main dog character and one for the voice commands from the users playing the game.

## Controls &  Voice Commands
This game, as said above, was developed for a mixed reality experience. Meaning the game's controls blends the use of voice commands with a computer keyboard and mouse.

### Main Menu
For the Main Menu, the voice commands implemented are used to control the starting the game and exiting the game. All commands are listed below.

### Start the Game
These voice commands can be followed with `"the game"`
* start, play, begin or continue
* lets play, lets start, lets begin or lets continue
* I want to play, start, begin or continue
* I would like to play, start, begin or continue

These commands fall under the rule `start`. All the commands above are used in a Switch Statement to call the function `StartGame` in the `MainMenu` Script. This function then starts a Coroutine function called `PlayGame`, which works with two other scripts named `FadeMusic` and `SceneChanger` to provide a smooth fade to black transition to the game's park setting.


### Exit the Game
These voice commands can be followed with `"the game"`
* exit, quit or close

The rule for these commands is `exit`. All these commands are used to exit the game by calling the function `ExitGame` in the Main Menu Script by the use of the same switch statement used for starting the game.

These functionalities are not limited to voice commands. They can also be done through the UI by clicking the buttons on the Main Menu's UI.

<br><br>


### Pause Menu
The pause menu is controlled by the `Esc` key and voice commands and the menu's UI buttons for resuming the game and going back to the main menu.   

##### Resume Game

These voice commands can be followed with `"the game"`
* resume or go back
* take me back to the game

##### Back to the Main Menu
* main menu
* go home
* back to main menu
* back to the main menu
* bring up the main menu

These commands are working with the rules `resume` and `mainMenu`. These commands are used in a Switch Statement so the commands can call the functions `ResumeGame` and `BackMainMenu`.

### Main Character
The voice commands for the dog are used for actions and animations. The dog's voice commands consist of 4 rules. These rules are ``idle``, ``sit``, ``walk``, and ``run``, and their commands are listed below.

These voice commands can be followed with ``"dog"``, `"boy"` or `"girl"`
* Idle
   * be idle, yield, stop or halt
* Sit  
  * sit, rest relax or control yourself
* Walk
  * walk, go, stroll or wander
  * start or begin walking
  * start or begin strolling
  * start or begin wandering
* Run
  * run, jog, or dash
  * start or begin running
  * start or begin jogging
  * start or begin dashing
  * run, jog or dash around

All these commands call functions to have the dog be idle, have the dog sit and move in a low or high profile. These commands are blended with the keyboard and mouse. The space bar is used to make the dog jump, and the mouse is used to rotate the dog in any desired direction. Like the Menus commands, these commands are in a switch statement that calls all the necessary functions to make the dog do the actions and animations listed above.

## Conclusion
Developing this game was a unique experience. The uniqueness was trying to develop the idea that would work well for a mixed reality experience, which allowed my creative side to flow and develop a fully functional game. I believe the final product turned out quite well from the initial idea and provides a smooth, interactive, and enjoyable experience for the user playing it.
