using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int Health;
    private int numberOfWoodArrow;
    private int numberOfFlinstone;

    public int GetHealth()
    {
        return Health;
    }
    public int GetNumberOfWoodArrow()
    {
        return numberOfWoodArrow;
    }
    public int GetNumberOfFlinstone()
    {
        return numberOfFlinstone;
    }
}
