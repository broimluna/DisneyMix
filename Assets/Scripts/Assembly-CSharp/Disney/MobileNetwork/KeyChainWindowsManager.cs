using LitJson;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using UnityEngine;

namespace Disney.MobileNetwork
{
    public class KeyChainWindowsManager : KeyChainManager
    {
        private const string APP_DATA_KEY = "cp.AppData";
        private const string APP_DATA_FILE_NAME = "cp.AppData.dat";
        private const string DLL_NAME = "KeyChainWindows";

        // Internal cache of the key-value pairs
        private Dictionary<string, string> appData = new Dictionary<string, string>();

        #region Native DLL Imports
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int _cryptProtectData(string dataIn, ref int dataOutSize, out IntPtr dataOut);

        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int _cryptUnprotectData(byte[] dataIn, int dataInLength, out IntPtr dataOut);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void _keyChainFree(IntPtr ptr);
        #endregion

        protected override void Init()
        {
            // Equivalent to "GenerateAndStoreKey" - we load existing data into memory
            LoadFromDisk();
        }

        public override void PutString(string key, string value)
        {
            appData[key] = value;
            SaveToDisk();
        }

        public override string GetString(string key)
        {
            if (appData.TryGetValue(key, out string value))
            {
                return value;
            }
            return null;
        }

        public override void RemoveString(string key)
        {
            if (appData.Remove(key))
            {
                SaveToDisk();
            }
        }

        #region Internal Logic
        private void LoadFromDisk()
        {
            string encryptedBase64 = null;
            string path = Path.Combine(Application.persistentDataPath, APP_DATA_FILE_NAME);

            // 1. Try to load from file, fallback to PlayerPrefs
            if (File.Exists(path)) encryptedBase64 = File.ReadAllText(path);
            if (string.IsNullOrEmpty(encryptedBase64)) encryptedBase64 = PlayerPrefs.GetString(APP_DATA_KEY, null);

            if (string.IsNullOrEmpty(encryptedBase64)) return;

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                IntPtr outPtr = IntPtr.Zero;

                // 2. Decrypt using Native DLL
                int success = _cryptUnprotectData(encryptedBytes, encryptedBytes.Length, out outPtr);

                if (success != 0 && outPtr != IntPtr.Zero)
                {
                    string json = Marshal.PtrToStringAnsi(outPtr);
                    _keyChainFree(outPtr);
                    appData = JsonMapper.ToObject<Dictionary<string, string>>(json);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"KeyChainWindows: Failed to load data: {e.Message}");
            }
        }

        private void SaveToDisk()
        {
            try
            {
                string json = JsonMapper.ToJson(appData);
                IntPtr outPtr = IntPtr.Zero;
                int size = 0;

                // 1. Encrypt using Native DLL
                int success = _cryptProtectData(json, ref size, out outPtr);

                if (success != 0 && outPtr != IntPtr.Zero)
                {
                    byte[] managedArray = new byte[size];
                    Marshal.Copy(outPtr, managedArray, 0, size);
                    _keyChainFree(outPtr);

                    string base64 = Convert.ToBase64String(managedArray);

                    // 2. Persistent Storage
                    PlayerPrefs.SetString(APP_DATA_KEY, base64);
                    PlayerPrefs.Save();
                    File.WriteAllText(Path.Combine(Application.persistentDataPath, APP_DATA_FILE_NAME), base64);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"KeyChainWindows: Failed to save data: {e.Message}");
            }
        }
        #endregion
    }
}