using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Day
{
    Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
}

public enum TimeOfTheDay
{
    Morning, Afternoon, Evening
}

public class GameManager : Singleton<GameManager>
{
    [Header("===== Generate Game Prefab =====")]
    [Header("- Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject cameraPrefab;
    [Space(10)]
    [SerializeField] Transform spawnPoin;
    [HideInInspector] public PlayerManager player;
    [HideInInspector] public PlayerUI playerUI;

    [Header("===== Time =====")]
    [SerializeField] float startTimeSpeed;
    float curTimeSpeed;
    public int curDayCount;
    [HideInInspector] public Day curDay;
    [HideInInspector] public TimeOfTheDay curTimeOfTheDay;
    [HideInInspector] public int curHours;
    [HideInInspector] public int curMinute;
    [HideInInspector] public float curSec;

    private void Awake()
    {
        InitPrefab();
        ResetTimeSpeed();
    }

    private void Update()
    {
        IncreaseTime();
    }

    void InitPrefab()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoin.position, Quaternion.identity);
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        this.player = playerManager;
        PlayerUI playerUI = player.GetComponent<PlayerUI>();
        this.playerUI = playerUI;
        GameObject camera = Instantiate(cameraPrefab, spawnPoin.position, Quaternion.identity);
        CameraController camCon = camera.GetComponent<CameraController>();
        camCon.SelectTarget(player.transform);
    }

    #region Time

    public void ResetTimeSpeed()
    {
        SetTimeSpeed(startTimeSpeed);
    }

    public void SetTimeSpeed(float speed)
    {
        curTimeSpeed = speed;
    }

    void IncreaseTime()
    {
        curSec += curTimeSpeed * Time.deltaTime;
        if (curSec >= 60)
        {
            curMinute++;
            if (curMinute >= 60)
            {
                curHours++;
                curMinute = 0;

                if (curHours % 8 == 0) SwitchTimeOfTheDay();

                if (curHours >= 24)
                {
                    curDayCount++;
                    playerUI.UpdateDayCount();
                    SwitchDay();
                    curHours = 0;
                }
            }
            curSec = 0;
        }
    }

    void SwitchDay()
    {
        switch (curDay)
        {
            case Day.Monday:
                curDay = Day.Tuesday;
                break;
            case Day.Tuesday:
                curDay = Day.Wednesday;
                break;
            case Day.Wednesday:
                curDay = Day.Thursday;
                break;
            case Day.Thursday:
                curDay = Day.Friday;
                break;
            case Day.Friday:
                curDay = Day.Saturday;
                break;
            case Day.Saturday:
                curDay = Day.Sunday;
                break;
            case Day.Sunday:
                curDay = Day.Monday;
                break;
        }
        playerUI.UpdateDay();
    }

    void SwitchTimeOfTheDay()
    {
        switch (curTimeOfTheDay)
        {
            case TimeOfTheDay.Morning:
                curTimeOfTheDay = TimeOfTheDay.Afternoon;
                break;
            case TimeOfTheDay.Afternoon:
                curTimeOfTheDay = TimeOfTheDay.Evening;
                break;
            case TimeOfTheDay.Evening:
                curTimeOfTheDay = TimeOfTheDay.Morning;
                break;
        }
        playerUI.UpdateTimeOfDayText();
    }

    #endregion

}
