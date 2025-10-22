using UnityEngine;

public class Monopoly1 : MonoBehaviour
{
    [SerializeField] private TurnController turn;
    private int _lastActionFrame = -1; // anti-double appel même frame

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

        bool triggered = false;

#if UNITY_ANDROID || UNITY_IOS
        // Mobile : uniquement le premier touch
        triggered = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
#else
        // PC/Editor : Espace OU clic gauche
        triggered = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
#endif

        // Anti double-trigger : n’agir qu’une fois par frame
        if (triggered && _lastActionFrame != Time.frameCount)
        {
            _lastActionFrame = Time.frameCount;
            turn.RollAndMove();
        }
    }

    public void OnRollButton()
    {
        if (turn == null) return;
        // Anti double-trigger pour les appels UI
        if (_lastActionFrame == Time.frameCount) return;
        _lastActionFrame = Time.frameCount;
        turn.RollAndMove();
    }
}
