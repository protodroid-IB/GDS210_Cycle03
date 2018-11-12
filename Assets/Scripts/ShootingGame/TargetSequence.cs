using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetSequence
{
    public List<Sequence> sequence;
}

[System.Serializable]
public class Sequence
{
    public float timeInterval;
    public float targetTime;
    public float exitTime;
    public bool randomize;
    public List<int> targetID;
}
