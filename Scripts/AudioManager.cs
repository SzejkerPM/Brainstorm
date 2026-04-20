using Godot;

namespace Brainstorm.Scripts;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    private AudioStreamPlayer _musicPlayer;
    private AudioStreamPlayer _sfxPlayer;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        if (Instance != null && Instance != this)
        {
            QueueFree();
            return;
        }

        Instance = this;

        _musicPlayer = new AudioStreamPlayer();
        _sfxPlayer = new AudioStreamPlayer();

        AddChild(_musicPlayer);
        AddChild(_sfxPlayer);

        _musicPlayer.Bus = "Music";
        _sfxPlayer.Bus = "SFX";
    }

    public void PlayMusic(AudioStream music)
    {
        if (music == null) return;

        _musicPlayer.Stream = music;
        _musicPlayer.Play();
    }

    public void PlaySfx(AudioStream sfx)
    {
        if (sfx == null) return;

        _sfxPlayer.Stream = sfx;
        _sfxPlayer.Play();
    }
}