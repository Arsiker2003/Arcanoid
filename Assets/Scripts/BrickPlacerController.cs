using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPlacerController : MonoBehaviour
{
    public GameObject brickGroup;
    public GameObject brickPrefab;
    public static int brickAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Vector3 blockPosition = new Vector3(-6.75f, 11f, 0f) + new Vector3(j * 2.25f, i * 1.5f, 0);
                GameObject newBrick = Instantiate(brickPrefab, blockPosition, Quaternion.identity, brickGroup.transform);
                newBrick.tag = brickPrefab.tag;
                
                
                System.Random rnd = new System.Random();
                int num = rnd.Next(0, 4);
                switch (num)
                {
                    case 0:
                        newBrick.GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case 1:
                        newBrick.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case 2:
                        newBrick.GetComponent<Renderer>().material.color = Color.yellow;
                        break;
                    case 3:
                        newBrick.GetComponent<Renderer>().material.color = Color.green;
                        break;
                    default:
                        newBrick.GetComponent<Renderer>().material.color = Color.white;
                        break;
                }
                brickAmount++;
                
            }
        }
    }
}
