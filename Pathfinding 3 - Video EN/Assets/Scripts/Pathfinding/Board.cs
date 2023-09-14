using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    #region Fields
    public static Board Instance;
    public static Vector2Int[] Directions = new Vector2Int[4]{
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    public Vector2Int MinSize;
    public Vector2Int MaxSize;
    public Dictionary<Vector2Int, TileLogic> Tiles;
    public float NodeSize;
    public bool DrawGizmos;
    public Transform ObstacleHolder;

    #endregion
    #region Unity Events
    void Awake(){
        Instance = this;
        Tiles = new Dictionary<Vector2Int, TileLogic>();
        CreateTileLogics();
        Debug.Log(Tiles.Count);
    }

    void OnDrawGizmos()
    {
        if(!DrawGizmos || Tiles == null || Tiles.Count == 0) return;
        Gizmos.color = Color.red;
        foreach(TileLogic t in Tiles.Values)
        {
            Gizmos.DrawCube(t.WorldPosition, Vector3.one * (NodeSize/2));
        }
    }
    #endregion
    #region Methods
    public static TileLogic GetTile(Vector2Int position){
        TileLogic tile;
        if(Instance.Tiles.TryGetValue(position, out tile))
            return tile;
        return null;
    }
    public void ClearSearch(){
        foreach(TileLogic t in Tiles.Values){
            t.CostFromOrigin = int.MaxValue;
            t.CostToObjective = int.MaxValue;
            t.Score = int.MaxValue;
            t.Previous = null;
        }
    }

    public TileLogic WorldPositionToTile(Vector3 pos)
    {
        Vector3 nodePosition = pos/NodeSize;
        Vector2Int pos2D = new Vector2Int(Mathf.RoundToInt(nodePosition.x), Mathf.RoundToInt(nodePosition.z));
        TileLogic toReturn = GetTile(pos2D);
        //Debug.LogFormat("From:{0}, to {1}, Tile:{2}", pos, pos2D, toReturn);
        return toReturn;
    }

    void CreateTileLogics(){
        for(int x=MinSize.x; x<MaxSize.x; x++){
            for(int y=MinSize.y; y<MaxSize.y; y++){
                TileLogic tile = new TileLogic();
                tile.Position = new Vector2Int(x, y);
                Tiles.Add(tile.Position, tile);
                SetTile(tile);
            }
        }
    }
    void SetTile(TileLogic tileLogic){
        tileLogic.MoveCost = 1;
        tileLogic.WorldPosition = new Vector3(tileLogic.Position.x, 0, tileLogic.Position.y) * NodeSize;
        CheckCollision(tileLogic);
        /*string tileType = Tilemap.GetTile(tileLogic.Position).name;
        switch(tileType){
            case "blockedTile":
                tileLogic.MoveCost = int.MaxValue;
                break;
            case "2":
                tileLogic.MoveCost = 2;
                break;
            case "3":
                tileLogic.MoveCost = 3;
                break;
            default:
                tileLogic.MoveCost = 1;
                break;
        }*/
    }

    void CheckCollision(TileLogic tileLogic)
    {
        Collider[] colliders = Physics.OverlapSphere(tileLogic.WorldPosition, NodeSize);
        foreach(Collider coll in colliders)
        {
            if(coll.transform.parent == ObstacleHolder)
            {
                //Debug.Log("Obstacle at " + tileLogic.WorldPosition);
                tileLogic.MoveCost = int.MaxValue; 
            }
        }
    }
    #endregion
}
