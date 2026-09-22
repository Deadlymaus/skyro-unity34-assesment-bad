using System.Collections.Generic;
using UnityEngine;

// I will touch Game Manager
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> enemyPrefabs;
    [SerializeField] float spawnEvery;
    private float time;
    [SerializeField] bool paused;
    public int score = 0;
    public bool gameOver;

    private PlayerController player;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        player = FindAnyObjectByType<PlayerController>();

        HUD.instance.UpdateScore("score: " + score);
        HUD.instance.UpdateHP("hp: " + player.hp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
        }

        time -= Time.deltaTime;
        if (time <= 0f)
        {
            time = spawnEvery;
            Spawning();
        }
    }

    public void Spawning()
    {
        Vector3 pos = new Vector3(player.transform.position.x + Random.Range(-3f, 3f), player.transform.position.y + Random.Range(-3f, 3f), 0f);

        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count-1)], pos, Quaternion.identity);
    }

    public void AddScore(int score)
    {
        this.score += score;
        HUD.instance.UpdateScore("score: " + this.score);
    }
}
