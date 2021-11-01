using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridBehavior : MonoBehaviour
{

    private static GridBehavior instance;
    private readonly List<TileBehavior> tilesForReset = new List<TileBehavior>();
    private readonly Dictionary<Node, TileBehavior> tiles = new Dictionary<Node, TileBehavior>();
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private GameObject movementPrefab;
    private GameObject prefabInstance;
    private Node[,] grid;
    private Node startNode;
    private Node goalNode;
    public Node StartNode
    {
        get { return this.startNode; }
    }
    public Node GoalNode
    {
        get { return this.goalNode; }
    }
    public static GridBehavior Instance
    {
        get { return instance; }
    }
    public Dictionary<Node, TileBehavior> Tiles
    {
        get { return this.tiles; }
    }
    public Node[,] Grid
    {
        get { return this.grid; }
    }
    public Node GetNode(int x, int y)
    {
        return this.Grid[x, y];
    }

    void Start()
    {
        instance = this;
        this.SetUpGrid();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                TileBehavior tile = hit.collider.GetComponent<TileBehavior>();

                if (this.startNode == null)
                {
                    if (tile != null && tile.Node.IsWalkable)
                    {
                        this.startNode = tile.Node;
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
                        this.tilesForReset.Add(tile);
                    }
                }
                else if (this.goalNode == null)
                {
                    if (tile != null && tile.Node.IsWalkable && tile.Node != this.startNode)
                    {
                        this.goalNode = tile.Node;
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
                        this.tilesForReset.Add(tile);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                TileBehavior tile = hit.collider.GetComponent<TileBehavior>();

                if (tile.Node == this.startNode || tile.Node == this.goalNode)
                    return;
                tile.Node.IsWalkable = false;
                hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
                EventManager.Publish("CalculateNewPath");
                this.tilesForReset.Add(tile);
            }
        }
    }
    public void Spawn(GameObject spawnButton)
    {
        TileBehavior t;
        if (this.startNode == null || this.goalNode == null)
            return;
        if (this.tiles.TryGetValue(this.StartNode, out t))
        {
            this.prefabInstance = Instantiate(this.movementPrefab, t.transform.position, Quaternion.identity);
        }
        spawnButton.SetActive(false);
    }
    public void Clear(GameObject spawnbutton)
    {
        this.startNode = null;
        this.goalNode = null;

        Destroy(this.prefabInstance);
        foreach (TileBehavior tile in this.tilesForReset)
        {
            tile.Node.IsWalkable = true;
            tile.GetComponent<SpriteRenderer>().material.color = Color.white;
        }

        spawnbutton.SetActive(true);
    }

    void SetUpGrid()
    {
        Vector3 worldPos = Vector3.zero;
        bool walkable;
        this.grid = new Node[12, 12];

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                GameObject go = Instantiate(this.tilePrefab);
                go.transform.position = new Vector3(worldPos.x + i , worldPos.y - j , 0);
                walkable = (i % 12 == 0 || j % 12 == 0 || i == 12 - 1 || j == 12 - 1) ? false : true;
                Node node = new Node(i, j, walkable);
                TileBehavior tile = go.GetComponent<TileBehavior>();
                tile.Node = node;
                this.grid[i, j] = node;

                this.tiles.Add(node, tile);
            }
        }
    }
}
