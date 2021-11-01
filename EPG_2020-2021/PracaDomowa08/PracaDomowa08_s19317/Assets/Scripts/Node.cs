using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    private readonly List<Node> neighbors = new List<Node>();
    private int x, y;
    private bool walkable;
    private int gCost, hCost;
    private Node parent;
    public Node(int xPos,int yPos,bool isWalkable)
    {
        this.x = xPos;
        this.y = yPos;
        this.walkable = isWalkable;
    }
    public int XPosition
    {
        get { return this.x; }
        set { this.x = value; }
    }
    public int YPosition
    {
        get { return this.y; }
        set { this.y = value; }
    }

    public bool IsWalkable
    {
        get { return this.walkable; }
        set { this.walkable = value; }
    }
    public int HCost
    {
        get { return this.hCost; }
        set { this.hCost = value; }
    }

    public int GCost
    {
        get { return this.gCost; }
        set { this.gCost = value; }
    }

    public int FCost
    {
        get { return this.gCost + this.hCost; }
    }

    public Node Parent
    {
        get { return this.parent; }
        set { this.parent = value; }
    }
    public List<Node> GetNeighbors()
    {

        this.neighbors.Clear();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                Node neighbor = GridBehavior.Instance.GetNode(this.x + i, this.y + j);
                if (!neighbor.IsWalkable)
                    continue;
                this.neighbors.Add(neighbor);
            }
        }

        return this.neighbors;
    }
}
