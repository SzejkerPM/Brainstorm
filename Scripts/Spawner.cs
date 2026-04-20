using Godot;

namespace Brainstorm.Scripts;

public partial class Spawner : Node2D
{
    [Export] private PackedScene _goodScene;
    [Export] private PackedScene _badScene;

    [Export] private float _goodMinSpawnTime = 0.5f;
    [Export] private float _badMinSpawnTime = 0.5f;

    [Export] private float _goodDecreaseSpawnTime = 0.02f;
    [Export] private float _badDecreaseSpawnTime = 0.02f;

    private Timer _goodSpawnTimer;
    private Timer _badSpawnTimer;

    // Centered spawn positions based on 14px sprite width and ~5px gap to fit -122 to 122 range.
    private readonly int[] _spawnPoints = [-115, -96, -77, -58, -38, -19, 0, 19, 38, 58, 77, 96, 115];

    private int _lastGoodIndex;
    private int _lastBadIndex;

    public override void _Ready()
    {
        if (_goodScene == null || _badScene == null)
        {
            GD.PrintErr("Spawner: No Scene Loaded to Spawn in Spawner");
            ProcessMode = ProcessModeEnum.Disabled;
        }
        else
        {
            _goodSpawnTimer = GetNode<Timer>("GoodTimer");
            _goodSpawnTimer.Timeout += OnGoodTimerTimeout;

            _badSpawnTimer = GetNode<Timer>("BadTimer");
            _badSpawnTimer.Timeout += OnBadTimerTimeout;
        }
    }

    private void OnGoodTimerTimeout()
    {
        SpawnSceneOnRandomPoint(_goodScene, out _lastGoodIndex);
        DecreaseTimer(_goodSpawnTimer, _goodMinSpawnTime, _goodDecreaseSpawnTime);
    }

    private void OnBadTimerTimeout()
    {
        SpawnSceneOnRandomPoint(_badScene, out _lastBadIndex);
        DecreaseTimer(_badSpawnTimer, _badMinSpawnTime, _badDecreaseSpawnTime);
    }

    private void SpawnSceneOnRandomPoint(PackedScene scene, out int lastSpawnIndexRef)
    {
        var objectToSpawn = scene.Instantiate<Node2D>();

        var randomSpawnPoint =
            GD.RandRange(0, _spawnPoints.Length - 1);

        while (randomSpawnPoint == _lastGoodIndex || randomSpawnPoint == _lastBadIndex)
        {
            randomSpawnPoint = GD.RandRange(0, _spawnPoints.Length - 1);
        }

        objectToSpawn.Position = new Vector2(_spawnPoints[randomSpawnPoint], 0);

        lastSpawnIndexRef = randomSpawnPoint;

        AddChild(objectToSpawn);
    }

    private static void DecreaseTimer(Timer timer, float minTime, float decreaseTime)
    {
        if (timer.WaitTime > minTime)
        {
            timer.WaitTime -= decreaseTime;
        }
    }
}