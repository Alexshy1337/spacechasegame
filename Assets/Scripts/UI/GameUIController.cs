using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static SpaceChaseGame;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreLabel;
    [SerializeField] private TextMeshProUGUI _stepsLabel;
    [SerializeField] private TextMeshProUGUI _enemyDistanceLabel;
    [SerializeField] private TextMeshProUGUI _mineCapacityLabel;
    [SerializeField] private TextMeshProUGUI _jumpDistanceLabel;
    [SerializeField] private Button _buttonUpgradeMineCapacity;
    [SerializeField] private Button _buttonUpgradeJumpDistance;
    [SerializeField] private Button _buttonDelay;
    [SerializeField] private Button _buttonJumpUp;
    [SerializeField] private Button _buttonJumpRight;
    [SerializeField] private Button _buttonJumpLeft;
    [SerializeField] private Button _buttonMenu;
    [SerializeField] private TextMeshProUGUI _oreLabel;
    private PlayerController _player;
    private EnemyController _deathTrigger;

    

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _deathTrigger = GameObject.FindGameObjectWithTag("Death").GetComponent<EnemyController>();

        _buttonMenu.onClick.AddListener(() => {
            
            //load menu scene
        });
        
        _buttonJumpLeft.onClick.AddListener(() => {
            _player.Jump(DIR_LEFT);
        });
        
        _buttonJumpUp.onClick.AddListener(() => {
            _player.Jump(DIR_UP);
        });

        _buttonJumpRight.onClick.AddListener(() => {
            _player.Jump(DIR_RIGHT);
        });

        _buttonDelay.onClick.AddListener(() => {
            _player.DelayEnemy();
        });

        _buttonUpgradeMineCapacity.onClick.AddListener(() => {
            _player.UpgradeMineCapacity(); 
        });

        _buttonUpgradeJumpDistance.onClick.AddListener(() => {
            _player.UpgradeJumpDistance();
        });


        ScoreIncreaseEvent += OnScoreUpdate;
        UIUpdateEvent += OnOreUpdate;
        UIUpdateEvent += OnMineCapacityUpdate;
        UIUpdateEvent += OnTimeIncrease;
        UIUpdateEvent += OnJumpDistanceUpdate;
        UIUpdateEvent += OnEnemyDistanceUpdate;
        //SpaceChaseEvents.OnOreIncrease += OnOreUpdate;
        UIUpdateEvent.Invoke(this, EventArgs.Empty);
        //REWRITE to have just 1 method and one string for += to event and -= to event
        // or not...
    }


    private void OnDestroy()
    {
        ScoreIncreaseEvent -= OnScoreUpdate;
        UIUpdateEvent -= OnOreUpdate;
        UIUpdateEvent -= OnMineCapacityUpdate;
        UIUpdateEvent -= OnTimeIncrease;
        UIUpdateEvent -= OnJumpDistanceUpdate;
        UIUpdateEvent -= OnEnemyDistanceUpdate;

    }

    private void OnTimeIncrease(object o, EventArgs e)
    {
        _stepsLabel.text = "Steps: " + TimeSteps.ToString();
    }

    private void OnScoreUpdate(object o, EventArgs e)
    {
        _scoreLabel.text = "Score: " + Score.ToString();
    }

    private void OnOreUpdate(object o, EventArgs e)
    {
        _oreLabel.text = OreCollected.ToString();
    }

    private void OnMineCapacityUpdate(object o, EventArgs e)
    {
        _mineCapacityLabel.text = "Mine capacity: " + MineCapacity.ToString();
    }

    private void OnJumpDistanceUpdate(object o, EventArgs e)
    {
        _jumpDistanceLabel.text = "Jump Distance: " + JumpDistance.ToString();
    }

    private void OnEnemyDistanceUpdate(object o, EventArgs e)
    {
        _enemyDistanceLabel.text = "Enemy Distance: " + VerticalDistance(_player.transform, _deathTrigger.transform).ToString();
    }
}
