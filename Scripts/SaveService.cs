using Godot;

namespace Brainstorm.Scripts;

public static class SaveService
{
    private const string SavePath = "user://save.dat";

    public static void Save(SaveData data)
    {
        using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);

        if (file == null)
        {
            GD.PrintErr("SaveService: Could not open save file");
            return;
        }

        var dataDictionary = new Godot.Collections.Dictionary
        {
            { "HighScore", data.HighScore },
        };

        file.StoreVar(dataDictionary);

        GD.Print("SaveService: Game Saved");
    }

    public static SaveData Load()
    {
        if (!FileAccess.FileExists((SavePath)))
        {
            GD.Print("SaveService: File not found. Creating a new one.");
            return new SaveData();
        }

        using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);

        if (file.GetVar().AsGodotDictionary() is Godot.Collections.Dictionary dictionary)
        {
            return new SaveData
            {
                HighScore = (int)dictionary["HighScore"],
            };
        }

        return new SaveData();
    }
}