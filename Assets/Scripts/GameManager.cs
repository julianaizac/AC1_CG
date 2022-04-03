using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject hazardPrefab;
    public int maxHazardToSpawn = 3;

    public TMPro.TextMeshPro scoreText;

    public int score;
    private float timer;
    private static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazard());
    }

    private void Update()
    {
        if(gameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();
            timer = 0;
        }
        
    }

    private IEnumerator SpawnHazard()
    {
        var hazardToSpawn = Random.Range(1, maxHazardToSpawn);

        for(int i = 0; i < hazardToSpawn; i++)
        {
            var x = Random.Range(-6, 6);
            var drag = Random.Range(0f, 2f);

            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity);

            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        var timeToWait = Random.Range(0.5f, 1.5f);

        yield return new WaitForSeconds(timeToWait);
        yield return SpawnHazard();

    }

    public static void GameOver()
    {
        gameOver = true;
    }

}
