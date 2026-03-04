using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAgent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float brakeForce = 10f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float stopDistance = 3f;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform currentNode;

    private TrafficLightController currentTrafficLight;
    private Rigidbody rb;

    private float currentSpeed = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (currentNode == null) return;

        bool shouldStop = false;

        // Red light check
        if (currentTrafficLight != null &&
            currentTrafficLight.CurrentLightState() == LightState.Red)
        {
            shouldStop = true;
        }

        // Obstacle check
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f,
                            transform.forward,
                            out RaycastHit hit,
                            stopDistance,
                            obstacleMask))
        {
            shouldStop = true;
        }

        // Speed control
        if (shouldStop)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                brakeForce * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                maxSpeed,
                acceleration * Time.fixedDeltaTime);
        }

        // Apply movement
        rb.velocity = transform.forward * currentSpeed;

        // Rotate towards next node
        Vector3 dir = (currentNode.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation,
                                             targetRot,
                                             Time.deltaTime * turnSpeed);

        // Switch node if close
        if (Vector3.Distance(transform.position, currentNode.position) < 1f)
        {
            Node n = currentNode.GetComponent<Node>();
            if (n != null)
                currentNode = n.GetNextNode();
        }
    }

    public void SetTrafficLight(TrafficLightController light)
    {
        currentTrafficLight = light;
    }

    public Transform GetCurrentNode() => currentNode;
    public void SetCurrentNode(Transform newNode) { currentNode = newNode; }
}