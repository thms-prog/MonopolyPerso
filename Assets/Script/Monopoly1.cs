using UnityEngine;

public class Monopoly1 : MonoBehaviour
{
    [SerializeField] private TurnController turn;

    // Anti double-déclenchement (même frame)
    private static int _lastActionFrame = -1; // ← static : même garde-fou si jamais 2 instances existent

    void Awake()
    {
        if (turn == null) turn = GetComponent<TurnController>();
#if UNITY_2023_1_OR_NEWER
        if (turn == null) turn = FindFirstObjectByType<TurnController>();
#else
        if (turn == null) turn = FindObjectOfType<TurnController>();
#endif
    }

    void Update()
    {
        if (turn == null) return;

        // 🔑 IMPORTANT :
        // On NE capte plus les clics/touches globaux ici
        // (sinon bouton UI / dés cliquables déclenchent 2x).
        // On garde uniquement la barre ESPACE pour tester dans l’éditeur.

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space))
            SafeTrigger();
#endif
    }

    // Appelé par le bouton UI (actuel) OU par les futurs scripts de dés cliquables
    public void OnRollButton() => SafeTrigger();

    // (Alias facultatif pour les dés, si tu préfères nommer autrement)
    public void OnDiceTapped() => SafeTrigger();

    private void SafeTrigger()
    {
        if (turn == null) return;
        if (_lastActionFrame == Time.frameCount) return; // débounce
        _lastActionFrame = Time.frameCount;

        turn.RollAndMove();
    }
}
