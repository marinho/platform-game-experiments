using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{

    /// <summary>
    /// Terrain Tiles, similar to Pipeline Tiles, are tiles which take into consideration its orthogonal and diagonal neighboring tiles and displays a sprite depending on whether the neighboring tile is the same tile.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New Random Platform Tile", menuName = "2D Extras/Tiles/Random Platform Tile", order = 361)]
    public class RandomPlatformTile : TileBase
    {
        [SerializeField] public Sprite[] Single;
        [SerializeField] public Sprite[] LeftEnd;
        [SerializeField] public Sprite[] Middle;
        [SerializeField] public Sprite[] RightEnd;

        /// <summary>
        /// This method is called when the tile is refreshed.
        /// </summary>
        /// <param name="location">Position of the Tile on the Tilemap.</param>
        /// <param name="tileMap">The Tilemap the tile is present on.</param>
        public override void RefreshTile(Vector3Int location, ITilemap tileMap)
        {
            for (int yd = -1; yd <= 1; yd++)
                for (int xd = -1; xd <= 1; xd++)
                {
                    Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                    if (TileValue(tileMap, position))
                        tileMap.RefreshTile(position);
                }
        }

        /// <summary>
        /// Retrieves any tile rendering data from the scripted tile.
        /// </summary>
        /// <param name="position">Position of the Tile on the Tilemap.</param>
        /// <param name="tilemap">The Tilemap the tile is present on.</param>
        /// <param name="tileData">Data to render the tile.</param>
        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            UpdateTile(location, tileMap, ref tileData);
        }

        private void UpdateTile(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;

            int mask = TileValue(tileMap, location + new Vector3Int(0, 1, 0)) ? 1 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(1, 1, 0)) ? 2 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(1, 0, 0)) ? 4 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(1, -1, 0)) ? 8 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(0, -1, 0)) ? 16 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(-1, -1, 0)) ? 32 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(-1, 0, 0)) ? 64 : 0;
            mask += TileValue(tileMap, location + new Vector3Int(-1, 1, 0)) ? 128 : 0;

            byte original = (byte)mask;
            if ((original | 254) < 255) { mask = mask & 125; }
            if ((original | 251) < 255) { mask = mask & 245; }
            if ((original | 239) < 255) { mask = mask & 215; }
            if ((original | 191) < 255) { mask = mask & 95; }

            Sprite[] sprites = GetSpriteList((byte)mask);

            if (TileValue(tileMap, location))
            {
                tileData.sprite = GetRandomSprite(sprites);
                tileData.transform = GetTransform((byte)mask);
                tileData.color = Color.white;
                tileData.flags = TileFlags.LockTransform | TileFlags.LockColor;
                tileData.colliderType = Tile.ColliderType.Sprite;
            }
        }

        private Sprite GetRandomSprite(Sprite[] spriteList) {
            if (spriteList.Length == 0) {
                return null;
            }

            int spriteIndex = Random.Range(0, spriteList.Length - 1);
            return spriteList[spriteIndex];
        }

        private bool TileValue(ITilemap tileMap, Vector3Int position)
        {
            TileBase tile = tileMap.GetTile(position);
            return (tile != null && tile == this);
        }

        private Sprite[] GetSpriteList(byte mask)
        {
            switch (mask)
            {
                case 0: return Single;
                case 1:
                case 4: return LeftEnd;
                case 18:
                case 64: return RightEnd;
                default: return Middle;
            }
        }

        private Matrix4x4 GetTransform(byte mask)
        {
            return Matrix4x4.identity;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RandomPlatformTile))]
    public class RandomPlatformTileEditor : Editor
    {
        private RandomPlatformTile tile { get { return (target as RandomPlatformTile); } }

        public void OnEnable()
        {
            bool setDirty = false;
            
            if (tile.Single == null || tile.Single.Length == 0) {
                tile.Single = new Sprite[1]; setDirty = true;
            }

            if (tile.LeftEnd == null || tile.LeftEnd.Length == 0) {
                tile.LeftEnd = new Sprite[1]; setDirty = true;
            }

            if (tile.RightEnd == null || tile.RightEnd.Length == 0) {
                tile.RightEnd = new Sprite[1]; setDirty = true;
            }

            if (tile.Middle == null || tile.Middle.Length == 0) {
                tile.Middle = new Sprite[1]; setDirty = true;
            }

            if (setDirty) {
                EditorUtility.SetDirty(tile);
            }
        }

        // public void OnEnable()
        // {
        //     if (tile.m_Sprites == null || tile.m_Sprites.Length != 15)
        //     {
        //         tile.m_Sprites = new Sprite[15];
        //         EditorUtility.SetDirty(tile);
        //     }
        // }


        // public override void OnInspectorGUI()
        // {
        //     EditorGUILayout.LabelField("Place sprites shown based on the contents of the sprite.");
        //     EditorGUILayout.Space();

        //     float oldLabelWidth = EditorGUIUtility.labelWidth;
        //     EditorGUIUtility.labelWidth = 210;

        //     EditorGUI.BeginChangeCheck();
        //     tile.m_Sprites[0] = (Sprite) EditorGUILayout.ObjectField("Filled", tile.m_Sprites[0], typeof(Sprite), false, null);
        //     tile.m_Sprites[1] = (Sprite) EditorGUILayout.ObjectField("Three Sides", tile.m_Sprites[1], typeof(Sprite), false, null);
        //     tile.m_Sprites[2] = (Sprite) EditorGUILayout.ObjectField("Two Sides and One Corner", tile.m_Sprites[2], typeof(Sprite), false, null);
        //     tile.m_Sprites[3] = (Sprite) EditorGUILayout.ObjectField("Two Adjacent Sides", tile.m_Sprites[3], typeof(Sprite), false, null);
        //     tile.m_Sprites[4] = (Sprite) EditorGUILayout.ObjectField("Two Opposite Sides", tile.m_Sprites[4], typeof(Sprite), false, null);
        //     tile.m_Sprites[5] = (Sprite) EditorGUILayout.ObjectField("One Side and Two Corners", tile.m_Sprites[5], typeof(Sprite), false, null);
        //     tile.m_Sprites[6] = (Sprite) EditorGUILayout.ObjectField("One Side and One Lower Corner", tile.m_Sprites[6], typeof(Sprite), false, null);
        //     tile.m_Sprites[7] = (Sprite) EditorGUILayout.ObjectField("One Side and One Upper Corner", tile.m_Sprites[7], typeof(Sprite), false, null);
        //     tile.m_Sprites[8] = (Sprite) EditorGUILayout.ObjectField("One Side", tile.m_Sprites[8], typeof(Sprite), false, null);
        //     tile.m_Sprites[9] = (Sprite) EditorGUILayout.ObjectField("Four Corners", tile.m_Sprites[9], typeof(Sprite), false, null);
        //     tile.m_Sprites[10] = (Sprite) EditorGUILayout.ObjectField("Three Corners", tile.m_Sprites[10], typeof(Sprite), false, null);
        //     tile.m_Sprites[11] = (Sprite) EditorGUILayout.ObjectField("Two Adjacent Corners", tile.m_Sprites[11], typeof(Sprite), false, null);
        //     tile.m_Sprites[12] = (Sprite) EditorGUILayout.ObjectField("Two Opposite Corners", tile.m_Sprites[12], typeof(Sprite), false, null);
        //     tile.m_Sprites[13] = (Sprite) EditorGUILayout.ObjectField("One Corner", tile.m_Sprites[13], typeof(Sprite), false, null);
        //     tile.m_Sprites[14] = (Sprite) EditorGUILayout.ObjectField("Empty", tile.m_Sprites[14], typeof(Sprite), false, null);
        //     if (EditorGUI.EndChangeCheck())
        //         EditorUtility.SetDirty(tile);

        //     EditorGUIUtility.labelWidth = oldLabelWidth;
        // }
    }
#endif
}
