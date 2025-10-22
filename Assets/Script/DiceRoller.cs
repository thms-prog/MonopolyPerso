using UnityEngine;

public static class DiceRoller
{
    // Un dé à 'faces' faces (par défaut 6) -> entier 1..faces
    public static int Roll(int faces = 6)
    {
        return Random.Range(1, faces + 1);
    }

    // Deux dés (2D6) : renvoie la somme et sort aussi d1/d2
    public static int Roll2D6(out int d1, out int d2)
    {
        d1 = Roll();
        d2 = Roll();
        return d1 + d2;
    }
}
