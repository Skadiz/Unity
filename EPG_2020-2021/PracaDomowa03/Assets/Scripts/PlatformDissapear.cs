using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDissapear : MonoBehaviour
{
    IEnumerator OnCollisionEnter2D(Collision2D col)
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
