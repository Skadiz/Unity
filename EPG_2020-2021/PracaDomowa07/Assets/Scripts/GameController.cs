using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // 1. Gdzie znajduje się gracz?
    // 2. Czy na polu (x, y) znajduje się skrzynia?
    // 3. Spróbuj przesunąć gracza w kierunku d

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject targetPrefab;
    public GameObject playerPrefab;
    public GameObject cratePrefab;

    public Text text;
    private int steps = 0;

    public List<Level> levels;
    public int currentLevel = 0;

    Camera cam;

    GameObject player;
    private GameObject[,] crates;
    private GameObject[,] targets;
    private bool[,] walls;
    private Vector2Int playerPosition;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;

        levels = new List<Level>();
        levels.Add(new Level());      

        LoadLevel(0);
    }

    // Update is called once per frame
    void Update() {
        
    }

    //po wykonianiu LoadLevel(idx) gra znajduje się w stanie początkowym poziomu o indeksie idx
    private void LoadLevel(int idx) {
        currentLevel = idx;
        Level lev = levels[currentLevel];

        targets = new GameObject[lev.width, lev.height];
        crates = new GameObject[lev.width, lev.height];
        walls = new bool[lev.width, lev.height];
    
        for (int row = 0; row < lev.height; row++) {
            for (int col = 0; col < lev.levelLayout[row].Count; col++) {
                InstantiateField(row, col);
            }
        }
        CenterCameraOn(lev.width / 2, lev.height / 2);
    }

    private void InstantiateField(int row, int col) {
        Field field = levels[currentLevel].levelLayout[row][col];

        Debug.Log(field);

        if (field.floorType == FloorType.Floor)
        {
            switch (field.entity)
            {
                case Entity.Player:
                    Instantiate(floorPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    player = Instantiate(playerPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    player.GetComponent<Player>().gameController = this;
                    break;
                case Entity.Crate:
                    Instantiate(floorPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    crates[col,row] = Instantiate(cratePrefab, new Vector3(col, row, 0), Quaternion.identity);
                    
                    break;
                case Entity.None:
                    Instantiate(floorPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    break;
            }
        }
        else
        {
            switch (field.floorType)
            {          
                case FloorType.Target:
                    targets[col,row] = Instantiate(targetPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    break;
                case FloorType.Wall:
                    Instantiate(wallPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    walls[col,row] = true;
                    break;
            }
        }      
    }

    private void CenterCameraOn(float x, float y) {
        cam.transform.position = new Vector3(x,y,-12);
        cam.orthographicSize = (y+x)/2;
    }

    public bool TryMovePlayer(Vector2Int direction) {
        playerPosition = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y);
        Vector2Int aimCell = new Vector2Int(playerPosition.x + direction.x, playerPosition.y + direction.y);


        if (crates[aimCell.x, aimCell.y] != null)
        {
            if (TryMoveCrate(aimCell, direction))
            {
                playerPosition.x += direction.x;
                playerPosition.y += direction.y;
                player.transform.position = new Vector3(playerPosition.x, playerPosition.y);
                if (direction != Vector2Int.zero)
                {
                    steps++;
                    text.text = steps.ToString();
                }

            }

        }
        else if (!walls[playerPosition.x + direction.x, playerPosition.y + direction.y])
        {

            playerPosition.x += direction.x;
            playerPosition.y += direction.y;
            player.transform.position = new Vector3(playerPosition.x, playerPosition.y);
            if (direction != Vector2Int.zero)
            {
                steps++;
                text.text = steps.ToString();
            }
        }
        return true;
    }


    private void WinCheck()
    {
        int bound0 = crates.GetUpperBound(0);
        int bound1 = crates.GetUpperBound(1);
        // ... Loop over bounds.
        for (int i = 0; i <= bound0; i++)
        {
            for (int j = 0; j <= bound1; j++)
            {
                
                if((crates[i,j] != null) == (targets[i,j] != null))
                {
                   
                }
                else
                {
                    Debug.Log("Loose");
                    return;
                }
            }
            
        }
        if (currentLevel < levels.Count-1)
        {
            Destroy(player);
            for (int i = 0; i <= bound0; i++)
            {
                for (int j = 0; j <= bound1; j++)
                {

                    if ((crates[i, j] != null))
                    {
                        Destroy(crates[i,j]);
                    }
                    if ((targets[i, j] != null))
                    {
                        Destroy(targets[i,j]);
                    }
                   
                }

            }

            LoadLevel(++currentLevel);
            
        }
        else
        {
            Application.Quit();
        }
        text.text = "0";
        steps = 0;
        Debug.Log("Win");
    }


    private bool TryMoveCrate(Vector2Int pos, Vector2Int dir) {
        
        if (!walls[pos.x + dir.x, pos.y + dir.y] && crates[pos.x + dir.x, pos.y + dir.y] == null)
        {

            crates[pos.x + dir.x, pos.y + dir.y] = crates[pos.x, pos.y];
            crates[pos.x, pos.y] = null;
            pos.x += dir.x;
            pos.y += dir.y;

            crates[pos.x,pos.y].transform.position = new Vector3(pos.x, pos.y);
            WinCheck();
            return true;
        }

        return false;
    }
}
