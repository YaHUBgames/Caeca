using UnityEngine;

namespace Caeca.DebugSystems
{
    /// <summary>
    /// Custom ScriptableObject-based debugger. 
    /// You can toggle each debugger separately and can see just the messages you want.
    /// It doesn't compile with build version.
    /// </summary>
    [System.Serializable, CreateAssetMenu(menuName = "Debuging/Logger")]
    public class DebugLogger : ScriptableObject
    {
        [ContextMenu("Toggle debug logger"), UnityEngine.Tooltip("Toggles this debug loggger.")]
        void ToggleLogger()
        {
            showLogError = !showLog;
            showLogWarning = !showLog;
            showLog = !showLog;
            showDebugDraws = !showLog;
        }

        [ContextMenu("Show debug logs"), UnityEngine.Tooltip("Turn on this debug loggger.")]
        public void ShowAllLogs()
        {
            showLogError = true;
            showLogWarning = true;
            showLog = true;
            showDebugDraws = true;
        }

        [ContextMenu("Show debug logs"), UnityEngine.Tooltip("Turn off this debug loggger.")]
        public void HideAllLogs()
        {
            showLogError = false;
            showLogWarning = false;
            showLog = false;
            showDebugDraws = false;
        }

        /// <summary>Controls whether or not Basic debug messages are logged.</summary>
        [UnityEngine.Tooltip("Log basic messages")]
        public bool showLog = false;
        /// <summary>Controls whether or not Warning debug messages are logged.</summary>
        [UnityEngine.Tooltip("Log warning messages")]
        public bool showLogWarning = false;
        /// <summary>Controls whether or not Error debug messages are logged.</summary>
        [UnityEngine.Tooltip("Log error messages")]
        public bool showLogError = false;
        /// <summary>Controls whether or not debug draws in scene are shown.</summary>
        [UnityEngine.Tooltip("Show debug draws in scene")]
        public bool showDebugDraws = false;

        /// <summary>Logs a Basic debug message to the console.</summary>
        /// <param name="message">Debug message string or object.</param>
        /// <param name="context">Object that this log refers to.</param>
        public void Log(object message, Object context = null)
        {
#if UNITY_EDITOR
            if (showLog)
                Debug.Log(message, context);
#endif
        }

        /// <summary>Logs a Warning debug message to the console.</summary>
        /// <param name="message">Warning message string or object.</param>
        /// <param name="context">Object that this warning refers to.</param>
        public void LogWarning(object message, Object context = null)
        {
#if UNITY_EDITOR
            if (showLogWarning)
                Debug.LogWarning(message, context);
#endif
        }

        /// <summary>Logs a Error debug message to the console.</summary>
        /// <param name="message">Error message string or object.</param>
        /// <param name="context">Object that this log refers to.</param>
        public void LogError(object message, Object context = null)
        {
#if UNITY_EDITOR
            if (showLogError)
                Debug.LogError(message, context);
#endif
        }

        /// <summary>Draws a debug line in scene view.</summary>
        /// <param name="start">Start point of the line.</param>
        /// <param name="end">End point of the line.</param>
        /// <param name="color">Color of the line.</param>
        /// <param name="duration">For how long should the line be visible.</param>
        /// <param name="depthTest">Should the line be obscured by any object closer to the camera?</param>
        public void DebugLine(Vector3 start, Vector3 end, Color color, float duration = -1, bool depthTest = false)
        {
#if UNITY_EDITOR
            if (!showDebugDraws)
                return;
            if (duration < 0)
            {
                Debug.DrawLine(start, end, color);
                return;
            }
            Debug.DrawLine(start, end, color, duration, depthTest);
#endif
        }
    }
}
