using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class GA
{
    // current generation number
    public static int genNum = 0;

    // population size (must be an even number)
    public const int POP_SIZE = 30;

    // the length of all chromosomes
    const int CHROM_LENGTH = 16;

    // probabilities
    const double P_CROSS = 0.6;
    const double P_MUTATION = 1.0 / CHROM_LENGTH;

    // maximum change in x and y values during mutation
    const int MAX_MUTATION = 10;

    // current generation
    static Population currGen;

    // random number generator
    static System.Random RANDOM = new System.Random();


    /*----------------------------------------------------
     * Creates a Population of randomly
     * generated individuals with RandomPopulation()
     * and draws each individual with DrawNewPaddle().
     */
    public static void Init()
    {
        // generate base individuals
        currGen = RandomPopulation();

        // draw population of paddles
        for (int i = 0; i < POP_SIZE; i++)
            DrawNewPaddle(currGen.GetIndividual(i).GetChromosome(), i);
    }


    // Called by GameManager once all individuals have played
    public static void NextGen()
    {
        // generate statistics for currGen
        currGen.Statistics();

        // breed next population
        currGen = BreedPopulation(currGen);
        genNum++;

        // draw population of paddles
        for (int i = 0; i < POP_SIZE; i++)
            DrawNewPaddle(currGen.GetIndividual(i).GetChromosome(), i);
    }


    /*----------------------------------------------------
     * Creates a Population of randomly
     * generated individuals.
     */
    static Population RandomPopulation()
    {
        Individual[] inds = new Individual[POP_SIZE];

        for (int i = 0; i < POP_SIZE; i++)
        {
            inds[i] = RandomIndividual();
        }

        return new Population(inds, null, 0);
    }


    /*----------------------------------------------------
     * Generates a new individual with random
     * chromosome values.
     */
    static Individual RandomIndividual()
    {
        float[] chrom = new float[CHROM_LENGTH];
        for (int i = 0; i < CHROM_LENGTH; i++)
            chrom[i] = RANDOM.Next(101);
        Individual ind = new Individual(chrom, -1, -1, 0);

        return ind;
    }


    /*----------------------------------------------------
     * Draws a new sprite to be tested in
     * Unity. Saves PNG to Assets/Resources
     * directory.
     */
    static void DrawNewPaddle(float[] chrom, int paddleNum)
    {

        // setup args for DrawShape.exe
        string chromAsString = "";
        foreach (float f in chrom)
            chromAsString += f + " ";
        string args = paddleNum + " " + chromAsString;

        // draw new paddle
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/DrawShapes/DrawShape.exe";
        start.Arguments = string.Format("{0}", args);
        Process.Start(start);

        // import new sprite into unity
        AssetDatabase.Refresh();
        //AssetDatabase.ImportAsset("Assets/Resources/" + paddleNum + ".png");
    }


    /*----------------------------------------------------
     * Called in GameManager class to send the
     * result of each individual's game to its
     * respective object in the population.
     */
    public static void SendScores(double p1Score, double p2Score, double max, int paddleNum)
    {
        double opponentLoss = System.Math.Abs(max - p2Score);

        double fitness = (p1Score + opponentLoss) / (max * 2);
        
        currGen.GetIndividual(paddleNum).SetFitness(fitness);

        string message = "Gen: " + genNum + ", Ind: " + paddleNum + ", Fit: " + fitness;
        
        UnityEngine.Debug.Log(message);
    }


    /*----------------------------------------------------
     * Selects an individual for breeding, and
     * returns its index.
     */
    static int SelectIndividual(Population p)
    {
        double partsum = 0.0;
        double rand = RANDOM.NextDouble() * p.GetSumFitness();
        int i;
        for (i = 0; partsum < rand && i < p.GetPopSize(); i++)
            partsum += p.GetIndividual(i).GetFitness();
        return i - 1;
    }


    /*----------------------------------------------------
     * Breeds a new population from the previous
     * generation.
     */
    static Population BreedPopulation(Population curr)
    {
        Individual[] inds = new Individual[POP_SIZE];
        for (int i = 0; i < POP_SIZE; i += 2)
        {
            int p1Index = SelectIndividual(curr);
            int p2Index = SelectIndividual(curr);

            Individual p1 = curr.GetIndividual(p1Index);
            Individual p2 = curr.GetIndividual(p2Index);

            Individual[] twoInds = CrossOver(p1, p1Index, p2, p2Index);
            Individual c1 = twoInds[0];
            Individual c2 = twoInds[1];

            inds[i] = c1;
            inds[i + 1] = c2;
        }

        return new Population(inds, curr, genNum+1);
    }


    static Individual[] CrossOver(Individual p1, int p1Index, Individual p2, int p2Index)
    {
        // create array of new individuals
        Individual[] inds = new Individual[2];

        float[] childChrom1 = new float[CHROM_LENGTH];
        float[] childChrom2 = new float[CHROM_LENGTH];
        float[] parentChrom1 = p1.GetChromosome();
        float[] parentChrom2 = p2.GetChromosome();

        int crossingPoint = EvenRand(1, CHROM_LENGTH - 2);

        // flip coin
        if (CoinFlip(P_CROSS))
        {
            // cross parents to create children
            for (int i = 0; i < crossingPoint; i++)
            {
                childChrom1[i] = parentChrom1[i];
                childChrom2[i] = parentChrom2[i];
            }
            for (int i = crossingPoint; i < CHROM_LENGTH; i++)
            {
                childChrom1[i] = parentChrom2[i];
                childChrom2[i] = parentChrom1[i];
            }

            // mutate children
            Mutate(childChrom1);
            Mutate(childChrom2);

            inds[0] = new Individual(childChrom1, p1Index, p2Index, crossingPoint);
            inds[1] = new Individual(childChrom2, p1Index, p2Index, crossingPoint);
        }

        else
        {
            // coin flip came up tails
            crossingPoint = 0;
            childChrom1 = parentChrom1;
            childChrom2 = parentChrom2;
            inds[0] = new Individual(childChrom1, p1Index, p2Index, 0);
            inds[1] = new Individual(childChrom2, p1Index, p2Index, 0);
        }

        return inds;
    }

    // loops through chromosome with a chance of mutation
    static void Mutate(float[] c)
    {
        for (int i = 0; i < CHROM_LENGTH; i++)
        {
            // flip coin
            if (CoinFlip(P_MUTATION))
            {
                // mutate nucleotide
                c[i] = DeltaXY(MAX_MUTATION, (int)c[i]);
            }
        }
    }

    // generate new nucleotide
    static float DeltaXY(int maxDelta, int currVal)
    {
        int delta = RANDOM.Next(-maxDelta, maxDelta + 1);
        currVal += delta;

        // check if this mutation would place the
        // new vertex out of the bounds of the paddle image
        if (currVal < 0)
            currVal = 0;
        else if (currVal > 100)
            currVal = 100;
      
        return currVal;
    }

    // biased coin toss used to decide if crossover will happen
    static bool CoinFlip(double p)
    {
        return (RANDOM.NextDouble() < p);
    }

    // used to choose random crossing point (must be an even number)
    static int EvenRand(int min, int max)
    {
        int r = RANDOM.Next(min, max + 1);
        if (r % 2 == 1)
            r++;
        return r;
    }
}