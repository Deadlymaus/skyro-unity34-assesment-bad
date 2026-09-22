using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class PlayerController: MonoBehaviour
{
    [SerializeField] float speed = 5.5f;
    [SerializeField] float shootingSpeed = 0.18f;

    public GameObject bulletPrefab;
    public int hp = 37;

    float lastShot;

    void Update()
    {
        GameOver();
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && Time.time > lastShot + shootingSpeed)
        {
            lastShot = Time.time;
            StartCoroutine(shoot());
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    IEnumerator shoot()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.name = "bullet";

        Destroy(bullet, 2);
        while (bullet != null)
        {
            bullet.transform.position += transform.rotation * Vector2.up * 2 * Time.fixedDeltaTime;
            yield return null;
        }
    }

    void GameOver()
    {
        if(hp <= 0)
        {
            GameManager.instance.gameOver = true;
            Time.timeScale = 0f;
            EditorApplication.ExitPlaymode();
        }
    }
}
