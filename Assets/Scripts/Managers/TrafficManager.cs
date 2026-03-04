using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] startNodes;
    public float spawnInterval = 3f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnVehicle();
            timer = 0f;
        }
    }

    private void SpawnVehicle()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform startNode = startNodes[Random.Range(0, startNodes.Length)];

        GameObject v = Instantiate(vehiclePrefab, spawn.position, spawn.rotation);
        v.GetComponent<VehicleAgent>().SetCurrentNode(startNode);

    }
}
