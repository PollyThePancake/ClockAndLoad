using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_BulletController : MonoBehaviour
{
    public float shotSpeed;
    public Rigidbody2D rb;
    public void Move()
    {
        Destroy(this, 20f);
        rb.AddForce(transform.right * shotSpeed, ForceMode2D.Impulse);
    }
    
}
