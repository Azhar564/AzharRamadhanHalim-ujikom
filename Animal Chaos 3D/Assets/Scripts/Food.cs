using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Food : MonoBehaviour
{
    public float speed = 300.0f;
    public int hungerValue = 25;
    public float lifetime = 3.0f;
    public AudioClip eatClip;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody>();

        GameMananger.Instance.onPause += OnPause;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Vector3.forward * speed * 0.01f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Animal animal = other.GetComponent<Animal>();
            animal.hungerNeed -= hungerValue;
            AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
            GameMananger.Instance.onPause -= OnPause;
            Destroy(gameObject);
        }
    }

    private void OnPause(bool pauseActive)
    {
        enabled = !pauseActive;
    }
}
