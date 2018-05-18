

class Individual
{
    private double fitness;

    // array of x,y coordinate pairs
    private float[] chromosome;

    // 'rents
    private int parent1, parent2;

    // cross site
    private int xSite;

    public Individual(float[] chrom, int p1, int p2, int crossSite)
    {
        chromosome = chrom;
        parent1 = p1;
        parent2 = p2;
        xSite = crossSite;
    }

    public Individual Clone()
    {
        Individual clone = new Individual(chromosome, parent1, parent2, xSite);
        return clone;
    }

    public float[] GetChromosome()
    {
        return (float[]) chromosome.Clone();
    }

    public double GetFitness()
    {
        return fitness;
    }

    public int GetParent1()
    {
        return parent1;
    }

    public int GetParent2()
    {
        return parent2;
    }

    public void SetFitness(double f)
    {
        fitness = f;
    }
   
    public override string ToString()
    {
        string chrom = "";
        foreach (float f in chromosome)
            chrom += f + " ";

        return "   (" + parent1 + ", " + parent2 + ")   " + xSite + "\t" + chrom + "\t" + fitness;
    }
}