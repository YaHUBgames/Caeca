using UnityEngine;


using Caeca.Interfaces;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca
{
    public class TestingScript1 : MonoBehaviour, ITestIntPassable
    {
        [SerializeField] private BoolSO boolSO;
        
        private void Start() {
            boolSO.ChangeVariable(true);
        }

        [ContextMenu("T"), UnityEngine.Tooltip("TT")]
        void ToggleLogger()
        {
            boolSO.ChangeVariable(!boolSO.value);
        }

        public int GetInt()
        {
            return 77;
        }
    }
}
