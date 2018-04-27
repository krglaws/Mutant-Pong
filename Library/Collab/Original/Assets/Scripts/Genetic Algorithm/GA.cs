using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class GA
{
    public static string DrawNewPaddle(string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/DrawShapes/DrawShape.exe";
        start.Arguments = string.Format("{0}", args);
        Process.Start(start);
        AssetDatabase.ImportAsset("Assets/PongAssets/MutantPaddle.png");
        MorphSprite MutantPaddle = GameObject.FindGameObjectWithTag("MutantPaddle").GetComponent<MorphSprite>();
        MutantPaddle.updatePaddle();
        return "";
    }
}

