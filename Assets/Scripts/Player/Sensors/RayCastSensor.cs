using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

public class RayCastSensor : Sensor
{

    private void Awake()
    {
        SpaceChaseGame.SensorsUpdateEvent += UpdateSensor;
    }

    private void UpdateSensor(object o, EventArgs e)
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.01f);



        if (hitInfo.collider == null)
        {
            state = SensorData.Space;
            planet = null;
        }
        else
        if (hitInfo.collider.CompareTag("Wall"))
        {
            Log("Sensor entered wall");
            state = SensorData.Wall;
            planet = null;
        }
        else
        if (hitInfo.collider.CompareTag("Death"))
        {
            Log("Sensor entered death");
            state = SensorData.Death;
            planet = null;
        }
        else
        if (hitInfo.collider.CompareTag("Planet"))
        {
            Log("Sensor entered planet");
            state = SensorData.Planet;
            planet = hitInfo.collider.GetComponent<Planet>();
        }


    }

    private void OnDestroy()
    {
        SpaceChaseGame.SensorsUpdateEvent -= UpdateSensor;
    }

}
