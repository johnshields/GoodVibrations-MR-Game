<h1 align="center">Gesture Based UI Development</h1>

<h4 align="center">John Shields - G00348436</h4>

***

<p align="center">
<a href="https://github.com/johnshields/GoodVibrations-MR-Game">
<img src="good_vibes.png" alt="GAME" width="400"/>
</a>
</p>

# Overview
Good Vibrations is a Mixed Reality Game that allows the user to control the game using voice commands, keyboard, and mouse. The player plays as a dog in the game and collects bones around the vibrant park setting filled with other dogs and their owners. The game has music upbeat music that fits the setting and is credited in the game's GitHub Repository. Once all the bones are collected, the game ends and loads back to the Main Menu.

The inspiration for the game's title came from the back story of how the Beach Boy's song titled 'Good Vibrations' was written. The song was inspired by how dogs pick up vibrations from humans around them. With that being said, the title provides two means for the game, one for the main dog character and one for the voice commands from the users playing the game.

# Controls &  Voice Commands
This game, as said above, was developed for a mixed reality experience. Meaning the game's controls blends the use of voice commands with a computer keyboard and mouse.

## Main Menu
For the Main Menu, the voice commands implemented are used to control the starting the game and exiting the game. All commands are listed below.

### Starting The Game
* start the game
* play the game
* begin the game
* continue the game

These commands fall under the rule `start`. All the commands above are used in a Switch Statement to call the function `StartGame` in the Main Menu Script. This function then starts a Coroutine function called `PlayGame`, which works with two other scripts named `FadeMusic` and `SceneChanger` to provide a smooth fade to black transition to the game's park setting.

### Exiting The Game
* exit the game
* quit the game
* close the game

The rule for these commands is `exit`. All these commands are used to exit the game by calling the function `ExitGame` in the Main Menu Script by the use of the same switch statement used for starting the game.

These functionalities are not limited to voice commands. They can also be done through the UI by clicking the buttons on the Main Menu's UI.

## Pause Menu
The  Pause Menu was initially planned to be controlled by voice commands as well. In an early stage of the game, a pause menu with voice commands was implemented. While testing the commands, it did not work so well. Many attempts were made to get it working right, but the commands often conflicted with others and caused the menu to flicker. For example, there was one command that loaded the Main Menu, and with music being played in the game, the grammar recognizer would pick up words from the music and load back the main menu right in the middle of game progress. This turned out not to be an ideal choice and was forfeited. The pause menu is now controlled by the `Esc` key and the menu's UI buttons for resuming the game and going back to the main menu.   

## Main Character
The voice commands for the dog are used for actions and animations. The dog's voice commands consist of 4 rules. These rules are ``idle``, ``sit``, ``walk``, and ``run``, and their commands are listed below.

Each of these voice commands is followed with ``"dog"``

* idle, yield, stop or halt
* sit or rest
* walk, go, stroll or wander
* run, jog, or dash

All these commands call functions to have the dog be idle, have the dog sit and move in a low or high profile. These commands are blended with the keyboard and mouse. The Spacebar is used to make the dog jump, and the mouse is used to rotate the dog in any desired direction. Like the Main Menu commands, these commands are in a switch statement that calls all the necessary functions to make the dog do the actions and animations listed above.

All the controls and voice commands can be found in the game by going to the Controls Menu from the Main Menu.

# Conclusion
Developing this game was a unique experience. The uniqueness was trying to develop the idea that would work well for a mixed reality experience, which allowed my creative side to flow and develop a fully functional game. I believe the final product turned out quite well from the initial idea and provides a smooth, interactive, and enjoyable experience for the user playing it.

***
<h4 align="center">END OF DOCUMENT</h4>
