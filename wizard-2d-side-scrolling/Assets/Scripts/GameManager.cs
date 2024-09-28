using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("===== Generate Game Prefab =====")]
    [Header("- Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject cameraPrefab;
    [Space(10)]
    [SerializeField] Transform spawnPoin;

    private void Awake()
    {
        InitPrefab();
    }

    void InitPrefab()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoin.position, Quaternion.identity);
        GameObject camera = Instantiate(cameraPrefab, spawnPoin.position, Quaternion.identity);
        CameraController camCon = camera.GetComponent<CameraController>();
        camCon.SelectTarget(player.transform);
    }

}
