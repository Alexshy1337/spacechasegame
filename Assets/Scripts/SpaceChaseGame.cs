using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceChaseGame
{

    //CONSTANTS
    public const int BORDER_COORDINATE = 5;

    public const int DIR_UP = 0;
    public const int DIR_DOWN = 1;
    public const int DIR_LEFT = 2;
    public const int DIR_RIGHT = 3;

    public const int JUMP_ORE_COST = 3;
    public const int DELAY_ORE_COST = 10;
    public const int JUMP_UPGRADE_COST = 2;
    public const int MINE_CAPACITY_UPGRADE_COST = 2;
    //CONSTANTS


    //STATIC MEMBERS
    public static float Score = 0;
    public static int TimeSteps = 0;
    public static int MineCapacity = 1;
    public static int JumpDistance = 2;
    public static int OreCollected = 0;

    public static EventHandler TimeStepEvent;
    public static EventHandler UIUpdateEvent;
    public static EventHandler ScoreIncreaseEvent;
    public static EventHandler EnemyDistanceUpdateEvent;
    public static EventHandler SensorsUpdateEvent;

    //STATIC MEMBERS


    public static float VerticalDistance(Transform objectOne, Transform objectTwo)
    {
        return MathF.Abs(objectOne.position.y - objectTwo.position.y);
    }
}
