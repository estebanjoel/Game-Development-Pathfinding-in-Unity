using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tile _redSquare;
    [SerializeField] private Vector3Int _position;
    [SerializeField] private Tilemap _tilemap;
    [ContextMenu("Paint")] 
    void Paint()
    {
        //_tilemap.SetTile(_position, _redSquare);
        _tilemap.SetTileFlags(_position, TileFlags.None);
        _tilemap.SetColor(_position, Color.blue);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
