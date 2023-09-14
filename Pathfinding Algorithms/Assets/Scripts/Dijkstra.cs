using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dijkstra : Pathfinder
{
    protected override IEnumerator Search(TileLogic start)
    {
        //Seteo los valores iniciales para el algoritmo. Una lista para buscar los tiles y dos colas para chequear ahora y chequear despues
        tileSearch = new List<TileLogic>();
        int iterationCount = 0;

        tileSearch.Add(start);
        Board.Instance.ClearSearch();
        Queue<TileLogic> checkNow = new Queue<TileLogic>();
        Queue<TileLogic> checkNext = new Queue<TileLogic>();

        start.costFromOrigin = 0;
        checkNow.Enqueue(start);

        while(checkNow.Count > 0)
        {
            TileLogic tile = checkNow.Dequeue();
            //Los tiles que chequea ahora se pintan de verde
            Board.Instance.PaintTile(tile.position, Color.green);
            for(int i = 0; i < Board.Directions.Length; i++)
            {
                TileLogic next = Board.GetTile(tile.position + Board.Directions[i]);
                yield return new WaitForSeconds(0.025f);
                iterationCount++;
                
                if(next == null || next.costFromOrigin <= tile.costFromOrigin + next.moveCost) continue;
                next.costFromOrigin = tile.costFromOrigin + next.moveCost;
                
                if(ValidateMovement(tile, next))
                {
                    checkNext.Enqueue(next);
                    next.previous = tile;
                    tileSearch.Add(next);
                    //Los tiles que chequea despues se pintan de amarillo
                    Board.Instance.PaintTile(next.position, Color.yellow);
                }
            }

            //Una vez que la cola checkNow está vacía cambia las referencias para terminar de pintar de verde los tiles en amarillo
            if(checkNow.Count == 0)
            {
                SwapReferences(ref checkNow, ref checkNext);
            }
        }
        Debug.Log("Iterations: " + iterationCount);
    }

    private void SwapReferences(ref Queue<TileLogic> checkNow, ref Queue<TileLogic> checkNext)
    {
        Queue<TileLogic> temp = checkNow;
        checkNow = checkNext;
        checkNext = temp;
    }
}
