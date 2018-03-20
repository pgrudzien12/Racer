using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var prefab = GameObject.FindWithTag("Respawn");
        prefab.SetActive(false);
        int maxObjects = 1;
        float translation = 0.1f;
        float halfway = translation * (maxObjects / 2);
        for (int i = 0; i < maxObjects; i++)
        {
            var newone = Instantiate(prefab);
            newone.transform.Translate(new Vector3((i * translation) - halfway, 0));
            newone.GetComponentInChildren<Renderer>().material.color = Color.magenta;
            newone.SetActive(true);
        }

        prefab.GetComponentInChildren<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
