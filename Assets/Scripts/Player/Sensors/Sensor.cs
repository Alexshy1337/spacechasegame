using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

public abstract class Sensor : MonoBehaviour
{
    public enum SensorData
    {
        Wall,
        Death,
        Planet,
        Space
    }

    public SensorData state;
    public Planet planet;

    private void Start()
    {
        state = SensorData.Space;
    }
}
