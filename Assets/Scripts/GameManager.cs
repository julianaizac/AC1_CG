using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefab;
    public int maxHazardToSpawn = 3;

    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;

    public GameObject player;

    public GameObject mainVcam;
    public GameObject zoomVcam;

    public GameObject gameOverMenu;
    private bool gameOver;

    public int score;
    private float timer;

    private Coroutine hazardsCoroutine;

    private static GameManager instance;
    public static GameManager Instance => instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void OnEnable()
    {
        player.SetActive(true);

        zoomVcam.SetActive(false);
        mainVcam.SetActive(true);

        gameOver = false;
        scoreText.text = "0";
        score = 0;
        timer = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazard());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1, 0.5f));
                backgroundMenu.gameObject.SetActive(false);
            }

            if (Time.timeScale == 1)
            {
                StartCoroutine(ScaleTime(1, 0, 0.5f));
                backgroundMenu.gameObject.SetActive(true);
            }
        }

        if(gameOver) return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();
            timer = 0;
        }
        
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while(timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * end;
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

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);

        gameOver = true;

        mainVcam.SetActive(false);
        zoomVcam.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

}
