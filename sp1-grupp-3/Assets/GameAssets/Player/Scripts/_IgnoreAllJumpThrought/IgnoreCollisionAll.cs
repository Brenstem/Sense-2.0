using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionAll : MonoBehaviour
{
    List<GameObject> ignoreObjects = new List<GameObject>();
    List<GameObject> secondIgnore = new List<GameObject>();
    
    public List<string> layerNames = new List<string>();
    public string secondLayerName;

    void Awake()
    {
        ignoreObjects = FindGameObjectsWithLayer();
        secondIgnore = FindGameObjectsWithLayerWithSecondLayer();

        for(int i = 0; i < ignoreObjects.Count; i++)
        {
            for(int j = 0; j < secondIgnore.Count; j++)
            {
                if(ignoreObjects[i].GetComponent<Collider2D>() != null && secondIgnore[j].GetComponent<Collider2D>() != null)
                {
                    Physics2D.IgnoreCollision(ignoreObjects[i].GetComponent<Collider2D>(), secondIgnore[j].GetComponent<Collider2D>(), true);
                }
            }
        }
    }

    List<GameObject> FindGameObjectsWithLayer()
    {
        var lias = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();

        for (int i = 0; i < lias.Length; i++)
        {
            for(int j = 0; j < layerNames.Count; j++)
            {   
                if (lias[i].layer == LayerMask.NameToLayer(layerNames[j]))
                {
                    goList.Add(lias[i]);
                }
            }
        }
        return goList;
    }

    List<GameObject> FindGameObjectsWithLayerWithSecondLayer()
    {
        var lias = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();

        for (int i = 0; i < lias.Length; i++)
        {
            if (lias[i].layer == LayerMask.NameToLayer(secondLayerName))
            {
                goList.Add(lias[i]);
            }
        }
        return goList;
    }
}