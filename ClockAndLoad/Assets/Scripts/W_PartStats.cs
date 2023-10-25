using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_PartStats : MonoBehaviour
{
    public W_PartManager partStats;
    public List<GameObject> attachPoint;
    public float gizmoSize = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        foreach (GameObject go in attachPoint)
        {
            if (go != null)
            {
                Gizmos.DrawSphere(go.transform.position, gizmoSize);
            }
        }

    }
}
