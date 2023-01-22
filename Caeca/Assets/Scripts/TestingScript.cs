using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca
{
    public class TestingScript : MonoBehaviour
    {
        [SerializeField] private InterfaceObject<ITestIntPassable> testInterface;
        [SerializeField] private DebugLogger debugLogger;

        [SerializeField] private BoolSO boolSO;

        private void OnValidate()
        {
            testInterface.OnValidate();
        }

        private void Awake() {
            boolSO.OnVarSync += ShowBool;
        }

        private void OnDestroy() {
            boolSO.OnVarSync -= ShowBool;
        }

        private void Update()
        {
            //debugLogger.Log(testInterface.intfs.GetInt());
        }

        public void ShowBool(bool b){
            debugLogger.Log(b);
        }
    }

    public interface ITestIntPassable
    {
        public int GetInt();
    }
}
