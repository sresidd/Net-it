using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine.UI;

public class movePlayer : MonoBehaviour
{
    ListAudio listAudio;
    public event Action<Vector2> OnGoal;
    public event Action OnShoot,OnReset;
    Rigidbody2D rigBall,rigPlayer;
    GameObject playerParent,player,ball,parent;
    Animator playerAnim,ballAnim;
    static int chances;
    bool rotatePlayer;
    spawner _spawner;
    Vector2 initialBallPos,initialBallScale,initialPlayerPos;
    
    void Start()
    {
        playerParent = GameObject.FindGameObjectWithTag("Player");
        rigPlayer = playerParent.GetComponent<Rigidbody2D>();
        player = playerParent.transform.GetChild(0).gameObject;
        playerAnim = player.GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("ball");
        ballAnim = ball.GetComponent<Animator>();
        parent = GameObject.FindGameObjectWithTag("parent");
        rigBall = ball.GetComponent<Rigidbody2D>();
        _spawner = FindObjectOfType<spawner>();
        listAudio = FindObjectOfType<ListAudio>();
        initialBallPos = ball.transform.position;
        initialPlayerPos = playerParent.transform.position;
        ball.transform.position = initialBallPos;

    }

    public void Jump(){
        playerAnim.SetBool("diving",true);
        listAudio.PlayAudioWithOneShot(UnityEngine.Random.Range(0,4));
        ball.transform.position = initialBallPos;
        rigBall.bodyType = RigidbodyType2D.Static;
        ball.transform.DOScale(Vector2.one*.5f,.8f).SetEase(Ease.OutSine);
        ballAnim.SetBool("spin",true);
        Invoke("RespawnBall",3f);
        if((_spawner.ReturnFactor()%Int16.Parse(transform.GetChild(1).GetComponent<TMP_Text>().text))!= 0){
            rotatePlayer = true;
            playerParent.transform.DOMove(transform.position,.5f).SetEase(Ease.Linear).OnComplete(()=>OnFalse());
            ball.transform.DOJump(transform.position,.8f,1,.5f).SetEase(Ease.OutSine);
            ball.transform.SetParent(player.transform,true);
            rigBall.bodyType = RigidbodyType2D.Dynamic;
        }
        else{

            ball.transform.DOJump(transform.position,.8f,1,.5f).SetEase(Ease.OutSine).OnComplete(()=>EnableRigBd());
            ball.transform.SetParent(parent.transform,true);
            OnGoal?.Invoke(transform.position);
        }
        OnShoot?.Invoke();
    }

    private void OnFalse()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        rigPlayer.bodyType = RigidbodyType2D.Dynamic;
        ballAnim.SetBool("spin",false);
        rotatePlayer = false;
        listAudio.PlayAudioWithOneShot(UnityEngine.Random.Range(11,14));
        listAudio.PlayAudioWithOneShot(15);
        Invoke("SetImapactInactive",.5f);
        // if(playerParent.transform.rotation.z>10)
        // {
        //     playerParent.transform.DORotate(new Vector3(0,0,90),.5f);
        // }
        // else if(playerParent.transform.rotation.z<-10)
        // {
        //     playerParent.transform.DORotate(new Vector3(0,0,-90),.5f);
        // }
    }
    void SetImapactInactive(){
            gameObject.transform.GetChild(2).gameObject.SetActive(false);

    }

    void RespawnBall(){
        rigPlayer.bodyType= RigidbodyType2D.Static;
        playerParent.transform.DORotate(Vector3.zero,.5f);
        playerParent.transform.DOMove(initialPlayerPos,.5f);
        playerAnim.SetBool("diving",false);
        rigBall.bodyType = RigidbodyType2D.Static;
        ballAnim.SetBool("spin",true);
        ball.transform.DOJump(initialBallPos,2f,1,1f).SetEase(Ease.Linear).OnComplete(()=>OnReset.Invoke());
        ball.transform.DOScale(Vector2.one,1f).SetEase(Ease.Linear).OnComplete(()=>ballAnim.SetBool("spin",false));
    }

    void EnableRigBd(){
        listAudio.PlayAudioWithOneShot(16);
        rigPlayer.bodyType = RigidbodyType2D.Dynamic;
        ballAnim.SetBool("spin",false);
        listAudio.PlayAudioWithOneShot(UnityEngine.Random.Range(4,8));
        listAudio.PlayAudioWithOneShot(14);
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        rigBall.bodyType = RigidbodyType2D.Dynamic;
        Invoke("PlayDropSound",.1f);
        
    }

    void Update(){
        if(rotatePlayer){
            Vector3 vectorToTarget = transform.position -playerParent.transform.position;
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

    void PlayDropSound(){
        listAudio.PlayAudioWithOneShot(UnityEngine.Random.Range(8,11));
    }
}
