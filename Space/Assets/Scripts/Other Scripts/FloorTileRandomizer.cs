using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorTileRandomizer : MonoBehaviour
{
    Tilemap tilemap;
    public TileBase[] randomTiles;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        RandomizeTiles();
    }

    void RandomizeTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                TileBase randomTile = randomTiles[Random.Range(0, randomTiles.Length)];
                tilemap.SetTile(pos, randomTile);
            }
        }
    }
}