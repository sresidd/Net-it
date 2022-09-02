using UnityEngine;
using DG.Tweening;

public class levelTween : MonoBehaviour
{
    [SerializeField] CanvasGroup backgroundImage;
    [SerializeField] GameObject LevelSelection;
    [SerializeField] GameObject home;

    public void OnEnable(){
        LevelSelection.transform.localScale = Vector3.zero;  
        gameObject.transform.localPosition = new Vector3(Screen.width+200,0,0); 
        home.transform.localPosition = Vector3.zero; 
        backgroundImage.alpha = 0;
        home.transform.DOLocalMoveX(-Screen.width-200,0.5f).SetEase(Ease.InQuad).OnComplete(ActiveSelectionScreen);
        transform.DOLocalMoveX(0,0.5f).SetEase(Ease.InQuad);
        LevelSelection.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.InQuad);
              
    }
    void ActiveSelectionScreen(){ 
        backgroundImage.DOFade(1,.2f);
        home.SetActive(false);
    }

    public void OnClose(){ 
        home.SetActive(true);       
        LevelSelection.transform.DOScale(Vector3.zero,0.5f).OnComplete(OnComplete);    
        backgroundImage.DOFade(0,.5f);   
        home.transform.DOLocalMoveX(0,0.5f).SetEase(Ease.InQuad);   
    }

    void OnComplete(){  
        gameObject.transform.DOLocalMoveX(Screen.width+200,0.5f).SetEase(Ease.InQuad);        
        gameObject.SetActive(false);
    }
}
