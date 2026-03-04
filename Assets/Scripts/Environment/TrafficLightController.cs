using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightState { Green, Yellow, Red}
public class TrafficLightController : MonoBehaviour
{
    [SerializeField] private LightState lightState = LightState.Green;
    [SerializeField] private float greenTime = 5f;
    [SerializeField] private float yellowTime = 2f;
    [SerializeField] private float redTime = 5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;


        switch(lightState)
        {
            case LightState.Green:
                    if (timer >= greenTime) SwitchState(LightState.Yellow);
                break;
            case LightState.Yellow:
                if (timer >= yellowTime) SwitchState(LightState.Red);
                break;
            case LightState.Red:
                if (timer >= redTime) SwitchState(LightState.Green);
                break;
        }
        Debug.Log(lightState.ToString());
    }
    private void SwitchState(LightState newState)
    {
        lightState = newState;
        timer = 0f;
    }

    public LightState CurrentLightState() => lightState;

}
