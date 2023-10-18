using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SpaceChaseGame;

public class GameGenerator : MonoBehaviour
{

    [SerializeField] GameObject _planetPrefab;
    [SerializeField] GameObject _wallPrefab;
    [SerializeField] float _planetProbability = 0.3f;
    [SerializeField] int _timeSinceLastPlanet = 0;
    [SerializeField] int _timeSinceLastWall = 0;

    private List<int> _xPositions = new List<int> { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
    private System.Random random = new();
    private float height = 6;
    //[SerializeField]



    void Start()
    {
        ScoreIncreaseEvent += GenerateNewRow;
    }

    private void OnDestroy()
    {
        ScoreIncreaseEvent -= GenerateNewRow;
    }

    private void GenerateNewRow(object o, EventArgs e)
    {
        height = Score + 6;
        int obstaclesAmount = ObstaclesAmount();
        int planetsAmount = PlanetsAmount(obstaclesAmount);

        List<int> positions = new();
        positions.AddRange(_xPositions);
        
        for (int i = 0; i < planetsAmount; i++)
        {
            GameObject planet = PlaceObstacle(_planetPrefab, positions);
            if (planet != null)
                planet.GetComponent<Planet>().OreAmount = GenerateOreAmount();
        }

        for (int i = 0; i < obstaclesAmount - planetsAmount; i++)
        {
            PlaceObstacle(_wallPrefab, positions);
        }
    }

    private GameObject PlaceObstacle(GameObject prefab, List<int> positions)
    {
        if (positions.Count != 0)
        {
            int listIndex = random.Next(0, positions.Count);
            int x_pos = positions[listIndex];
            positions.RemoveAt(listIndex);
            return Instantiate(prefab, new Vector3(x_pos, height), Quaternion.identity);
        }
        return null;
    }

    private int ObstaclesAmount()
    {
        //need a steady increase from low number of obstacles to high tied to the score
        return random.Next(0, 9);
    }

    private int PlanetsAmount(int obstaclesAmount)
    {
        //need a steady increase from low number of obstacles to high tied to the score
        return random.Next(0,obstaclesAmount);
    }

    private int GenerateOreAmount()
    {
        int score = (int)Score;
        return random.Next(1 + score/4, 3 + score/2);
    }
}
