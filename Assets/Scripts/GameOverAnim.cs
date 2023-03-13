using DG.Tweening;
using UnityEngine;

public class GameOverAnim : MonoBehaviour
{
    [SerializeField] Transform _pointToMove;
    [SerializeField] Transform _startPos;
    public void LabelMove()
    {
        transform.DOMove(_pointToMove.position, 1);
    }
    public void LabelStartPos()
    {
        transform.position = _startPos.position;
    }
}
