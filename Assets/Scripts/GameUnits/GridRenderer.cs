using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : MonoBehaviour
{

    public const int VERT_LINE_COUNT = 12, HOR_LINE_COUNT = 25;
    public const float LEFT_BORDER = -5.5f, BOTTOM_BORDER = -9.5f, TOP_BORDER = 15f, RIGHT_BORDER = 5.5f, LINE_WIDTH = 0.02f;
    [SerializeField] Transform _linePrefab;


    void Start()
    {
        
        for(int i = 0; i < VERT_LINE_COUNT; i++)
        {
            Transform gridLine = Instantiate(_linePrefab, transform);
            LineRenderer line = gridLine.GetComponent<LineRenderer>();
            line.SetPosition(0, new Vector3(i + LEFT_BORDER, BOTTOM_BORDER, 0));
            line.SetPosition(1, new Vector3(i + LEFT_BORDER, TOP_BORDER, 0));
            line.endColor = Color.white;
            line.startColor = Color.white;
            line.startWidth = LINE_WIDTH;
            line.endWidth = LINE_WIDTH;
        }

        for (int i = 0; i < HOR_LINE_COUNT; i++)
        {
            Transform gridLine = Instantiate(_linePrefab, transform);
            LineRenderer line = gridLine.GetComponent<LineRenderer>();
            line.SetPosition(0, new Vector3(LEFT_BORDER, i + BOTTOM_BORDER, 0));
            line.SetPosition(1, new Vector3(RIGHT_BORDER, i + BOTTOM_BORDER, 0));
            line.endColor = Color.white;
            line.startColor = Color.white;
            line.startWidth = LINE_WIDTH;
            line.endWidth = LINE_WIDTH;
        }

    }


}

