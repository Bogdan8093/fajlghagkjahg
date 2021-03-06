﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    public static GameTimer current; //A public static reference to itself (make's it visible to other objects without a reference)
#pragma warning disable 0649
    private float startTime;
#pragma warning restore 0649
    private float timerValue;
#pragma warning disable 0414
    private float startTimerValue = 0.0f;
#pragma warning restore 0414
    public bool timerStop = true;
    public float timerSpeed = 1.0f;

    void Awake() {
        //Ensure that there is only one object pool
        if (current == null)
            current = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float t = Time.time - startTime;
#pragma warning disable 0219
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
#pragma warning restore 0219

        if (!timerStop) {
            timerValue -= Time.deltaTime * timerSpeed;
            if (timerValue <= 0.0f) {
                startTimerValue = 0.0f;
                timerValue = 0.0f;
                timerStop = true;
                timerSpeed = 1.0f;
            }
        }
        // Debug.Log("Timer: " + getTimerValue());
    }

    public void setStartTimerValue(float timerValue) {
        this.startTimerValue = timerValue;
        this.timerValue = timerValue;
    }

    public float getTimerValue() {
        return timerValue;
    }
    public void stopTimer(bool start) {
        timerStop = start;
    }
}