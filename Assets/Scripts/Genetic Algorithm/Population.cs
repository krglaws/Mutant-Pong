

class Population
{
    // population size
    private int size;

    // population of Inidividuals
    private Individual[] population;

    // stats
    private double maxFit, minFit, averageFit, sumFit;

    public Population(Individual[] pop)
    {
        population = pop;
        size = pop.Length;
    }

    public double GetAverageFitness()
    {
        return averageFit;
    }

    public Individual GetIndividual(int index)
    {
        return population[index];
    }

    public double GetMaxFitness()
    {
        return maxFit;
    }

    public double GetMinFitness()
    {
        return minFit;
    }

    public Individual[] GetPopulation()
    {
        return (Individual[]) population.Clone();
    }

    private void statistics()
    {
        maxFit = 0;
        minFit = 1;
        sumFit = 0;
        averageFit = 0;
        
        for (int i = 0; i < size; i++)
        {
            double fitness = population[i].GetFitness();
            sumFit += fitness;
            if (minFit > fitness)
                minFit = fitness;
            if (maxFit < fitness)
                maxFit = fitness;
        }
        averageFit = sumFit / size;
    }
}
