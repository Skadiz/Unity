using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gameController;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        Vector2Int direction = new Vector2Int(0,0);
        if (Input.GetKeyDown(KeyCode.A))
            direction = new Vector2Int(-1, 0);
        else if (Input.GetKeyDown(KeyCode.D))
            direction = new Vector2Int(1, 0);
        else if (Input.GetKeyDown(KeyCode.W))
            direction = new Vector2Int(0, 1);
        else if (Input.GetKeyDown(KeyCode.S))
            direction = new Vector2Int(0, -1);
        // ustaw wartosc direction na jedno z (1, 0), (-1, 0), (0, 1), (0, -1) w zaleznosci od wcisnietych klawiszy
        if (gameController != null)
        gameController.TryMovePlayer(direction);
    }
}
