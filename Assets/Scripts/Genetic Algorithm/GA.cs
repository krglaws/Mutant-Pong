using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GA
{

    static int round = 0;

    // population size
    const int POP_SIZE = 30;

    // the length of all chromosomes
    const int CHROM_LENGTH = 16;

    // current generation
    static Population currGen;

    // random number generator
    static System.Random RAND;

    public static void Init()
    {
        RAND = new System.Random();
        
        // generate base individuals
        currGen = RandomPopulation();

        // draw population of paddles
        for (int i = 0; i < POP_SIZE; i++)
            try
            {
                DrawNewPaddle(currGen.GetIndividual(i).GetChromosome(), i);
            } catch(NullReferenceException e)
            {
                UnityEngine.Debug.Log("error drawing paddle at "+i);
            }
    }

    /*----------------------------------------------------
     * Function:    RandomPopulation
     * Purpose:     Creates a Population of randomly
     *              generated individuals.
     */
    static Population RandomPopulation()
    {
        Individual[] inds = new Individual[POP_SIZE];

        for (int i = 0; i < POP_SIZE; i++)
        {
            inds[i] = RandomIndividual();
        }

        return new Population(inds);
    }

    /*----------------------------------------------------
     * Function:    RandomIndividual
     * Purpose:     Generates a new individual with random
     *              chromosome values.
     */
    static Individual RandomIndividual()
    {
        float[] chrom = new float[CHROM_LENGTH];
        for (int i = 0; i < CHROM_LENGTH; i++)
            chrom[i] = RAND.Next(101);
        Individual ind = new Individual(chrom, 0, 0);

        return ind;
    }

    /*----------------------------------------------------
     * Function:    DrawNewPaddle
     * Purpose:     Draws a new sprite to be tested in
     *              Unity. Saves PNG to Assets/Resources
     *              directory.
     *  
     * In:          string args:    x,y coordinate pairs.
     *              int paddleNum:  number of current paddle
     *                              to be drawn (e.g. '1.png').
     */
    public static void DrawNewPaddle(float[] chrom, int paddleNum)
    {
        // remove collider from MutantPaddle
        //MorphSprite instanceofMorphSprite = GameObject.Find("MutantPaddle").GetComponent<MorphSprite>();
        //instanceofMorphSprite.RemoveCollider();

        // setup args for DrawShape.exe
        string args = ""+paddleNum;
        foreach(float f in chrom)
        {
            args += " " + f;
        }

        // draw new paddle
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/DrawShapes/DrawShape.exe";
        start.Arguments = string.Format("{0}", args);
        Process.Start(start);

        // import new sprite into unity
        AssetDatabase.ImportAsset("Assets/Resources/"+paddleNum+".png");

        //SpriteRenderer renderer = GameObject.Find("MutantPaddle").GetComponent<SpriteRenderer>();
        /*
        if (round % 2 == 0)
        {
            AssetDatabase.ImportAsset("Assets/PongAssets/MutantPaddle1.png");
            renderer.sprite = Resources.Load<Sprite>("MutantPaddle1");
            round++;
        }
        else
        {
            AssetDatabase.ImportAsset("Assets/PongAssets/MutantPaddle2.png");
            renderer.sprite = Resources.Load<Sprite>("MutantPaddle2");
            round++;
        }

        // reset collider
        instanceofMorphSprite.AttachCollider();
        */
    }

    static void SetupNextSprite(string spriteName)
    {
        // remove collider from MutantPaddle
        //MorphSprite instanceofMorphSprite = GameObject.Find("MutantPaddle").GetComponent<MorphSprite>();
        //instanceofMorphSprite.RemoveCollider();

        SpriteRenderer renderer = GameObject.Find("MutantPaddle").GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>(spriteName);
    }
}
