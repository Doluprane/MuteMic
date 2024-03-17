using CSCore.CoreAudioAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MuteMic
{
    internal sealed class AudioTools
    {
        public static MMDevice GetDefaultDevice(DataFlow df, Role role)
        {
            MMDevice defaultDevice = MMDeviceEnumerator.DefaultAudioEndpoint(df, role);
            if (defaultDevice == null)
            {
                throw new Exception("Device is null");
            }
            else
            {
                return defaultDevice;
            }
        }

        public static void ReverseMicMute(MMDevice recordingDevice)
        {
            Guid appGuid = Guid.Empty;
            AudioEndpointVolume defaultRecordingDeviceVolume = AudioEndpointVolume.FromDevice(recordingDevice);

            try
            {
                if (defaultRecordingDeviceVolume.IsMuted == true)
                {
                    defaultRecordingDeviceVolume.SetMute(false, appGuid);
                    defaultRecordingDeviceVolume.MasterVolumeLevelScalar = 1;
                    ConsoleBeep(false);
                }
                else if (defaultRecordingDeviceVolume.IsMuted == false)
                {
                    defaultRecordingDeviceVolume.SetMute(true, appGuid);
                    defaultRecordingDeviceVolume.MasterVolumeLevelScalar = 0;
                    ConsoleBeep(true);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        public static void SetCustomVolumeDependingOnApp(MMDevice playbackDevice, Dictionary<string, float> appVolume)
        {
            AudioSessionManager2 sessionManager = AudioSessionManager2.FromMMDevice(playbackDevice);
            AudioSessionEnumerator sessionEnumerator = sessionManager.GetSessionEnumerator();

            try
            {
                foreach (AudioSessionControl session in sessionEnumerator)
                {
                    AudioSessionControl2 sessionControlProcess = session.QueryInterface<AudioSessionControl2>();
                    SimpleAudioVolume volume = session.QueryInterface<SimpleAudioVolume>();
                    string processName = Process.GetProcessById(sessionControlProcess.ProcessID).ProcessName;

                    if (appVolume.ContainsKey(processName))
                    {
                        volume.MasterVolume = appVolume[processName];
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
        private static void ConsoleBeep(bool muted)
        {
            if (muted)
            {
                for (int i = 400; i >= 200; i -= 100)
                {
                    Console.Beep(i, 150);
                }
            }
            else
            {
                for (int i = 200; i <= 400; i += 100)
                {
                    Console.Beep(i, 150);
                }
            }
        }
    }
}
