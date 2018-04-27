using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class GA
{

    public static string DrawNewPaddle(string args)
    {
        MorphSprite instanceofMorphSprite = GameObject.Find("MutantPaddle").GetComponent<MorphSprite>();

        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/DrawShapes/DrawShape.exe";
        start.Arguments = string.Format("{0}", args);
        Process.Start(start);
        AssetDatabase.ImportAsset("Assets/PongAssets/MutantPaddle.png");
        //instanceofMorphSprite.Reattach();

        
        return "";
    }
}

