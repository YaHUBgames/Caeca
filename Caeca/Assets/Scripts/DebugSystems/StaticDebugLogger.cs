using UnityEngine;

namespace Caeca.DebugSystems
{
    public class StaticDebugLogger
    {
        public static DebugLogger logger;

        static StaticDebugLogger()
        {
            logger = (DebugLogger)ScriptableObject.CreateInstance(typeof(DebugLogger));
            logger.ShowAllLogs();
        }
    }
}