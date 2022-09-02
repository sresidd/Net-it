using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    SceneTransition sceneTrasition;
    GameObject trasitionImage;

    void Start(){
        sceneTrasition = FindObjectOfType<SceneTransition>();
        trasitionImage = sceneTrasition.GetTrasitionImage();
    }
    
    public void ChangeScene(int sceneIndex)
    {
        // SceneManager.LoadScene(sceneIndex);
        trasitionImage.SetActive(true);
        trasitionImage.transform.localPosition = new Vector2(-Screen.width,trasitionImage.transform.localPosition.y);
        trasitionImage.transform.DOMove(Vector2.zero,.5f).OnComplete(()=>{EndTrasition(sceneIndex);});
    }

    private void EndTrasition(int _index)
    {
        SceneManager.LoadScene(_index);
        trasitionImage.transform.DOLocalMoveX(Screen.width,.5f).OnComplete(()=>sceneTrasition.GetTrasitionImage().SetActive(false));
    }

    // IEnumerator SceneChange(int _index)
    // {
    //     yield return new WaitForSecondsRealtime(.5f);
    //     print("current index: " + _index);

        
    // }

    public void ReloadScene()
    {
        ChangeScene(ReturnCurrentSceneIndex());
    }

    public void NextScene(){
        ChangeScene(ReturnNextSceneIndex());
    }

    public int ReturnCurrentSceneIndex(){
        return SceneManager.GetActiveScene().buildIndex;
    }
    

    public int ReturnNextSceneIndex(){
        return ReturnCurrentSceneIndex()+1;
    }

    public void Home(){
        ChangeScene(0);
    }

}

