using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinder : MonoBehaviour
{
    #region Fields
    //public Vector2Int InitialPosition;
    public Vector2Int ObjectivePosition;
    public int SearchLength;
    protected List<TileLogic> tilesSearch;
    #endregion
    #region Unity Events
    /*[ContextMenu("Search")]
    void TriggerSearch(){
        Search(Board.GetTile(InitialPosition));
    }*/
    [ContextMenu("Print Path")]
    void TriggerPrintPath(){
        TileLogic objective = Board.GetTile(ObjectivePosition);
        if(tilesSearch.Contains(objective)){
            List<TileLogic> path = BuildPath(objective);
            PrintPath(path);
        }else{
            Debug.Log("Objective not found");
        }
    }
    #endregion
    #region Methods
    public abstract void Search(TileLogic start, TileLogic objective);
    public List<TileLogic> BuildPath(TileLogic lastTile){
        List<TileLogic> path = new List<TileLogic>();
        TileLogic temp = lastTile;
        while(temp.Previous!=null){
            path.Add(temp);
            temp = temp.Previous;
        }
        path.Add(temp);
        path.Reverse();
        return path;
    }
    void PrintPath(List<TileLogic> path){
        foreach(TileLogic t in path){
            Debug.Log(t.Position);
        }
    }
    #endregion
}
