using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : MonoBehaviour
{
    [SerializeField] Transform holePosition;
    [SerializeField] int maxMovesAllowed;

    public Transform HolePosition { get => holePosition; private set => holePosition = value; }
    public int MaxMovesAllowed { get => maxMovesAllowed; private set => maxMovesAllowed = value; }
}
