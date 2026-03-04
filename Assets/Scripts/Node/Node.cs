using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform[] nextNodes;

    public Transform GetNextNode()
    {
        if (nextNodes.Length == 0) return null;
        return nextNodes[Random.Range(0, nextNodes.Length)];
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);

        if (nextNodes == null) return;

        Gizmos.color = Color.green;
        foreach (var n in nextNodes)
        {
            if (n != null)
                Gizmos.DrawLine(transform.position, n.position);
        }
    }
}
