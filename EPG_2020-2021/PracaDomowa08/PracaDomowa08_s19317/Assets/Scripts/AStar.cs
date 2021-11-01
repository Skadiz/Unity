using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class AStar
{
   
    public static List<Node> RunAStar(Node start, Node goal)
    {
        List<Node> finalPath = new List<Node>();
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        Node currentNode;
        openList.Add(start);
        while (openList.Count > 0)
        {
            List<Node> sorted = openList.OrderByDescending(node => node.FCost).ToList();
            sorted.Reverse();
            if (openList.Contains(goal))
            {
                currentNode = goal;
                finalPath.Add(currentNode);
                while (currentNode != start)
                {
                    finalPath.Add(currentNode.Parent);
                    currentNode = currentNode.Parent;
                }
                break;
            }

            currentNode = sorted[0];
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            List<Node> neighbors = currentNode.GetNeighbors();
            foreach (Node node in neighbors)
            {
                if (closedList.Contains(node))
                    continue;

                //H Koszt
                int x = Math.Abs(goal.XPosition - node.XPosition);
                int y = Math.Abs(goal.YPosition - node.YPosition);

                node.HCost = (x + y) * 100;

                int xDiff = node.XPosition - currentNode.XPosition;
                int yDiff = node.YPosition - currentNode.YPosition;

               
                int moveCost = (xDiff == 0 || yDiff == 0) ? 100 : 141;

                moveCost += currentNode.GCost;

               
                if (moveCost < node.GCost || !openList.Contains(node))
                {
                    node.GCost = moveCost;
                    node.Parent = currentNode;
                    if (!openList.Contains(node))
                        openList.Add(node);
                }
            }
        }

        return finalPath;
    }
}
