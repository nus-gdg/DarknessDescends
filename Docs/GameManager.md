# GameManager
GameManager controls the high level state of the game which inclde:
* Pregame
* Running
* Pause

It will emit an event everytime this state changes.

# GameplayController
Gameplay Controller orchestrates the interactions between the different systems in the game. For example, when an enemy dies, it will have to update the score and drop a reward for the player.

It is responsible for:
1. Loading and Saving Saved Data
1. Handling logic Hero and Enemy death
1. Handling the end of the Game

The method OnEnemyDeath should be called when an enemy is killed by the player. It accepts as a single argument an `IEnemy`  corresponding to the enemy that died. The `IEnemy` promises the required details for updating score and drops.

# GameLogicManager

The GameLogicManager class keeps track of the following statistics:
- The player's score
- The player's high score
- The number of enemies defeated

# UIManager
Has references to the relevant User Interface Elements in the game.
It exposes the following fields in the Unity Editor:
- scoreDisplay, in charge of displaying the player's score
- timeDisplay, in charge of displaying the time elapsed
- endgameDisplay, a Game Over message for the player when the game has ended
- pauseMenu, to be displayed on the pause

# Saving Data

The SavedData class currently keeps track of only the Player’s High Score.

On Awake, the GameplayController will query if pre-existing saved data exists. If so, it will load the saved data and call the method UpdateStateBasedOnSaveData.
As only High Score data is currently saved, this method simply updates the Player’s current High Score.

When the Player dies, they are able to restart the level while either saving or deleting their SaveData.

If they choose the former, the RecordSavedData method is called. This method creates a new instance SaveData and stores all relevant information in this instance.
Then, this instance of SaveData is saved as a file on their computer.

If they choose the latter, the DeleteSaveData method is called. This will destroy the instance of SaveData on the Player’s computer.