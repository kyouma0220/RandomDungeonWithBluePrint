using UnityEngine;
using UnityEngine.Tilemaps;

namespace RandomDungeonWithBluePrint
{
    public class FieldView : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap = default;
        [SerializeField] private Tile floorTile = default;
        [SerializeField] private Tile wallTile = default;

        public void ShowField(Field field)
        {
            tilemap.ClearAllTiles();

            for (var x = 0; x < field.Grid.Size.x; x++)
            {
                for (var y = 0; y < field.Grid.Size.y; y++)
                {
                    var tile = field.Grid[x, y] == (int) Constants.MapChipType.Wall ? wallTile : floorTile;
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }
}
