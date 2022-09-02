using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// using Chimpvine.WebClient;
public class Timer : MonoBehaviour
{
    // [SerializeField]Image _time;
    public event Action OnTimerEnd;

    LevelManager levelManager;
    SettingButtons setting;
    UIManager uI;
    ListAudio listAudio;
    TMP_Text text;
    float _timeRemaining = 61f;
    float _timerFullTime;
    bool _timerIsRunning = false;
    int timeIndex;

    public int returnTime()
    {
        return timeIndex;
    }

    public void PauseTimer(bool time)
    {
        _timerIsRunning = time;
    }

    private void Start()
    {
        listAudio = FindObjectOfType<ListAudio>();
        setting = FindObjectOfType<SettingButtons>();
        levelManager = FindObjectOfType<LevelManager>();
        _timerFullTime = _timeRemaining;
        // _time.fillAmount = 1;   
        text = GetComponent<TMP_Text>();
        // text.text = _timerFullTime.ToString();
        _timerIsRunning = true;
        FindObjectOfType<spawner>().OnLevelComplete+=PauseTimerOnEnd;
        // FindObjectOfType<Checker>().OnLevelStart += OnLevelStart;
        // Starts the timer automatically
    }

    private void PauseTimerOnEnd()
    {
        _timerIsRunning = false;
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {                
                // _time.fillAmount = (_timeRemaining/_timerFullTime);
                // print(_time.fillAmount);
                timeIndex = (int)_timeRemaining;                
                text.text = timeIndex.ToString();
                              
                // print(timeIndex);
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = 0;
               _timerIsRunning = false;
                // OnTimerEnd?.Invoke();
                setting.ShowGameOverUI();
                // // ChimpvineRestClient.SendGameUpdateRequest(levelManager.ReturnCurrentSceneIndex().ToString(),0);
                // listAudio.PlayAudioWithOneShot(41);

                // int _sceneIndex;
                // _sceneIndex = levelManager.ReturnCurrentSceneIndex();
                // switch(_sceneIndex){
                // case 1: 
                //     ShowHighScore("highScore1");
                //     break; 
                // case 2:
                //     ShowHighScore("highScore2");
                //     break;
                // case 3:
                //     ShowHighScore("highScore3");
                //     break;
                // case 4:
                //     ShowHighScore("highScore4");
                //     break;
                // case 5:
                //     ShowHighScore("highScore5");
                //     break;
                // default:
                //     print("No scores to update.");
                //     break;
                }
            }
        }
    }
    // void ShowHighScore(string _name){
    //     GameObject.FindGameObjectWithTag("hscore").GetComponent<TMP_Text>().text = "HIGH SCORE: " + PlayerPrefs.GetInt(_name).ToString();
    // }

    // private void OnLevelStart(){
    //     _timerIsRunning = true;
    // }
