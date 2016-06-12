using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CrowdSpawner : MonoBehaviour
{

    public GameObject template;
    public int quantity = 20;
    public float maxWidthHeight = 26;

    void Start()
    {

    }

    public void SpawnCrowd()
    {
        int row = 0;
        int col = 0;
        int maxCol = (int)Mathf.Sqrt(quantity);
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(template, new Vector3(col, 0, row) + transform.position, Quaternion.identity);
            col++;
            if (col == maxCol)
            {
                col = 0;
                row++;
            }
        }
    }

}