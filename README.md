# MuteMic
## Quick description
This small app is used to mute and unmute a microphone and also set a specific volume for different apps.
It uses [.NET Framework 4.8.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481), [CSCore 1.2.1.2](https://github.com/filoe/cscore), [Fody 6.8.0](https://github.com/Fody/Fody) and [Costura for Fody 5.7.0](https://github.com/Fody/Costura).
CSCore is the library used to manage audio devices and volumes and Fody and Costura are used to embed everything into a single .exe file.

## Use case example
My usage of MuteMic is very easy to understand: I wanted an app able to mute or unmute my microphone on the fly by reversing its current state.
If it's mute => unmute and vice/versa, check (`AudioTools.ReverseMicMute(...)`).
==It also does a cool beep sound.==

To have more flexibility, I used the software of my mouse to bind this .exe to an available button.
Whatever I'm doing, working, gaming, speaking, I can trigger this app quickly with my mouse.

Also, from time to time, the default volume of some apps changes by itself (after an update for example). I just wanted to get rid of opening the volume mixer after each update. 
This app implements a method to set a custom volume depending on app (`AudioTools.SetCustomVolumeDependingOnApp(...)`).
