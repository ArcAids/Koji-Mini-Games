using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    [SerializeField] leaderboardListUI leaderboardListUI;
    [SerializeField] Transform listParent;

    private void OnEnable()
    {
        CreateList(null) ;
    }

    public void CreateList(List<(string name,string score)> data)
    {
        if (data == null) return;
        foreach (var entry in data)
        {
            Instantiate(leaderboardListUI, listParent).SetData(entry.name,entry.score);
        }
    }
}
