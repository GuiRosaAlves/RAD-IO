using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderIntrerference : MonoBehaviour {

    public List<ParticleSystem> signals = new List<ParticleSystem>();
    public List<GameObject> interferences = new List<GameObject>();

    // Use this for initialization
    void Start () {
        foreach (var o in GameObject.FindGameObjectsWithTag("Sinal"))
        {
            signals.Add(o.GetComponent<ParticleSystem>());
        }
        foreach (var o in GameObject.FindGameObjectsWithTag("Cilindro"))
        {
            interferences.Add(o);
        }
        AddToTrigger();
	}

    void AddToTrigger()
    {
        foreach (var p in signals)
        {
            for (int i = 0; i < p.trigger.maxColliderCount; i++)
            {
                p.trigger.SetCollider(i, null);
            }
            for (int i = 0; i < interferences.Count; i++)
            {
                p.trigger.SetCollider(i, interferences[i].GetComponent<Collider>().transform);
            }
            
        }
    }
}
