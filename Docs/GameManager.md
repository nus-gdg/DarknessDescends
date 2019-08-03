# GameManager

The GameManager class keeps track of the following statistics:
- The player's score
- The player's high score
- The number of enemies defeated
- The time elapsed since the start of the game
- The state of the game e.g. if the player is dead or not

In addition, it exposes the following fields in the Unity Editor:
- scoreDisplay, in charge of displaying the player's score
- timeDisplay, in charge of displaying the time elapsed
- endgameDisplay, a Game Over message for the player when the game has ended
- SpawnerList, a list that should contain references to each Spawn Point in the current scene

The method enemyDies should be called when an enemy is killed by the player. It accepts as a single argument an _int_ value corresponding to the score earned from 
killing the enemy. The total score is incremented by the same amount.

The method playerCharacterDies should be called when the player dies. The GameManager will handle the game's state accordingly.

The private method SendMessagesToSpawnPoints will be called at each frame, and will update the time elapsed of each Spawn Point.

# Saving Data

The SavedData class currently keeps track of only the Player’s High Score.

On Awake, the GameManager will query if pre-existing saved data exists. If so, it will load the saved data and call the method UpdateStateBasedOnSavedData. 
As only High Score data is currently saved, this method simply updates the Player’s current High Score.

When the Player dies, they are able to restart the level while either saving or deleting their SavedData. 

If they choose the former, the RecordSavedData method is called. This method creates a new instance SavedData and stores all relevant information in this instance. 
Then, this instance of SavedData is saved as a file on their computer.

If they choose the latter, the DeleteSavedData method is called. This will destroy the instance of SavedData on the Player’s computer.