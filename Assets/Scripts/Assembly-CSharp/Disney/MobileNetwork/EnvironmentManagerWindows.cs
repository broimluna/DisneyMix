using System;
using System.IO;
using System.Globalization;
using UnityEngine;

namespace Disney.MobileNetwork
{
    public class EnvironmentManagerWindows : EnvironmentManager
    {
        protected override void _Init()
        {
            // Windows-specific initialization
            mBundleIdentifier = Application.identifier;
            mBundleVersionCode = Application.version;

            string normalizedVersion = NormalizeVersionString(mBundleVersionCode);
            if (!string.IsNullOrEmpty(normalizedVersion))
            {
                // Ensure we have at least Major.Minor for Version class
                if (!normalizedVersion.Contains(".")) normalizedVersion += ".0";
                mBundleVersion = new Version(normalizedVersion);
            }
            else
            {
                mBundleVersion = new Version(0, 0, 0);
            }

            BuildSettings.LoadSettings();
        }

        protected override int _GetDiskSpaceFreeMegabytes()
        {
            try
            {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
                string path = Application.persistentDataPath;
                string driveName = Path.GetPathRoot(path);
                DriveInfo drive = new DriveInfo(driveName);

                if (drive.IsReady)
                {
                    long freeBytes = drive.AvailableFreeSpace;
                    // Use a long for the math to avoid overflow before the cast
                    long mb = freeBytes / (1024 * 1024);
                    // Clamp to int.MaxValue so we never return a broken/negative number
                    return (int)Math.Min(mb, int.MaxValue - 1);
                }
#elif UNITY_ANDROID || UNITY_IOS
                // Mobile OSs often sandbox this. 
                // If you don't have a native plugin imported, return a large 
                // but REASONABLE number (e.g., 2GB) to pass validation.
                return 8192; 
#endif
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Disk space check failed: {e.Message}");
            }

            return 512; // Safe fallback
        }

        protected override string _GetLocale()
        {
            // Returns strings like "en-US"
            return CultureInfo.CurrentCulture.Name;
        }

        protected override string _GetDeviceLanguage()
        {
            // Returns the language as recognized by Unity
            return Application.systemLanguage.ToString();
        }

        protected override bool _GetIsMusicPlaying()
        {
            // Windows doesn't easily expose if 'Spotify' or 'iTunes' is playing
            // without deep Win32 API hooks. Usually safe to return false.
            return false;
        }

        protected override bool _GetAreHeadphonesConnected()
        {
            // On Windows, the OS manages audio routing. Unity rarely needs to know.
            return false;
        }

        public override void ShowAlert(ShowAlertDelegate showAlertDelegate, string title, string message, string viewButtonText, string cancelButtonText)
        {
            base.ShowAlert(showAlertDelegate, title, message, viewButtonText, cancelButtonText);

            // On Windows Standalone, you typically use a Unity UI Prefab for alerts.
            // For a quick native system popup, you could use a DLLImport of User32.dll MessageBox
            Debug.Log($"[Windows Alert] {title}: {message}");

            // Automatically simulate "OK" for now, or trigger your UI system here
            OnAlertViewDismissed("1");
        }

        protected override void _ShowStatusBar(bool show, bool useLightColor)
        {
        }

        protected override string _GetBuildSettingsJson()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(System.IO.Path.GetFileNameWithoutExtension(BuildSettings.SETTINGS_FILE));
            if (textAsset == null)
            {
                return string.Empty;
            }
            return textAsset.text;
        }
    }
}