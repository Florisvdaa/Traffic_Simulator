using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAgent : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float stopDistance = 3f;
    [SerializeField] private Transform currentNode;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (currentNode == null) return;

        Vector3 dir = (currentNode.position - transform.position).normalized;

        // check for obstacles
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out RaycastHit hit, stopDistance))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.1f);
            return;
        }

        // Move forward
        rb.velocity = transform.forward * speed;

        // Rotate towards next node
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);

        // If close to node -> pick next
        if (Vector3.Distance(transform.position, currentNode.position) < 1f)
        {
            Node n = currentNode.GetComponent<Node>();
            if (n != null)
                currentNode = n.GetNextNode();
        }
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public Transform GetCurrentNode() => currentNode;
    public void SetCurrentNode(Transform newNode) { currentNode = newNode; }
}
