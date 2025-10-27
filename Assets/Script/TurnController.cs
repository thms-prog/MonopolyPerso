using TMPro;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [Header("Plateau")]
    [SerializeField] private int boardSize = 40;  // cases 0..39

    [Header("État courant")]
    [SerializeField] private int currentPosition = 0;  // position du pion (logique)
    [SerializeField] private int lastD1 = 0, lastD2 = 0, lastTotal = 0;

    [SerializeField] private BoardController board;
    [SerializeField] private PawnController pawn;
    [SerializeField] private TextMeshProUGUI diceText;
    [SerializeField] private TextMeshProUGUI tileText;

    public void RollAndMove()
    {
        lastTotal = DiceRoller.Roll2D6(out lastD1, out lastD2);

        int oldPos = currentPosition;
        int newPos = (currentPosition + lastTotal) % boardSize;
        bool passedStart = (oldPos + lastTotal) >= boardSize;

        currentPosition = newPos;

        if (passedStart)
        {
            // Exemple : bonus “Départ” (on fera la banque plus tard)
            Debug.Log("Passage par la case Départ !");
        }

        Debug.Log($"🎲 {lastD1} + {lastD2} = {lastTotal} | Case {oldPos} → {currentPosition}");

        // Déplacement visuel du pion
        if (board != null && pawn != null)
        {
            pawn.MoveAlong(board, oldPos, lastTotal);
        }
    }

    // Accesseurs utiles (si besoin ailleurs ensuite)
    public int GetPosition() => currentPosition;
    public (int d1, int d2, int total) GetLastRoll() => (lastD1, lastD2, lastTotal);
}
