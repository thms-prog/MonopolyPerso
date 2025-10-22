using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Header("Repères de cases (index 0..N-1)")]
    public Transform[] tiles;  // Assigne Tile_00, Tile_01, ... dans l'ordre

    public int Size => tiles?.Length ?? 0;

    public Transform GetTile(int index)
    {
        if (tiles == null || tiles.Length == 0) return null;
        index = ((index % tiles.Length) + tiles.Length) % tiles.Length; // wrap sécurisé
        return tiles[index];
    }
}
