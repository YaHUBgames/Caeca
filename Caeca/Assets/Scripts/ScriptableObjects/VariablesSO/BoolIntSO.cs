using UnityEngine;

namespace Caeca.ScriptableObjects
{
    /// <summary>Scriptable object (bool, int) variable with event synchronization.</summary>
    [System.Serializable, CreateAssetMenu(menuName = "Scriptable Objects/.Variable_SO/Bool_Int_SO")]
    public class BoolIntSO : VariableSO<bool, int> { }
}