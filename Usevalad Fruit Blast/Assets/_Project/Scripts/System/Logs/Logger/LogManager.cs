using UnityEngine;

namespace _Project.Scripts.System.Logs.Logger
{
    public static class LogManager
    {
        public enum LogType
        {
            Debug = 0,
            Warning = 1,
            Error = 2
        }

        public static void RegisterLogMessage(LogType logType, string logMessage)
        {
            switch (logType)
            {
                case LogType.Debug:
                {
                    Debug.Log(logMessage);
                    break;
                }
                case LogType.Warning:
                {
                    Debug.LogWarning(logMessage);
                    break;
                }
                case LogType.Error:
                {
                    Debug.LogError(logMessage);
                    break;
                }
                default:
                {
                    Debug.Log(logMessage);
                    break;
                }
            }
        }
    }
}