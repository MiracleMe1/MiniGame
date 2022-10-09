using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CheckPointsSystem : MonoBehaviour
{
    public int numOfCheckPoints=0;
    public GameObject[] checkPoints;

    private int currentCheckPoint = 0;

    public int getCurrentCheckPoint()
    {
        return currentCheckPoint;
    }

}
