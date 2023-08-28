using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkAlgorithmParams_", menuName = "RandomWalk/RandomWalkData")]
public class RandomWalkParams : ScriptableObject
{
    public int iterations = 10, walkLenght = 10;

    public bool startRandomlyEachIteration = true;
}
