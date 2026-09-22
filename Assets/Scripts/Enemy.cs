using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.4f;
    public int hp = 3;
    float hitCooldown;

    private PlayerController player;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (other.GetComponent<PlayerController>() != null)
        {
            if (Time.time < hitCooldown) return;
            hitCooldown = Time.time + 0.4f;

            player.hp -= 3;

            GameManager.instance.AddScore(3);
            HUD.instance.UpdateHP("hp: " + player.hp);
        }

        if (other.gameObject.name.Contains("bullet"))
        {
            hp -= 1;
            Destroy(other.gameObject);
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other != null && other.GetComponent<PlayerController>() != null)
        {
            if (Time.time < hitCooldown) return;
            hitCooldown = Time.time + 0.55f;

            player.hp -= 5;

            GameManager.instance.AddScore(5);
            HUD.instance.UpdateHP("hp: " + player.hp);
        }
    }
}
