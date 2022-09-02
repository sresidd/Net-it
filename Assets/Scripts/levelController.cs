using UnityEngine;
using UnityEngine.UI;

public class levelController : MonoBehaviour
{
    public Button[] lvlButtons;
    public GameObject[] lev1;
    public GameObject[] lev2;
    public GameObject[] lev3;
    public GameObject[] lev4;
    public GameObject[] lev5;
    private void Awake()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }

        
        int highScore1 = PlayerPrefs.GetInt("highScore1",0);
        for(int i =  0;i<highScore1;i++){
            lev1[i].SetActive(true);
        }
        int highScore2 = PlayerPrefs.GetInt("highScore2",0);
        for(int i =  0;i<highScore2;i++){
            lev2[i].SetActive(true);
        }
        int highScore3 = PlayerPrefs.GetInt("highScore3",0);
        for(int i =  0;i<highScore3;i++){
            lev3[i].SetActive(true);
        }
        int highScore4 = PlayerPrefs.GetInt("highScore4",0);
        for(int i =  0;i<highScore4;i++){
            lev4[i].SetActive(true);
        }
        int highScore5 = PlayerPrefs.GetInt("highScore5",0);
        for(int i =  0;i<highScore5;i++){
            lev5[i].SetActive(true);
        }
    }

    public void DeleteAllData(){
        PlayerPrefs.DeleteAll();
        FindObjectOfType<LevelManager>().ReloadScene();
    }
}
