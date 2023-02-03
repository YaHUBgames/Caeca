using UnityEngine;

namespace Caeca.TerrainControl
{
    public enum GroundTypes
    {
        NOTHING = -4, NO_GROUND = -2, DEFAULT_GROUND = -1,
        DIRT, GRASS, GRAVEL, LEAVES, METAL, MUD, SAND, STONE, WATER, WOOD,
        SIZEOF
    };  //must be in alphabetical order

    public class TerrainManager : MonoBehaviour
    {
        public static TerrainManager instance { private set; get; }

        private void Awake()
        {
            if (TerrainManager.instance is not null)
            {
                Destroy(this);
                return;
            }
            TerrainManager.instance = this;
        }

        public static Vector2Int GetAlphaPositionOnTerrain(Vector3 _worldPosition, Terrain _terrain)
        {
            Vector2Int alphaPosition = Vector2Int.zero;
            TerrainData terrainData = _terrain.terrainData;

            Vector3 terrainPosition = _worldPosition - _terrain.transform.position;

            Vector3 mapPosition = new Vector3(terrainPosition.x / terrainData.size.x, 0,
                terrainPosition.z / terrainData.size.z);

            alphaPosition.x = (int)((float)(mapPosition.x * terrainData.alphamapWidth));
            alphaPosition.y = (int)((float)(mapPosition.z * terrainData.alphamapHeight));
            return alphaPosition;
        }

        public static float[] GetTerrainTextureValuesOnPosition(Vector3 _worldPosition, Terrain _terrain)
        {
            Vector2Int alphaPosition = TerrainManager.GetAlphaPositionOnTerrain(_worldPosition, _terrain);

            float[,,] alphaMap = _terrain.terrainData.GetAlphamaps(alphaPosition.x, alphaPosition.y, 1, 1);

            float[] textureValues = new float[(int)GroundTypes.SIZEOF];
            for (int i = 0; i < (int)GroundTypes.SIZEOF; i++)
                textureValues[i] = alphaMap[0, 0, i];

            return textureValues;
        }

        public static float GetTerrainTextureValueOfType(GroundTypes _groundType, float[] _textureValues)
        {
            return _textureValues[((int)_groundType)];
        }

        public static float GetGroundValueOfType(GroundTypes _groundType, float[] _textureValues, bool _standingOnTerrain ,Transform _standingOnObject)
        {
            float returnValue = 0f;
            
            if (_standingOnTerrain)
            {
                returnValue = GetTerrainTextureValueOfType(_groundType, _textureValues);
                return returnValue;
            }
            
            if (_groundType == GroundTypes.WOOD && _standingOnObject.CompareTag("Wooden"))
                returnValue = 1f;
            if (_groundType == GroundTypes.STONE && _standingOnObject.CompareTag("Stone"))
                returnValue = 1f;
            if (_groundType == GroundTypes.METAL && _standingOnObject.CompareTag("Metalic"))
                returnValue = 1f;
            
            return returnValue;
        }
    }
}
