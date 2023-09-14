using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public static Board Instance;
    public static Vector3Int[] Directions = new Vector3Int[4]
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left
    };
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] Vector3Int _size;
    [SerializeField] Dictionary<Vector3Int, TileLogic> _tiles;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _tiles = new Dictionary<Vector3Int, TileLogic>();
        CreateTileLogics();
    }

    public static TileLogic GetTile(Vector3Int pos)
    {
        TileLogic tile;
        if(Instance._tiles.TryGetValue(pos, out tile)) return tile;
        return null;
    }

    public void PaintTile(Vector3Int pos, Color color)
    {
        _tilemap.SetColor(pos, color);
    }

    public void ClearSearch()
    {
        foreach(TileLogic t in _tiles.Values)
        {
            _tilemap.SetColor(t.position, Color.white);
            t.costFromOrigin = int.MaxValue;
            t.costToObjective = int.MaxValue;
            t.score = int.MaxValue;
            t.previous = null;
        }
    }

    private void CreateTileLogics()
    {
        for(int x = 0; x < _size.x; x++)
            for(int y = 0; y < _size.y; y++)
            {
                TileLogic tile = new TileLogic();
                tile.position = new Vector3Int(x, y, 0);
                _tiles.Add(tile.position, tile);
                _tilemap.SetTileFlags(tile.position, TileFlags.None);
                SetTile(tile);
            }
    }

    void SetTile(TileLogic tile)
    {
        string tileType = _tilemap.GetTile(tile.position).name;
        switch(tileType)
        {
            case "blockedTile":
                //tile.occupied = true;
                tile.moveCost = int.MaxValue;
                break;
            case "squareTwo":
                tile.moveCost = 2;
                break;
            case "squareThree":
                tile.moveCost = 3;
                break;
            default:
                tile.moveCost = 1;
                break;
        }
    }
}
