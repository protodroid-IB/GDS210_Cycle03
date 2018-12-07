using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SequencePool
{
    public List<TargetSequence> pool;
}

[System.Serializable]
public class TargetSequence
{
    public string name;
    public List<SubSequence> sequence;
}

[System.Serializable]
public class SubSequence
{
    public float timeInterval;
    public float targetTime;
    public float exitTime;
    public bool randomize;
    public List<int> targetID;
}
