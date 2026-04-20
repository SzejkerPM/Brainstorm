using Godot;

namespace Brainstorm.Scripts;

public partial class FallingObject : Area2D
{
    [Export] private float _speed = 50;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += Vector2.Down * _speed * (float)delta;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area.IsInGroup("Killzone"))
        {
            QueueFree();
        }
    }
}