using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForTrafficLight : MonoBehaviour
{
    public TrafficLightController trafficLight; // This can be cleaner and better. (how to do this with 10000 traffic lights in the scene?

    [SerializeField] private float stopDistance = 4f;
    private VehicleAgent agent;

    private void Start()
    {
        agent = GetComponent<VehicleAgent>();
    }

    private void Update()
    {
        if (trafficLight == null) return;

        float dist = Vector3.Distance(transform.position, trafficLight.transform.position);

        if (dist < stopDistance && trafficLight.CurrentLightState() == LightState.Red)
        {
            agent.ChangeSpeed(0f);
        }
        else
        {
            agent.ChangeSpeed(8f); // Get the old speed and save it, or accelerate 
        }
    }
}
