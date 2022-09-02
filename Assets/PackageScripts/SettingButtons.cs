using UnityEngine;

public class SettingButtons : MonoBehaviour
{
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();  
    }

    public void ShowSettingsUI()
    {
        uiManager.GetSettingsUI().SetActive(true);
    }

    public void ShowGameOverUI(){
        uiManager.GetGameOverUI().SetActive(true);
    }

    public void ShowLevelCompleteUI(){
        uiManager.GetLevelCompleteUI().SetActive(true);
    }

    public void ShowPauseUI(){
        uiManager.GetPauseUI().SetActive(true);
    }

    public void HideSettingsUI()
    {
        uiManager.GetSettingsUI().GetComponent<UITween>().CloseSetting();
    }
        public void HideGameOverUI()
    {
        uiManager.GetGameOverUI().GetComponent<UITween>().CloseSetting();
    }    
    
    public void HideLevelCompleteUI(){
        
        // GameObject star = uiManager.GetLevelCompleteUI().transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;
        // for(int i=0;i<3;i++){
        //     star.transform.GetChild(i).gameObject.SetActive(false);
        // }
        uiManager.GetLevelCompleteUI().GetComponent<UITween>().CloseSetting();

    }
    public void HidePauseUI()
    {
        uiManager.GetPauseUI().GetComponent<UITween>().CloseSetting();
    }
    // }    public void HideLevelStartUI()
    // {
    //     uiManager.GetLevelStartUI().GetComponent<LevelStartTween>().CloseSetting();
    // }
}
