using System;
using TestProjectForMysteryTag;
using UnityEngine;

public class SpaceShooterGame : Singleton<SpaceShooterGame>, IGame
{
    [SerializeField] private Camera camera;

    private ObjectsPool objectsPool;
    private PlayerController playerController;
    private LevelGenerator levelGenerator;

    public Action DamagePlayerAction;
    public Action KillEnemyAction;

    public PlayerController PlayerController
    {
        get => playerController;
        set => playerController = value;
    }

    public ObjectsPool ObjectsPool
    {
        get => objectsPool;
        set => objectsPool = value;
    }

    public LevelGenerator LevelGenerator
    {
        get => levelGenerator;
        set => levelGenerator = value;
    }

    public int CountLive => countLive;

    public int NeedDestroyAsteroid { get => needDestroyAsteroid; set => needDestroyAsteroid = value; }
    public int CountDestroyAsteroid { get => countDestroyAsteroid; set => countDestroyAsteroid = value; }

    private int countLive = 1;

    private int needDestroyAsteroid = 2;
    private int countDestroyAsteroid = 0;

    public void StartGame()
    {
        objectsPool = GetComponent<ObjectsPool>();
        playerController = GetComponent<PlayerController>();
        levelGenerator = GetComponent<LevelGenerator>();
        Vector3 topCameraDisplay = camera.ScreenToWorldPoint(new Vector3(0f, camera.pixelHeight));
        Vector3 buttonCameraDisplay = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0f));
        Vector3 spawnPositionPlayer = new Vector3(0, -topCameraDisplay.y / 2, 15);
        playerController.Init(10, CalculateZoneMove(), spawnPositionPlayer);
        levelGenerator.Init(new Vector3(topCameraDisplay.x, topCameraDisplay.y, 15), buttonCameraDisplay, GameManager.Instance.CurrentMission.spawnWait, 3, GameManager.Instance.CurrentMission.countAsteroidsInWave);

        countLive = DataManager.Instance.CountLive;
        countDestroyAsteroid = 0;
        needDestroyAsteroid = GameManager.Instance.CurrentMission.countNeedKillEnemy;
        GUIController.Instance.ShowScreen<ScreenGame>();


    }

    private BoundaryMove CalculateZoneMove()
    {
        var sizeCameraDispaly = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight));
        BoundaryMove zoneMovePlayer = new BoundaryMove(-sizeCameraDispaly.x + 0.5f, sizeCameraDispaly.x - 0.5f,
            -sizeCameraDispaly.y + 1f, sizeCameraDispaly.y * 0.5f);
        return zoneMovePlayer;
    }

    public void AddScore(int value)
    {
        DataManager.Instance.AddScore(value);
    }

    public void DestroyAsteroid(int newScoreValue, Asteroid asteroid)
    {
        AddScore(newScoreValue);
        countDestroyAsteroid++;
        KillEnemyAction?.Invoke();
        if (countDestroyAsteroid >= NeedDestroyAsteroid) GameOver(true);
    }

    public void GetDamagePlayer(int count = 1)
    {
        countLive--;
        DamagePlayerAction?.Invoke();
        if (countLive <=  0) GameOver(false);
    }

    public void GameOver(bool isComplete)
    {
        levelGenerator.StopWaves();
        playerController.DestroyPlayer(false);
        if (isComplete)
        {
            GameManager.Instance.LevelComplete();
            GUIController.Instance.ShowScreen<ScreenWinGame>();
        }
        else
        {
            GUIController.Instance.ShowScreen<ScreenGameOver>();
        }
    }
}
