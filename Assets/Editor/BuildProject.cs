using System;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine.SceneManagement;

namespace unityTools{
    public class BuildProject {

    public static void BuildAndroid() {
        Build(BuildTarget.Android);
    }


    public static void BuildIos() {
        Build(BuildTarget.iOS);
    }

    public static void Build(BuildTarget target) {
        List<string> scenes = new List<string>();
        foreach(var scene in EditorBuildSettings.scenes)
        {
            if(scene.enabled)
                scenes.Add(scene.path);
        }
        BuildPipeline.BuildPlayer(scenes.ToArray(), Environment.GetCommandLineArgs().Last(), target,
            BuildOptions.None);
    }
    }
}