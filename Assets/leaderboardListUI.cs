using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class leaderboardListUI : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text score;

    public void SetData(string name,string score)
    {
        playerName.text = name;
        this.score.text = score;
    }
}
