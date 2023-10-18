using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpaceChaseGame;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int _baseMovePause = 5;
    [SerializeField] private static int _currentMovePause = 5;
    [SerializeField] private int _moveDistance = 1;
    [SerializeField] private int _enemyMovesCounter = 0;

    void Start()
    {
        TimeStepEvent += MoveEnemy;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            _moveDistance = 5;

    }

    private void OnDestroy()
    {
        TimeStepEvent -= MoveEnemy;
    }

    public static void DelayEnemy()
    {
        _currentMovePause += 5;
    }

    private void MoveEnemy(object o, EventArgs e)
    {
        if (_currentMovePause == 0)
        {
            StartCoroutine(MoveCoroutine(_moveDistance));
            _currentMovePause = _baseMovePause;
            _enemyMovesCounter++;
            if (_enemyMovesCounter % 3 == 0)
                IncreaseDifficulty();
        }
        else
        {
            _currentMovePause--;
        }
    }

    private void IncreaseDifficulty()
    {
        if (_baseMovePause != 0)
            _baseMovePause--;
        else
            _moveDistance++;

    }

    private IEnumerator MoveCoroutine(int distance)
    {

        for(int i = distance; i > 0; i--)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            yield return 3;
        }
        yield return 0;
    }
}
