using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repetidor : MonoBehaviour
{
    protected GameObject particuleReference;
    protected Repetidor fatherReference;
    public float secondsToLoseSignal = 2;
    protected enum ColliderInfo { Signal, Interference };
    protected ColliderInfo curCollisionInfo = default(ColliderInfo);
    protected bool canCollide = true;


    protected void Awake()
    {
        InvokeRepeating("CheckInterference", 0, 0.3f);
    }

    protected void OnMouseDrag()
    {
        if (particuleReference != null)
        {
            Destroy(particuleReference);
            particuleReference = null;
        }

        canCollide = false;
        GetComponent<Collider2D>().enabled = false;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }

    protected void OnMouseUp()
    {
        canCollide = true;
        GetComponent<Collider2D>().enabled = true;
    }

    protected void Update()
    {
        CheckConnection();
    }

    protected void OnParticleCollision(GameObject coll)
    {
        if (!canCollide)
            return;

        if (coll.transform.IsChildOf(transform))
            return;

        if (curCollisionInfo == ColliderInfo.Interference)
            return;

        if (coll.tag == "Interferencia")
        {
            curCollisionInfo = ColliderInfo.Interference;
            if (particuleReference == null)
            {
                AddParticle(coll);
            }
            else if (particuleReference.tag == "Sinal")
            {
                Destroy(particuleReference);
                AddParticle(coll);
            }
        }
        else if (coll.tag == "Sinal")
        {
            if (particuleReference == null)
            {
                AddParticle(coll);
            }
            else if (particuleReference.tag == "Interferencia")
            {
                Destroy(particuleReference);
                AddParticle(coll);
            }
        }
    }

    protected void OnDisable()
    {
        if (particuleReference != null)
        {
            Destroy(particuleReference);
        }
    }

    protected virtual void AddParticle(GameObject coll)
    {
        particuleReference = Instantiate(coll);
        particuleReference.transform.SetParent(transform);
        particuleReference.transform.localPosition = Vector3.zero;
        if (coll.transform.parent.GetComponent<Repetidor>())
        {
            fatherReference = coll.transform.parent.GetComponent<Repetidor>();
        }
        else
        {
            fatherReference = null;
        }
    }

    protected void CheckInterference()
    {
        if (curCollisionInfo == ColliderInfo.Interference)
            curCollisionInfo = default(ColliderInfo);
    }

    protected void CheckConnection()
    {
        if (fatherReference && fatherReference.particuleReference == null)
            Destroy(particuleReference);
    }
}
