using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileBehavior : MonoBehaviour
{

    private Node node;

    public Node Node
    {
        get { return this.node; }
        set { this.node = value; }
    }

    void Start()
    {
        if(!this.node.IsWalkable)
        {
            this.gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
        }
    }
}
