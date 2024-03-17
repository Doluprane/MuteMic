using System.Collections.Generic;

namespace MuteMic
{
    internal class Program
    {
        static void Main()
        {
            // Dictionnary used to define wanted volume depending on app, this could be also added in a .ini file
            Dictionary<string, float> appVolume = new Dictionary<string, float>
            {
                { "Spotify", 1 },
                { "Discord", 0.3F },
                { "Idle", 0.1F }
            };

            // ## Setting custom volume depending on app
            AudioTools.SetCustomVolumeDependingOnApp(AudioTools.GetDefaultDevice(CSCore.CoreAudioAPI.DataFlow.Render, CSCore.CoreAudioAPI.Role.Communications), appVolume);

            // ## Muting/unmuting microphone
            AudioTools.ReverseMicMute(AudioTools.GetDefaultDevice(CSCore.CoreAudioAPI.DataFlow.Capture, CSCore.CoreAudioAPI.Role.Communications));
        }
    }
}
