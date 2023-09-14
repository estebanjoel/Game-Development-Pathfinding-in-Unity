using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : Pathfinder
{
    [SerializeField] float _heuristicModifier;
    protected override IEnumerator Search(TileLogic start)
    {
        //Seteo los valores iniciales para el algoritmo. Una lista para buscar los tiles y dos colas para chequear ahora y chequear despues
        tileSearch = new List<TileLogic>();
        int iterationCount = 0;

        tileSearch.Add(start);
        Board.Instance.ClearSearch();

        TileLogic objective = Board.GetTile(objectivePosition);
        TileLogic current;

        List<TileLogic> openSet = new List<TileLogic>();
        openSet.Add(start);
        start.costFromOrigin = 0;

        while(openSet.Count > 0)
        {
            openSet.Sort((x, y)=> x.score.CompareTo(y.score));
            current = openSet[0];
            Board.Instance.PaintTile(current.position, Color.green);

            if(current == objective)
            {
                Debug.Log($"Objective Found!");
                break;
            } 
            
            openSet.RemoveAt(0);
            tileSearch.Add(current);
            for(int i = 0 ; i < Board.Directions.Length; i++)
            {
                TileLogic next = Board.GetTile(current.position + Board.Directions[i]);
                yield return new WaitForSeconds(0.025f);
                iterationCount++;

                if(next == null || next.costFromOrigin <= current.costFromOrigin + next.moveCost) continue;
                next.costFromOrigin = current.costFromOrigin + next.moveCost;
                
                if(ValidateMovement(current, next))
                {
                    next.previous = current;
                    next.costToObjective = Vector3Int.Distance(next.position, objective.position) * _heuristicModifier;
                    next.score = next.costFromOrigin + next.costToObjective;

                    if(!tileSearch.Contains(next)) openSet.Add(next);
                    //Los tiles que chequea despues se pintan de amarillo
                    Board.Instance.PaintTile(next.position, Color.yellow);
                }
            }
        }
    }
}
