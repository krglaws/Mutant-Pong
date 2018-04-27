

class Individual
{
    private double fitness;

    // array of x,y coordinate pairs
    private float[] chromosome;

    // 'rents
    private int parent1, parent2;

    public Individual(float[] chrom, int p1, int p2)
    {
        chromosome = chrom;
        parent1 = p1;
        parent2 = p2;
    }

    public Individual Clone()
    {
        Individual clone = new Individual(chromosome, parent1, parent2);
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
}