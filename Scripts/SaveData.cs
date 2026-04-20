using System;
using Godot;

namespace Brainstorm.Scripts;

public partial class SaveData : RefCounted
{
    private int _highScore;

    public int HighScore
    {
        get => _highScore;
        set => _highScore = Math.Max(0, value);
    }
}