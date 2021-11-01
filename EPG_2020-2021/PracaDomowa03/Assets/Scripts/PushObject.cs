using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        col.attachedRigidbody.AddForce(new Vector2(0, 1000));
    }
}
