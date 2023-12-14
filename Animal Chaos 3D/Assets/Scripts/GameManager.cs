using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameMananger : MonoBehaviour
{
    public static GameMananger Instance;

    public GameObject mainPanel, pausePanel, gameOverPanel;
    public InputActionReference pauseInput;

    [Header("Score & Timer")]
    public int score;
    public float timer;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text finalScoreText;

    [Header("Spawner")]
    public Transform spawnPlace;
    public float rangeHorizontal = 3.0f;
    public int spawnCount = 1;
    public float spawnInterval = 2.0f;
    public List<Animal> animals = new List<Animal>();

    private float tempSpawnInterval = 0.0f;
    private bool gameOver, pause;

    public System.Action<bool> onPause;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        pauseInput.action.started += ctx => { pause = !pause; ActivePausePanel(pause); onPause?.Invoke(pause); };
    }
    private void OnDisable()
    {
        pauseInput.action.started -= ctx => { pause = !pause; ActivePausePanel(pause); onPause?.Invoke(pause); };
    }

    // Start is called before the first frame update
    void Start()
    {
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver || pause)
        {
            return;
        }

        timer -= Time.deltaTime;
        timerText.text = "Timer: " + ((int)timer);

        if (tempSpawnInterval < spawnInterval)
        {
            tempSpawnInterval += Time.deltaTime;
        }
        else
        {
            SpawnAnimal();
            tempSpawnInterval = 0.0f;
        }

        if (timer < 0)
        {
            ShowGameOverPanel();
        }
    }

    private void SpawnAnimal()
    {
        Animal animal = animals[Random.Range(0, animals.Count)];
        Instantiate(animal,
            spawnPlace.position + new Vector3(Random.Range(-rangeHorizontal, rangeHorizontal), 0, 0),
            animal.transform.rotation,
            spawnPlace);
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + this.score;
        finalScoreText.text = scoreText.text;
    }

    public void ActiveMainPanel(bool active)
    {
        mainPanel.SetActive(active);
        pausePanel.SetActive(!active);

        pause = false;
        onPause.Invoke(pause);
    }

    public void ActivePausePanel(bool active)
    {
        pausePanel.SetActive(active);
        mainPanel.SetActive(!active);
    }

    public void ShowGameOverPanel()
    {
        pausePanel.SetActive(false);
        mainPanel.SetActive(false);
        gameOver = true;
        pause = false;

        gameOverPanel.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (!spawnPlace)
        {
            return;
        }
        Vector3 dir = new Vector3(rangeHorizontal, 0, 0);
        Gizmos.DrawLine(spawnPlace.position - dir, spawnPlace.position + dir);
    }
}
