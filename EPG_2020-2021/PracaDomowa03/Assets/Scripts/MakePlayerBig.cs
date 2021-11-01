using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayerBig : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject.transform.localScale.x != 1 && col.gameObject.transform.localScale.y != 1)
        {
            col.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
