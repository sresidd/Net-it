using UnityEngine;
using DG.Tweening;

public class UITween : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;

    private void OnEnable(){
        background.alpha = 0;
        background.DOFade(1,0.5f);

        box.localPosition = new Vector2(0,-Screen.height);
        box.transform.DOLocalMoveY(0,0.5f).SetEase(Ease.OutExpo).SetDelay(.1f);
    }

    public void CloseSetting(){
        background.DOFade(0,0.5f);
        box.transform.DOLocalMoveY(-Screen.height,.5f).SetEase(Ease.OutExpo).OnComplete(OnComplete);
    }

    private void OnComplete(){
        gameObject.SetActive(false);
    }
}

