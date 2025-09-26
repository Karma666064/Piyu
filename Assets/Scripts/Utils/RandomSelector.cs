using System;
using System.Collections.Generic;
using System.Linq;

//[Serializable]
public class RandomSelector
{
    private static Random random = new Random();

    /// <summary>
    /// Sélectionne un élément en fonction de probabilités définies.
    /// </summary>
    /// <typeparam name="T">Type des éléments du tableau</typeparam>
    /// <param name="values">Liste des valeurs possibles</param>
    /// <param name="weights">Liste des probabilités associées (en % ou en pondération)</param>
    /// <returns>Un élément choisi aléatoirement selon les probabilités</returns>
    public T ChooseWithWeights<T>(List<T> values, List<int> weights)
    {
        if (values.Count != weights.Count)
            throw new ArgumentException("Le nombre de valeurs et de poids doit être identique.");

        int totalWeight = weights.Sum();
        if (totalWeight <= 0)
            throw new ArgumentException("La somme des probabilités doit être > 0.");

        int roll = random.Next(0, totalWeight); // tirage entre 0 et (somme - 1)
        int cumulative = 0;

        for (int i = 0; i < values.Count; i++)
        {
            cumulative += weights[i];
            if (roll < cumulative)
                return values[i];
        }

        return values.Last(); // sécurité, normalement jamais atteint
    }
}
