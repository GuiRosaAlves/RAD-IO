using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repetidor : MonoBehaviour
{
    protected GameObject reference;
    public float secondsToLoseSignal = 2;
    public bool isReceivingInterference = false;
    private enum ColliderInfo { Signal, Interference };
    private ColliderInfo curCollisionInfo = default(ColliderInfo);

    private void Awake()
    {
        InvokeRepeating("CheckInterference", 0, 0.3f);
        InvokeRepeating("DisableParticule", 0, 0.5f);
    }

    private void OnParticleCollision(GameObject coll)
    {
        if (coll.transform.IsChildOf(transform))
            return;

        if (curCollisionInfo == ColliderInfo.Interference)
            return;

        if (coll.tag == "Interferencia")
        {
            curCollisionInfo = ColliderInfo.Interference;
            if (reference == null)
            {
                AddParticle(coll);
            }
            else if (reference.tag == "Sinal")
            {
                Destroy(reference);
                AddParticle(coll);
            }
        }
        else if (coll.tag == "Sinal")
        {
            if (reference == null)
            {
                AddParticle(coll);
            }
            else if (reference.tag == "Interferencia")
            {
                Destroy(reference);
                AddParticle(coll);
            }
        }
    }

    private void OnDisable()
    {
        if (reference != null)
        {
            Destroy(reference);
        }
    }

    public virtual void AddParticle(GameObject coll)
    {
        reference = Instantiate(coll);
        reference.transform.SetParent(transform);
        reference.transform.localPosition = Vector3.zero;
    }

    public void CheckInterference()
    {
        if (curCollisionInfo == ColliderInfo.Interference)
            curCollisionInfo = default(ColliderInfo);
    }

    public void DisableParticule()
    {
        if (curCollisionInfo == default(ColliderInfo))
        {
            Destroy(reference);
            reference = null;
        }
    }
}
