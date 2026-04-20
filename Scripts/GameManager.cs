using Godot;

namespace Brainstorm.Scripts;

public partial class GameManager : Node2D
{
    [Export] private GameUi _ui;
    [Export] private Player _player;

    [ExportGroup("Audio")] [Export] private AudioStream _soundGood;
    [Export] private AudioStream _soundBad;
    [Export] private AudioStream _music;

    [ExportGroup("Camera Shake")] [Export] private float _intensity = 5f;
    [Export] private float _duration = 0.2f;
    [Export] private int _numberOfShakes = 6;
    [Export] private int _gameOverIntensityMultiply = 5;
    [Export] private int _gameOverDurationMultiply = 2;
    private Vector2 _originalOffset;
    private Camera2D _camera;

    private SaveData _saveData;

    public override void _Ready()
    {
        _saveData = SaveService.Load();
        AudioManager.Instance.PlayMusic(_music);
        _ui.SetHighScoreOnMainMenu(_saveData.HighScore);
        _camera = GetViewport().GetCamera2D();
        _originalOffset = _camera.Offset;
        GetTree().Paused = true;
        SubscribeToEvents();
    }

    private void OnGameStarted()
    {
        GetTree().Paused = false;
    }

    private void OnGameRestart()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

    private void OnGameExit()
    {
        GetTree().Quit();
    }

    private void OnPlayerDied(int lastScore)
    {
        ApplyScreenShake(_intensity * _gameOverIntensityMultiply, _duration * _gameOverDurationMultiply);
        GetTree().Paused = true;
        UpdateAndSaveHighScore(lastScore);
        AudioManager.Instance.PlaySfx(_soundBad);
        _ui.OnGameOver(lastScore, _saveData.HighScore);
    }

    private void OnHealthChanged(int health)
    {
        ApplyScreenShake(_intensity, _duration);
        AudioManager.Instance.PlaySfx(_soundBad);
        _ui.OnHealthChanged(health);
    }

    private void OnPointsChanged(int points)
    {
        AudioManager.Instance.PlaySfx(_soundGood);
        _ui.OnPointsChanged(points);
    }

    private void UpdateAndSaveHighScore(int score)
    {
        if (score <= _saveData.HighScore) return;
        _saveData.HighScore = score;
        SaveService.Save(_saveData);
    }

    private void ApplyScreenShake(float intensity, float duration)
    {
        if (_camera == null) return;

        var tween = CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        for (var i = 0; i < _numberOfShakes; i++)
        {
            var randomOffset = _originalOffset + new Vector2(
                (float)GD.RandRange(-intensity, intensity),
                (float)GD.RandRange(-intensity, intensity)
            );

            tween.TweenProperty(_camera, "offset", randomOffset, duration / _numberOfShakes);
        }

        tween.TweenProperty(_camera, "offset", _originalOffset, duration / _numberOfShakes);
    }

    private void SubscribeToEvents()
    {
        _ui.GameStarted += OnGameStarted;
        _ui.RestartGame += OnGameRestart;
        _ui.ExitGame += OnGameExit;

        _player.PlayerDied += OnPlayerDied;
        _player.HealthChanged += OnHealthChanged;
        _player.PointsChanged += OnPointsChanged;
    }
}