using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilePerlinNoiseScript : MonoBehaviour
{
    [Header("Perlin Settings")]
    public float magnifcation = 1f;
    public int x_offset = 0;
    public int y_offset = 0;
    public int noiseSeed = 0;
    public bool randomizeNoiseOnGenerate = true;


    [Header("Bounds Settings")]
    public bool replaceAllTiles;
    private bool _replaceAllTiles;
    public bool useMask;
    private bool _useMask;
    public TileBase maskTile = null;


    [Header("Tile Distribution Settings")]
    public List<TileSettings> tileSettings;
    private Tilemap tilemap;


    [Serializable]
    public struct TileSettings
    {
        public TileBase tile;
        public Vector2 perlinRange;
    }

    private struct TileInfo
    {
        public TileInfo(int x, int y, TileBase tile)
        {
            this.x = x;
            this.y = y;
            this.tile = tile;
        }

        public int x;
        public int y;
        public TileBase tile;

        public override string ToString()
        {
            return $"({x}, {y}): {tile.name}";
        }
    }


    public void OnValidate()
    {
        if (replaceAllTiles != _replaceAllTiles) //replaceAllTiles toggled
        {
            _replaceAllTiles = replaceAllTiles;
            _useMask = !replaceAllTiles;
            useMask = !replaceAllTiles;
        }
        if (useMask != _useMask) //replaceAllTiles toggled
        {
            _useMask = useMask;
            _replaceAllTiles = !useMask;
            replaceAllTiles = !useMask;
        }
    }


    public void GenerateTilemap()
    {
        tilemap = GetComponent<Tilemap>();
        List<TileInfo> maskedTiles = GenerateMask();
        ApplyPerlinNoise(maskedTiles);

    }

    private void ApplyPerlinNoise(List<TileInfo> maskedTiles)
    {
        if (randomizeNoiseOnGenerate)
        {
            noiseSeed = UnityEngine.Random.Range(0, 100000);
        }

        foreach(TileInfo oldTile in maskedTiles)
        {
            float raw_perlin = Mathf.PerlinNoise(
                (oldTile.x - x_offset + noiseSeed) / magnifcation,
                (oldTile.y - y_offset + noiseSeed) / magnifcation                
            );

            float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
            AssignTile(oldTile, clamp_perlin);
        }
    }

    private void AssignTile(TileInfo oldTile, float perlin)
    {
        TileBase newTile = null;
        foreach(TileSettings tileSetting in tileSettings)
        {
            if (perlin >= tileSetting.perlinRange.x && perlin < tileSetting.perlinRange.y)
            {
                newTile = tileSetting.tile;
            }
        }
        Vector3Int position = new Vector3Int(oldTile.x, oldTile.y, 0) + tilemap.cellBounds.position;

        tilemap.SetTile(position, newTile);
    }

    private List<TileInfo> GenerateMask()
    {
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;

        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        List<TileInfo> maskedTiles = new();

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    if (useMask)
                    {
                        if (tile == maskTile) maskedTiles.Add(new TileInfo(x, y, tile));
                    }
                    else
                    {
                        maskedTiles.Add(new TileInfo(x, y, tile));
                    }
                }
            }
        }
        return maskedTiles;
    }
}
