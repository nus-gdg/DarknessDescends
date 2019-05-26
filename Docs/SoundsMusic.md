# Singleton

Sounds are played using the SoundController class, and Music is played with the MusicController class. Both of these classes are singleton classes, and should only be created once, when the game is starting, typically by already being in the scene in the Unity editor. They will persist across scene changes.

Both of these classes also emit sound through the use of an Audio Source. The audio source should be specified in the unity editor. You can try to move the audio source to direct where the audio is coming from, in this case we are simply using a single audio source and placing it directly on the main camera.

Both the SoundController and MusicController also support muting them, as well as an image to display depending on this status. This mute status is also saved in a player pref to persist across sessions. This is currently not utilized within the game.

# SoundController

The SoundController has a list of attributes, each of them referring to a sound that should be played. These are meant to be assigned within the Unity editor. Then, to play these sounds, call `SoundController.theController.playSound(SoundController.theController.sound)`. This would play `sound` immediately from the SoundController's Audio Source.

# MusicController

Similar to the SoundController, the music controller has a list of attributes, each referring to music that should be played. One of the attributes, however, is an array. This is intended to be used as a set of tracks to play, when you call `playRandomMusic()` (which is also called when the game starts), a randomly chosen track from that list is played.

To play Music with the MusicController, call `MusicController.theController.PlayMusic(MusicController.theController.music)`. This will immediately start playing `music` from the MusicController's audio source.