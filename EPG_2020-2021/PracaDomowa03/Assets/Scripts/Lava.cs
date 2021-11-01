using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        PlayerMovement pm = col.GetComponent<PlayerMovement>();
        yield return new WaitForSeconds(1);
        pm.health -= 1;
        
    }
}
