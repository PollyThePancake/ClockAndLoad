using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_ProjectileCollision : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StopProjectiles"))
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
