using UnityEngine;

namespace Caeca.TerrainControl
{
    /// <summary>
    /// Gets ground terrain or object and iths ground values.
    /// </summary>
    public class GroundedChecker : MonoBehaviour
    {
        [Header("Ground check settings")]
        [SerializeField] float distanceFromGround;
        [SerializeField] LayerMask groundMask;

        [HideInInspector]
        public bool isGrounded { private set; get; } = false;
        [HideInInspector]
        public bool isOnTerrain { private set; get; } = false;

        private Transform standingOn = null;
        private float[] groundValues = new float[(int)GroundTypes.SIZEOF];

        public bool GetGround()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceFromGround, groundMask))
            {
                standingOn = hit.transform;
                isGrounded = true;
                isOnTerrain = standingOn.CompareTag("Terrain");

                if (isOnTerrain)
                    groundValues = TerrainTextureManager.GetTerrainTextureValuesOnPosition(hit.point, standingOn.GetComponent<Terrain>());

                return true;
            }
            isGrounded = false;
            return false;
        }

        public float GetGroundValueOfType(GroundTypes _groundType)
        {
            if (isGrounded)
                return TerrainTextureManager.GetGroundValueOfType(_groundType, groundValues, isOnTerrain, standingOn);
            return 0;
        }
    }
}
