using System;
using System.Collections.Generic;
using System.Linq;

//[Serializable]
public class RandomSelector
{
    private static Random random = new Random();

    /// <summary>
    /// S�lectionne un �l�ment en fonction de probabilit�s d�finies.
    /// </summary>
    /// <typeparam name="T">Type des �l�ments du tableau</typeparam>
    /// <param name="values">Liste des valeurs possibles</param>
    /// <param name="weights">Liste des probabilit�s associ�es (en % ou en pond�ration)</param>
    /// <returns>Un �l�ment choisi al�atoirement selon les probabilit�s</returns>
    public T ChooseWithWeights<T>(List<T> values, List<int> weights)
    {
        if (values.Count != weights.Count)
            throw new ArgumentException("Le nombre de valeurs et de poids doit �tre identique.");

        int totalWeight = weights.Sum();
        if (totalWeight <= 0)
            throw new ArgumentException("La somme des probabilit�s doit �tre > 0.");

        int roll = random.Next(0, totalWeight); // tirage entre 0 et (somme - 1)
        int cumulative = 0;

        for (int i = 0; i < values.Count; i++)
        {
            cumulative += weights[i];
            if (roll < cumulative)
                return values[i];
        }

        return values.Last(); // s�curit�, normalement jamais atteint
    }
}
