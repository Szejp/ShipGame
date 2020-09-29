using System.Collections.Generic;
using System.Linq;
using LevelManagement.Presets;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class LevelManagerWindow : OdinEditorWindow
{
    [TableList] 
    public List<Preset> presets;

    static LevelManagerWindow window;

    PresetsManager PresetsManager;
    
    [PropertyOrder(-10)]
    [HorizontalGroup()]
    [Button(ButtonSizes.Medium)]
    public void SortPresets()
    {
        presets = presets.OrderBy(p => p.levelsRange.x).ToList();
    }

    [PropertyOrder(-10)]
    [HorizontalGroup()]
    [Button(ButtonSizes.Medium)]
    public void GeneratePresetsFromPrefabs()
    {
       var containers = window.PresetsManager.GetContainersFromPath();
       containers = containers.Where(p => !presets.Any(q => q.container.Equals(p))).ToList();

       foreach (var c in containers)
       {
           //var preset = new 
       }
    }

    [MenuItem("Window/LevelManagerWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:

        if (window == null)
        {
            Debug.Log("MyWindow.Init");
            window = (LevelManagerWindow) EditorWindow.GetWindow(typeof(LevelManagerWindow));
            window.PresetsManager = PresetsManager.instance;
            window.PresetsManager.LoadAll();
            window.presets = window.PresetsManager.presets;
            window.SortPresets();
        }
    }
}