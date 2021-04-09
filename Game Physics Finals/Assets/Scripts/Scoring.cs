﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Scoring : MonoBehaviour
{
    public int score = 0;
    public Text text;
    private int moveLevel = 5;
    private float[] arrowAngles = {0, 0, -30, 30, 0};

    public GameObject[] balls;
    public GameObject Sphere;
    public GameObject Arrow;

    public Transform[] waypoints;
    public Transform BallWaypoint;

    public GameObject BonusShot;
    public GameObject Character;
    public GameObject BallObject;

    public GameObject FadeEndUI;
    public Rigidbody rb;
    public int shots = 6;
    public Text shotText;

    public GameObject ClickStoper;

    // Start is called before the first frame update
    void Start()
    {
        rb = BallObject.GetComponent<Rigidbody>();
        Text text = GetComponent<Text>();
        BonusShot.SetActive(false);
    }

    void Update() {
        Cursor.visible = false;
        shotText.text = shots.ToString();
        // if (Input.GetMouseButtonDown(0)){
        //     StartCoroutine(ClickedResetTimer());
        // }
    }
    public void Clicked(){
        StartCoroutine(ClickedResetTimer());
    }
    // Update is called once per frame

    void OnTriggerEnter(Collider collision) {
        if(collision.transform.name == "Ball") {
        
            score = score + 25;
            moveLevel -= 1;

            switch(moveLevel){
                case 0:
                    StartCoroutine(MoveLevel());
                    break;
                case 5:
                    foreach(GameObject ball in balls) ball.SetActive(false);
                    break;
                default:
                    if(moveLevel == 1) BonusShot.SetActive(true);

                    balls[moveLevel-1].SetActive(true);
                    Character.transform.position = waypoints[moveLevel-1].position;
                    Character.transform.rotation = waypoints[moveLevel-1].rotation;
                    rb.isKinematic = true;
                    BallObject.transform.position = BallWaypoint.position;
                    BallObject.transform.rotation = BallWaypoint.rotation;
                    rb.isKinematic = false;
                    BallObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                    Arrow.transform.position = BallWaypoint.position;
                    Arrow.transform.rotation = Quaternion.Euler(0, arrowAngles[moveLevel-1], 0);
                    print(Arrow.transform.rotation.eulerAngles);
                    break;
            }

            text.text = score.ToString();
        }
    }
    IEnumerator MoveLevel (){
        FadeEndUI.SetActive(true);
        BonusShot.SetActive(false);
        Cursor.visible = true;
        ClickStoper.SetActive(true);
        yield return new WaitForSeconds(3);
        ClickStoper.SetActive(false);
        SceneManager.LoadScene(2);
    }
    IEnumerator ClickedResetTimer (){
        if (shots == 0)
        {
            SceneManager.LoadScene("GameLose");
        }
        else{
            shots--;
            Cursor.visible = true;
            ClickStoper.SetActive(true);
            yield return new WaitForSeconds(3);
            ClickStoper.SetActive(false);
            rb.isKinematic = true;
            BallObject.transform.position = BallWaypoint.position;
            BallObject.transform.rotation = BallWaypoint.rotation;
            rb.isKinematic = false;
        }
    }
}
