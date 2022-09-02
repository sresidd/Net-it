using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    // public event System.Action OnPointer;
    [SerializeField] TMP_Text dialogue;
    [SerializeField] string[] tutorialDialogues;

    int index;
    [SerializeField] float typingSpeed;
    // [SerializeField] GameObject box,GameBox,controller,FinalBox;

    ListAudio listAudio;
    // Start is called before the first frame update
    void Start()
    {
        listAudio = FindObjectOfType<ListAudio>();
        // controller.GetComponent<balloonControllerTutorial>().OnCountZero+=Finish;
        listAudio.PlayAudioWithOneShot(7);
        StartCoroutine(Types());
    }

    private void Finish()
    {
        listAudio.PlayAudioWithOneShot(16);
        // GameBox.SetActive(false);
        // FinalBox.SetActive(true);
        Invoke("GoHome",4f);

    }

    void GoHome(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator Types()
    {
        foreach(char letter in tutorialDialogues[index].ToCharArray()){
            dialogue.text += letter;
            if(dialogue.text == tutorialDialogues[index])
            {

                Invoke("NewMethod",2f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void NewMethod()
    {
        if (index < tutorialDialogues.Length - 1)
        {
            index++;
            dialogue.text = "";
            listAudio.PlayAudioWithOneShot(7+index);
            StartCoroutine(Types());
        }
        else
        {
            // box.transform.DOLocalMoveY(Screen.height,.5f).OnComplete(()=>{
            //     listAudio.PlayAudioWithOneShot(13);
            //     box.SetActive(false);
            //     GameBox.SetActive(true);
            //     GameBox.GetComponent<CanvasGroup>().alpha = 0f;
            //     GameBox.GetComponent<CanvasGroup>().DOFade(1f,.5f);
            //     controller.SetActive(true);
                // g2.SetActive(true);
                // Invoke("PlaySound",5f);
        // });


        }
    }
    

    // void PlaySound(){
    //     listAudio.PlayAudioWithOneShot(14);
    //     Invoke("ShowPointer",3f);
    // }

    void ShowPointer(){
        // OnPointer?.Invoke();
    }
}
