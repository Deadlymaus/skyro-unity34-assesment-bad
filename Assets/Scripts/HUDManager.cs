using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public TMP_Text scoreText;
    public TMP_Text hpText;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void UpdateScore(string score)
    {
        if (scoreText == null) { print("There is no scoreText"); return;}

        scoreText.text = score;
    }

    public void UpdateHP(string hp)
    {
        if (hpText == null) { print("There is no hpText"); return;}

        hpText.text = hp;

    }
}
