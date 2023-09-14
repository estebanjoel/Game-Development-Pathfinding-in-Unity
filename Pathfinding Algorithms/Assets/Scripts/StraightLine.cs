using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLine : MonoBehaviour
{
    public Vector3Int initialPosition;
    public Vector3Int targetPosition;
    [ContextMenu("Search")]
    void Search()
    {
        int xMovement = targetPosition.x > initialPosition.x ? 1 : -1;
        int yMovement = targetPosition.y > initialPosition.y ? 1 : -1;

        Vector3Int currentPosition = initialPosition;
        Board.Instance.PaintTile(currentPosition, Color.blue);
        while(currentPosition.x != targetPosition.x)
        {
            currentPosition.x += xMovement;
            Board.Instance.PaintTile(currentPosition, Color.green);
        }
        while(currentPosition.y != targetPosition.y)
        {
            currentPosition.y += yMovement;
            Board.Instance.PaintTile(currentPosition, Color.green);
        }
    }
}
