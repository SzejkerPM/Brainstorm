using Godot;

namespace Brainstorm.Scripts;

public partial class GameUi : CanvasLayer
{
    [Export] private Label _gameHudPlayerPoints;
    [Export] private TextureRect[] _health;
    [Export] private Texture2D _spriteLostHealth;
    [Export] private Control _gameHud;
    [Export] private ColorRect _gameOverScreen;
    [Export] private ColorRect _mainMenuScreen;
    [Export] private Label _gameOverPlayerPoints;
    [Export] private Label _gameOverPlayerHighPoints;
    [Export] private Label _mainMenuPlayerHighPoints;

    [Signal]
    public delegate void GameStartedEventHandler();

    [Signal]
    public delegate void RestartGameEventHandler();

    [Signal]
    public delegate void ExitGameEventHandler();

    public void SetHighScoreOnMainMenu(int highScore)
    {
        _mainMenuPlayerHighPoints.Text = $"High Score: {highScore}";
    }

    public void OnPointsChanged(int points)
    {
        _gameHudPlayerPoints.Text = points.ToString();
    }

    public void OnHealthChanged(int health)
    {
        _health[health].Texture = _spriteLostHealth;
    }

    public void OnGameOver(int lastScore, int highScore)
    {
        _gameOverPlayerPoints.Text = $"Score: {lastScore}";
        _gameOverPlayerHighPoints.Text = $"High Score: {highScore}";
        _gameHud.Hide();
        _gameOverScreen.Show();
    }

    private void OnRestartButtonPressed()
    {
        EmitSignal(SignalName.RestartGame);
    }

    private void OnExitButtonPressed()
    {
        EmitSignal(SignalName.ExitGame);
    }

    private void OnPlayButtonPressed()
    {
        _mainMenuScreen.Hide();
        EmitSignal(SignalName.GameStarted);
    }
}