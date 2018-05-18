using System;
using System.IO;
using UnityEngine;

class Population
{
    // population size
    private int size;

    // generation number
    private int genNum;

    // population of Inidividuals
    private Individual[] population;

    private Population parentPop;

    // stats
    private double maxFit, minFit, averageFit, sumFit;

    public Population(Individual[] pop, Population parentPop, int genNum)
    {
        population = pop;
        size = pop.Length;
        this.genNum = genNum;

        // destroy grandparents
        if (parentPop != null)
            parentPop.DestroyParents();

        this.parentPop = parentPop;
    }

    void DestroyParents()
    {
        parentPop = null;
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

    public int GetPopSize()
    {
        return size;
    }

    public double GetSumFitness()
    {
        return sumFit;
    }

    public Individual[] GetPopulation()
    {
        return (Individual[])population.Clone();
    }

    public void Statistics()
    {
        maxFit = -1;
        minFit = 10;
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

        // display population stats to console
        if (parentPop != null)
            GenerateResults(parentPop);
    }

    public void GenerateResults(Population parentPop)
    {
        // header line 1
        string report = "\t\t\t\t\t\tGeneration Report\n"; 

        // header line 2
        report += "\t\t\t\tGeneration " + (parentPop.genNum) + " \t\t\t\t\t\t Generation " + genNum + "\n";

        // header line 3
        report += "#  parents   xsite       chromosome \t\t\t\tfitness \t\t #   parents   xsite     chromosome \t\t\t\tfitness \n";

        Individual[] parentInds = parentPop.GetPopulation();
        Individual[] childInds = GetPopulation();

        Individual parent;
        Individual child;

        for (int i = 0; i < size; i++)
        {
            parent = parentInds[i];
            child = childInds[i];

            report += i + parent.ToString() + " \t | \t " + i + "   " + child.ToString() + "\n";
        }

        report += "Avg Fit: " + parentPop.GetAverageFitness() + ", Max Fit: " + parentPop.GetMaxFitness() + ", Min Fit: " + parentPop.GetMinFitness();
        report += "\t|\t Avg Fit: " + GetAverageFitness() + ", Max Fit: " + GetMaxFitness() + ", Min Fit: " + GetMinFitness();
        Debug.Log(report);

        saveEvolution(report);
    }

    void saveEvolution(string report)
    {

        string path = "Assets/MyEvolutionReport.txt";
        if (!File.Exists(path))
        {   //creates file if non-existing
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(report);
            }

        }
        using (StreamWriter sw = File.AppendText(path))
        {   sw.WriteLine(Environment.NewLine);
            sw.WriteLine(report);
            
        }
    }
}
