using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoB : MonoBehaviour {

    public float secondsToLoseSignal = 2;
    public float loseSignalRate = 2;
    public float currSignal = 0;
    public float maxSignal = 100f;  //Change fields to private
    [Range(1f, 50f)]
    public float multiplier = 1f;
    private float signalMultiplier;

    public bool isReceivingSignal = false;

    private void Start()
    {
        InvokeRepeating("Timer", 0, secondsToLoseSignal);
    }

    private void Update()
    {
         if(!isReceivingSignal)
        {
            currSignal -= 0.1f * loseSignalRate;
        }
        currSignal = Mathf.Clamp(currSignal, 0, maxSignal);
        if (currSignal == maxSignal)
        {
            GameManager.instance.NextPhase();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Sinal")
        {
            isReceivingSignal = true;
            signalMultiplier = other.GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
            currSignal += 0.1f * signalMultiplier * multiplier;
        }
    }

    private void Timer()
    {
        isReceivingSignal = false;
    }
}
