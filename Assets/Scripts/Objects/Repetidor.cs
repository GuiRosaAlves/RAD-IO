using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public enum ColliderInfo { Null, Signal, Interference };
public class Repetidor : MonoBehaviour, IBeginDragHandler
{
    protected GameObject particuleReference;
    public Repetidor fatherReference;
    public float secondsToLoseSignal = 2;
    protected ColliderInfo curCollisionInfo = ColliderInfo.Null;
    protected bool canCollide = true;
    public List<Repetidor> childsReferences = new List<Repetidor>();


    protected void Awake()
    {
        InvokeRepeating("CheckInterference", 0, 0.3f);
    }

    void OnMouseDown()
    {
        GetComponent<Collider2D>().enabled = false;

        DestroyReferences();

        canCollide = false;
    }

    protected void OnMouseDrag()
    {
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }

    void DestroyReferences()
    {
        if (particuleReference != null)
        {
            Destroy(particuleReference);
            particuleReference = null;
            foreach (var r in childsReferences)
            {
                r.DestroyReferences();
            }
            childsReferences = new List<Repetidor>();
            fatherReference = null;
        }
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
            if(coll.transform.parent.GetComponent<Repetidor>().fatherReference != this)
            {
                fatherReference = coll.transform.parent.GetComponent<Repetidor>();
                fatherReference.childsReferences.Add(this);
            }
        }
        else
        {
            fatherReference = null;
        }
    }

    protected void CheckInterference()
    {
        if (curCollisionInfo == ColliderInfo.Interference)
            curCollisionInfo = ColliderInfo.Null;
    }

    protected void CheckConnection()
    {
        if (fatherReference && fatherReference.particuleReference == null)
            Destroy(particuleReference);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("onbein");
    }
}
