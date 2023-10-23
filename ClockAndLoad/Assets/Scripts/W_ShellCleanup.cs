using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_ShellCleanup : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject,5f);
    }
}
