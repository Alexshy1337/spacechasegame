using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

public class TriggerColliderSensor : Sensor
{
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Wall"))
        {
            Log("Sensor entered wall");
            state = SensorData.Wall;
        }
        if (collision.CompareTag("Death"))
        {
            Log("Sensor entered death");
            state = SensorData.Death;
        }
        if (collision.CompareTag("Planet"))
        {
            Log("Sensor entered planet");
            state = SensorData.Planet;
            planet = collision.GetComponent<Planet>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        state = SensorData.Space;
        planet = null;
    }
}
