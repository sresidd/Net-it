using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class spawner : MonoBehaviour
{
    public event System.Action OnLevelComplete;
    SettingButtons settingButtons;
    UIManager uI;
    Timer timer;
    [SerializeField] GameObject canvas,item,playerParent;
    GameObject player;
    [SerializeField] int spawnNumber,factor,goalsNeeded;

    public int ReturnGoalsNeeded(){
        return goalsNeeded;
    }
    public int ReturnFactor(){
        return factor;
    }
    List<int> factors = new List<int>();
    List<int> notFactors = new List<int>();
    GameObject[] OverlapItems;
    [SerializeField]    List<GameObject> shootingPoints = new List<GameObject>();
    List<GameObject> RandomList = new List<GameObject>();
    [SerializeField] TMP_Text question,score,chancesTxt,compScoreTxt;
    Vector2 initialPlayerPos,_randomPos;
    int compScore,initialScore = 0,chances;
    bool rotatePlayer;
    List<GameObject> temp = new List<GameObject>();

    int starCount,sceneIndex;

    [System.Obsolete]
    void Awake()
    {
        player = playerParent.transform.GetChild(0).gameObject;
        compScore = goalsNeeded-1;
        chances = goalsNeeded+1;
        compScoreTxt.text = compScore.ToString();
        score.text = initialScore.ToString();

        chancesTxt.text = " X " + chances.ToString();
        initialPlayerPos = playerParent.transform.position;
        Factor();
        question.text = "What are the factors of "+ factor.ToString() + " ?";

        
        for(int i = 0;i<goalsNeeded;i++){
            SpawnPoints(factors,factors.Count -1);
            #region 
            // GameObject randomPoint = shootingPoints[Random.Range(0,shootingPoints.Count)];
            // while(temp.Contains(randomPoint))
            // {
            //     randomPoint = shootingPoints[Random.Range(0,shootingPoints.Count)];
            // }
            // temp.Add(randomPoint);
            // Vector2 randomPosition = randomPoint.transform.position;
            // GameObject spot = Instantiate(item, randomPosition, Quaternion.identity) as GameObject;
            // RandomList.Add(spot);
            // spot.transform.localScale = Vector2.one;
            // spot.transform.SetParent(randomPoint.transform,true);
            // spot.transform.localScale = Vector2.one;
            // int randomFactor = factors[(Random.Range(0,factors.Count-1))];
            // spot.transform.GetChild(1).GetComponent<TMP_Text>().text = randomFactor.ToString();
            // spot.GetComponent<movePlayer>().OnGoal+= MoveRandom;
            // spot.GetComponent<movePlayer>().OnShoot += CheckChances;
            // spot.GetComponent<movePlayer>().OnReset+=EnableButtons;
            // factors.Remove(randomFactor);
            #endregion
        }
        for(int i = goalsNeeded;i<spawnNumber;i++){
            SpawnPoints(notFactors,notFactors.Count);
            #region 
            // GameObject randomPoint = shootingPoints[Random.Range(0,shootingPoints.Count)];
            // while(temp.Contains(randomPoint))
            // {
            //     randomPoint = shootingPoints[Random.Range(0,shootingPoints.Count)];
            // }
            // temp.Add(randomPoint);
            // Vector2 randomPosition = randomPoint.transform.position;
            // GameObject spot = Instantiate(item, randomPosition, Quaternion.identity) as GameObject;
            // RandomList.Add(spot);
            // spot.transform.localScale = Vector2.one;
            // spot.transform.SetParent(randomPoint.transform,true);
            // spot.transform.localScale = Vector2.one;
            // int randomFactor = notFactors[(Random.Range(0,notFactors.Count))];
            // spot.transform.GetChild(1).GetComponent<TMP_Text>().text = randomFactor.ToString();
            // spot.GetComponent<movePlayer>().OnShoot += CheckChances;
            // spot.GetComponent<movePlayer>().OnReset+=EnableButtons;
            // notFactors.Remove(randomFactor);
            #endregion
        }

    }

    [System.Obsolete]
    void SpawnPoints(List<int> fac,int count){


        GameObject randomPoint = shootingPoints[Random.Range(0,shootingPoints.Count)];
            while(temp.Contains(randomPoint))
            {
                randomPoint = shootingPoints[Random.Range(0,shootingPoints.Count)];
            }
            temp.Add(randomPoint);
            Vector2 randomPosition = randomPoint.transform.position;
            GameObject spot = Instantiate(item, randomPosition, Quaternion.identity) as GameObject;
            RandomList.Add(spot);
            spot.transform.localScale = Vector2.one;
            spot.transform.SetParent(randomPoint.transform,true);
            spot.transform.localScale = Vector2.one;
            int randomFactor = fac[(Random.Range(0,count))];
            spot.transform.GetChild(1).GetComponent<TMP_Text>().text = randomFactor.ToString();
            
            if(fac == factors)
                spot.GetComponent<movePlayer>().OnGoal+= MoveRandom;
            
            spot.GetComponent<movePlayer>().OnShoot += CheckChances;
            spot.GetComponent<movePlayer>().OnReset+=EnableButtons;
            fac.Remove(randomFactor);
    }


    private void EnableButtons()
    {
                for(int i = 0;i<RandomList.Count;i++){
            RandomList[i].GetComponent<Button>().interactable = true;
        }
        playerParent.transform.rotation = Quaternion.identity;
    }

    void Start(){
        settingButtons = FindObjectOfType<SettingButtons>();
        uI = FindObjectOfType<UIManager>();
        timer = FindObjectOfType<Timer>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    void Update(){
    if(rotatePlayer){
        Vector3 vectorToTarget = (Vector3)_randomPos -playerParent.transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg)-90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        if(angle == -180){
                playerParent.transform.rotation = Quaternion.Slerp(playerParent.transform.rotation, Quaternion.identity, Time.deltaTime * 5f);   

                }
            else{
                playerParent.transform.rotation = Quaternion.Slerp(playerParent.transform.rotation, q, Time.deltaTime * 5f);   

            }
    }
    }
        
    

    private void CheckChances()
    {
        chances--;
        chancesTxt.text = " X " + chances.ToString();
        if((initialScore<goalsNeeded)&&(chances<1)){
            Invoke("GameOver",3f);
            print("gameOver");
        }
        for(int i = 0;i<RandomList.Count;i++){
            RandomList[i].GetComponent<Button>().interactable = false;
        }
    }

    void GameOver(){
        settingButtons.ShowGameOverUI();
    }
    
    [System.Obsolete]
    private void MoveRandom(Vector2 playerPos)
    {
        Loop:
        _randomPos = shootingPoints[Random.RandomRange(0,shootingPoints.Count)].transform.position;
        if(_randomPos == playerPos){
            _randomPos = shootingPoints[Random.RandomRange(0,shootingPoints.Count)].transform.position;
            goto Loop;
        }
        else{
            Invoke("ResetPlayerRotationandPosition",3f);
            rotatePlayer=true;
            playerParent.transform.DOMove(_randomPos,.5f).SetEase(Ease.Linear).OnComplete(()=>InitialPos());
            initialScore += 1;
            score.text = initialScore.ToString();
            if(initialScore>=goalsNeeded){
                Invoke("LevelComplete",2f);
                print("Win");
            }

        }

    }

    void ResetPlayerRotationandPosition(){
        playerParent.transform.DORotate(Vector3.zero,.5f);
        playerParent.transform.DOMove(initialPlayerPos,.5f);
    }
    void LevelComplete()
    {
        // int currentScene = sceneIndex - 1;
        UnlockLevel();
        settingButtons.ShowLevelCompleteUI();
        GameObject stars = uI.GetLevelCompleteUI().transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject;
        if (timer.returnTime() >= 35)
        {
            for (int i = 0; i < 3; i++)
            {
                stars.transform.GetChild(i).gameObject.SetActive(true);
            }
            starCount = 3;
            SceneIntNow(sceneIndex);
            print("3 stars");
        }

        else if (timer.returnTime()<35&&timer.returnTime()>=20){
            for (int i = 0; i < 2; i++)
            {
                stars.transform.GetChild(i).gameObject.SetActive(true);
            }
            starCount = 2;
            SceneIntNow(sceneIndex);
            print("2 stars");
        } 

        else if (timer.returnTime()<20){
            stars.transform.GetChild(0).gameObject.SetActive(true);
            starCount = 1;
            SceneIntNow(sceneIndex);

            print("1 Star");
        }
    }

    private void UnlockLevel()
    {
        if (sceneIndex+1 > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", sceneIndex+1);
        }
    }

    private void SceneIntNow(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                CheckScore("highScore1");
                break;
            case 2:
                CheckScore("highScore2");
                break;
            case 3:
                CheckScore("highScore3");
                break;
            case 4:
                CheckScore("highScore4");
                break;
            case 5:
                CheckScore("highScore5");
                break;
            default:
                print("No scores to update.");
                break;
        }
    }
        void CheckScore(string highscore){
        if(starCount>PlayerPrefs.GetInt(highscore,0))
            PlayerPrefs.SetInt(highscore,starCount);
    }


    void InitialPos(){
        rotatePlayer = false;
    }

    void Factor(){
        for(int i = 1;i<=factor;i++){
            if(factor%i == 0){
                // print(i);
                factors.Add(i);
            }
            else{
                notFactors.Add(i);
            }
        }
    }

}


