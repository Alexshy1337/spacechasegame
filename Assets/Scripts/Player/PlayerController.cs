using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpaceChaseGame;
using static Logger;

public class PlayerController : MonoBehaviour
{


    [SerializeField] Sensor _jumpLeft, _jumpUp, _jumpRight, _up, _down, _left, _right;
    Camera _mainCamera;
    Transform _death;
    Sensor[] _sensors, _jumpSensors;
    Vector2[] _movementVectors;

    

    private bool _isPlaying = true;

    

    private void Awake()
    {

        _sensors = new Sensor[4];
        _sensors[DIR_UP] = _up;
        _sensors[DIR_DOWN] = _down;
        _sensors[DIR_LEFT] = _left;
        _sensors[DIR_RIGHT] = _right;

        _jumpSensors = new Sensor[4];
        _jumpSensors[DIR_UP] = _jumpUp;
        _jumpSensors[DIR_DOWN] = null;
        _jumpSensors[DIR_LEFT] = _jumpLeft;
        _jumpSensors[DIR_RIGHT] = _jumpRight;

        //_jumpSensors[DIR_DOWN].state = Sensor.SensorData.Wall;

        _movementVectors = new Vector2[4];
        _movementVectors[DIR_UP] = new Vector2(0, 1);
        _movementVectors[DIR_DOWN] = new Vector2(0, -1);
        _movementVectors[DIR_LEFT] = new Vector2(-1, 0);
        _movementVectors[DIR_RIGHT] = new Vector2(1, 0);
        TimeStepEvent += IncreaseTimeStep;
    }

    void Start()
    {

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _death = GameObject.FindGameObjectWithTag("Death").transform;
        

    }

    void Update()
    {
        if (!_isPlaying)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Interact(DIR_RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Interact(DIR_LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Interact(DIR_UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Interact(DIR_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DelayEnemy();
        }

    }

    private void OnDestroy()
    {
        TimeStepEvent -= IncreaseTimeStep;
    }

    private void Interact(int direction)
    {
        if (CanMove(direction))
        {
            Move(_movementVectors[direction]);
            TimeStepEvent?.Invoke(this, EventArgs.Empty);
        }
        else if (CanMine(direction))
        {
            Mine(direction);
            TimeStepEvent?.Invoke(this, EventArgs.Empty);
        }
        
    }

    private void IncreaseTimeStep(object o, EventArgs e)
    {
        TimeSteps += 1;
        UIUpdateEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Move(Vector2 dir, int magnitude = 1)
    {
        for(int i = 0; i < magnitude; i++)
        {
            if (dir == _movementVectors[DIR_UP])
                TryIncreaseScore();
            transform.position += new Vector3(dir.x, dir.y, transform.position.z);
        }
        _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, transform.position.y, _mainCamera.transform.position.z);
        SensorsUpdateEvent?.Invoke(this, EventArgs.Empty);
    }
    
    private bool CanMove(int direction) => _sensors[direction].state == Sensor.SensorData.Space;
    
    private void Mine(int direction)
    {
        _sensors[direction].planet.OreAmount -= MineCapacity;
        OreCollected += MineCapacity;
        if (_sensors[direction].planet.OreAmount <= 0)
        {
            _sensors[direction].planet.MinedOut();
            OreCollected += _sensors[direction].planet.OreAmount;
        }
        UIUpdateEvent?.Invoke(this, EventArgs.Empty);
    }
    
    private bool CanMine(int direction)
    {
        return _sensors[direction].planet != null && _sensors[direction].planet.OreAmount > 0;
        
        //_sensors[direction].planet.Hardness <= _miningPower
    }

    public void Jump(int direction = DIR_UP)
    {
        if(CanJump(direction) && OreCollected >= JUMP_ORE_COST)
        {
            Move(_movementVectors[direction], JumpDistance);
            OreCollected -= JUMP_ORE_COST;
            TimeStepEvent?.Invoke(this, EventArgs.Empty);
            //soundSystem.playJumpSound ~
        }
        else
        {
            //show ui message "can't jump!"
            Debug.LogWarning("Can't Jump!");
        }
    }

    private bool CanJump(int direction)
    {
        return _jumpSensors[direction].state == Sensor.SensorData.Space;
    }
    
    public void UpgradeMineCapacity()
    {
        if (OreCollected >= MINE_CAPACITY_UPGRADE_COST)
        {
            MineCapacity++;
            OreCollected -= MINE_CAPACITY_UPGRADE_COST;
            UIUpdateEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public void UpgradeJumpDistance()
    {
        if(OreCollected >= JUMP_UPGRADE_COST)
        {
            JumpDistance++;
            MoveJumpSensors();
            OreCollected -= JUMP_UPGRADE_COST;
            SensorsUpdateEvent?.Invoke(this, EventArgs.Empty);
            UIUpdateEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void MoveJumpSensors()
    {
        _jumpSensors[DIR_LEFT].transform.position += new Vector3(-1, 0, 0);
        _jumpSensors[DIR_UP].transform.position += new Vector3(0, 1, 0);
        _jumpSensors[DIR_RIGHT].transform.position += new Vector3(1, 0, 0);
    }

    public void DelayEnemy()
    {
        if(OreCollected >= DELAY_ORE_COST)
        {
            EnemyController.DelayEnemy();
            OreCollected -= DELAY_ORE_COST;
        }
    }

    private void TryIncreaseScore()
    {
        if(Score < transform.position.y)
        {
            Score = transform.position.y;
            ScoreIncreaseEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("game is over");
        _isPlaying = false;
    }

    
}
