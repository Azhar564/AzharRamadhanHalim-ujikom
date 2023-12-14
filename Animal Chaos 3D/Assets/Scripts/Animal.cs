using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public float speed = 250;
    public int hungerNeed = 200;
    public int score = 1;
    public Slider hpBar;
    public ParticleSystem deathParticlePrefab;

    private Rigidbody rb;
    private int tempHungerNeed;

    private void Start()
    {
        tempHungerNeed = hungerNeed;
        rb = GetComponent<Rigidbody>();

        GameMananger.Instance.onPause += OnPause;
    }

    private void Update()
    {
        hpBar.value = ((float)tempHungerNeed - (float)hungerNeed) / (float)tempHungerNeed;
        if (hungerNeed < 0)
        {
            //Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            GameMananger.Instance.AddScore(score);
            GameMananger.Instance.onPause -= OnPause;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * 0.01f * Time.deltaTime);
    }

    private void OnPause(bool pauseActive)
    {
        enabled = !pauseActive;
    }
}
