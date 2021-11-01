using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Agent : MonoBehaviour
{

    private Stack<TileBehavior> path;
    private Vector3 destination;
    private TileBehavior currentTile;
    private TileBehavior previousTile;
    void Start()
    {
        EventManager.Subscribe("CalculateNewPath", this.CalculateNewPath);
        this.path = new Stack<TileBehavior>();
        List<Node> finalPath = AStar.RunAStar(GridBehavior.Instance.StartNode, GridBehavior.Instance.GoalNode);
        this.destination = this.transform.position;
        if (finalPath.Count == 0)
            return;
        TileBehavior tile;

        foreach (Node node in finalPath)
        {
            if (GridBehavior.Instance.Tiles.TryGetValue(node, out tile))
            {
                this.path.Push(tile);
            }
        }

        if (this.path.Count > 0)
        {
            this.currentTile = this.path.Pop();

            this.destination = this.currentTile.transform.position;
        }
    }


    void Update()
    {
        if (this.transform.position != this.destination)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.destination, 2 * Time.deltaTime);
        }
        else
        {
            if (this.path.Count > 0)
            {
                TileBehavior tile = this.path.Pop();

                if (tile.Node.IsWalkable)
                {
                    this.previousTile = this.currentTile;

                    this.currentTile = tile;

                    this.destination = this.currentTile.transform.position;
                }

            }
        }
    }

    private void OnDestroy()
    {
        EventManager.UnSubscribe("CalculateNewPath", this.CalculateNewPath);
    }


    private void CalculateNewPath()
    {
        List<Node> finalPath = new List<Node>();
        this.path = new Stack<TileBehavior>();
        this.destination = this.transform.position;
        if (this.currentTile != null)
        {           
            Node node = (!this.currentTile.Node.IsWalkable) ? this.previousTile.Node : this.currentTile.Node;
            finalPath = AStar.RunAStar(node, GridBehavior.Instance.GoalNode);
            TileBehavior tile;
            if (finalPath.Count == 0)
                return;
            foreach (Node n in finalPath)
            {
                if (GridBehavior.Instance.Tiles.TryGetValue(n, out tile))
                {
                    this.path.Push(tile);
                }
            }
            if (this.path.Count > 0)
            {
                this.currentTile = this.path.Pop();
                this.destination = this.currentTile.transform.position;
            }
        }
    }
}
