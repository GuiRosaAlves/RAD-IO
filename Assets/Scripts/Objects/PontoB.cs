﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PontoB : MonoBehaviour
{
    //UI Stuff
    public static UIManager instance;
    public Image SignalBar;
    public Color signalGreenColor = Color.green;
    public Color signalRedColor = Color.red;

    public ColliderInfo collisionInfo;

    public float secondsToLoseSignal = 2;
    public float loseSignalRate = 2;
    public bool doesInterferenceWin = false;
    private float currSignal = 0;
    private float maxSignal = 100f;
    [Range(1f, 50f)]
    public float multiplier = 1f;
    private float signalMultiplier;
    private float winCondition { get { return ((doesInterferenceWin) ? -maxSignal : maxSignal); } }
    private bool isTheBarFull = false;
    public bool isReceivingSignal = false;

    private void Start()
    {
        SwapScenesAux.instance.winCounter = (FindObjectsOfType<PontoB>().Length);
        InvokeRepeating("Timer", 0, secondsToLoseSignal);
    }

    private void Update()
    {
        float aux = 0;

        if (isReceivingSignal)
        {
            if (collisionInfo == ColliderInfo.Interference)
            {
                currSignal -= 0.1f * loseSignalRate * multiplier;
            }
            else if (collisionInfo == ColliderInfo.Signal)
            {
                currSignal += 0.1f * signalMultiplier * multiplier;
            }
        }
        else
        {
            if (currSignal < 0)
            {
                currSignal += 0.1f * loseSignalRate;
            }
            else
            {
                currSignal -= 0.1f * loseSignalRate;
            }
        }

        SignalBar.color = (currSignal < 0) ? signalRedColor : signalGreenColor;
        aux = (currSignal < 0) ? -100 : 0;
        SignalBar.fillAmount = (currSignal < 0) ? (currSignal * -1 / maxSignal) : (currSignal / maxSignal);

        currSignal = Mathf.Clamp(currSignal, aux, maxSignal);

        if (currSignal == winCondition)
        {
            if (isTheBarFull == false)
            {
                SwapScenesAux.instance.NextScene();
                isTheBarFull = true;
            }
        }
        else
        {
            if(isTheBarFull == true)
            {
                SwapScenesAux.instance.winCounter += 1;
                isTheBarFull = false;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Sinal")
        {
            isReceivingSignal = true;
            signalMultiplier = other.GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
            collisionInfo = ColliderInfo.Signal;
        }

        if (other.tag == "Interferencia")
        {
            isReceivingSignal = true;
            signalMultiplier = other.GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
            collisionInfo = ColliderInfo.Interference;
        }
    }

    private void Timer()
    {
        isReceivingSignal = false;
        collisionInfo = ColliderInfo.Null;
    }
}
