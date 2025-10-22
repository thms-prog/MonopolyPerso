using System.Collections;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    [SerializeField] private float stepDuration = 0.2f; // temps d'une case
    private bool _isMoving;

    public Coroutine MoveAlong(BoardController board, int fromIndex, int steps)
    {
        if (_isMoving || board == null || board.Size == 0) return null;
        return StartCoroutine(CoMove(board, fromIndex, steps));
    }

    private IEnumerator CoMove(BoardController board, int fromIndex, int steps)
    {
        _isMoving = true;
        int size = board.Size;
        int cur = fromIndex;
        int dir = steps >= 0 ? 1 : -1;
        int count = Mathf.Abs(steps);

        for (int i = 0; i < count; i++)
        {
            int next = ((cur + dir) % size + size) % size;
            Transform tNext = board.GetTile(next);
            if (tNext != null)
                yield return LerpTo(tNext.position, stepDuration);
            cur = next;
        }
        _isMoving = false;
    }

    private IEnumerator LerpTo(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        transform.position = target;
    }
}

