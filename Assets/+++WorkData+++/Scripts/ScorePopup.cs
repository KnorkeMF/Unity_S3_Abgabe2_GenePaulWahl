using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScorePopup : MonoBehaviour
{
    public TextMeshPro text;

    public float floatDistance = 1f;
    public float duration = 1f;

    public void Show(int amount)
    {
        text.text = "+" + amount;
        
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        
        Sequence s = DOTween.Sequence();
        
        s.Append(transform.DOScale(1.2f, 0.2f).From(0f).SetEase(Ease.OutBack));

        s.Join(transform.DOMoveY(transform.position.y + floatDistance, duration));
        s.Join(text.DOFade(0f, duration));
        
        s.OnComplete(() => Destroy(gameObject));
    }
}

