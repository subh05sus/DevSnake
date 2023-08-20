using UnityEngine;
using UnityEngine.UI;

public class TotalScoreManager : MonoBehaviour
{
    public Text totalScoreText;

    private void Start()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        totalScoreText.text = "You Scored: " + totalScore;
    }
}
