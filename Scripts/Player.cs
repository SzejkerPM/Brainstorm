using Godot;

namespace Brainstorm.Scripts;

public partial class Player : CharacterBody2D
{
    [Export] private float _speed = 120.0f;
    private int _health = 3;
    private int _points;

    [Export] private Sprite2D _spritePlayer;

    [Signal]
    public delegate void HealthChangedEventHandler(int health);

    [Signal]
    public delegate void PointsChangedEventHandler(int points);

    [Signal]
    public delegate void PlayerDiedEventHandler(int lastScore);

    [Export] private CpuParticles2D _pointCollectParticles;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * _speed;
            FlipSprite(direction.X);
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area.IsInGroup("Good"))
        {
            _points++;
            PlayCollectPointParticles();
            EmitSignal(SignalName.PointsChanged, _points);
        }
        else if (area.IsInGroup("Bad"))
        {
            _health--;

            if (_health > 0)
            {
                EmitSignal(SignalName.HealthChanged, _health);
            }
            else
            {
                EmitSignal(SignalName.PlayerDied, _points);
            }
        }

        area.QueueFree();
    }

    private void PlayCollectPointParticles()
    {
        _pointCollectParticles.Restart();
        _pointCollectParticles.Emitting = true;
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0) _spritePlayer.FlipH = true;
        else if (direction < 0) _spritePlayer.FlipH = false;

        var tween = CreateTween();
        _spritePlayer.Scale = new Vector2(1.2f, 0.8f);
        tween.TweenProperty(_spritePlayer, "scale", Vector2.One, 0.1f);
    }
}