using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    private GameObject repetidor;

    private void Start()
    {
        repetidor = Instantiate(GameManager.instance.repetidorPrefab);
        repetidor.SetActive(false);
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ActivatePrefab(clickWorldPos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickWorldPos, Vector2.up, 0.1f);

            if (hit)
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    void ActivatePrefab(Vector3 pos)
    {
        repetidor.transform.position = new Vector3(pos.x, pos.y, 0);
        repetidor.SetActive(true);
    }
}
