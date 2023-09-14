using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinder : MonoBehaviour
{
    public Vector3Int initialPosition;
    public Vector3Int objectivePosition;
    public int searchLength;
    protected List<TileLogic> tileSearch;
    
    [ContextMenu("Search")]
    void TriggerSearch()
    {
        
        StartCoroutine(Search(Board.GetTile(initialPosition)));
    }

    [ContextMenu("Print Path")]
    void TriggerPrintPath()
    {
        TileLogic objective = Board.GetTile(objectivePosition);
        if(tileSearch.Contains(objective))
        {
            List<TileLogic> path = BuildPath(objective);
            PrintPath(path);
        }
        else Debug.Log("Objective not found.");
    }

    private void PrintPath(List<TileLogic> path)
    {
        foreach(TileLogic t in path)
        {
            Debug.Log(t.position);
        }
    }

    private List<TileLogic> BuildPath(TileLogic lastTile)
    {
        List<TileLogic> path = new List<TileLogic>();
        TileLogic temp = lastTile;
        while(temp.previous != null)
        {
            path.Add(temp);
            temp = temp.previous;
        }
        path.Add(temp);
        path.Reverse();
        return path;
    }

    protected bool ValidateMovement(TileLogic from, TileLogic to)
    {
        if(to.costFromOrigin > searchLength) return false;
        else return true;
    }

    protected abstract IEnumerator Search(TileLogic start);
    
}
