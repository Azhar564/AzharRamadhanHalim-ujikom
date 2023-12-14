using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveInput;
    public InputActionReference throwInput;

    [Header("Player Data")]
    public GameObject foodPrefab;
    public float speed = 350.0f;
    public AudioClip throwClip;
    private float x;

    private Rigidbody rb;
    private Animator animator;

    private void OnEnable()
    {
        moveInput.action.performed += Move;
        moveInput.action.canceled += Move;

        throwInput.action.started += Throw;

    }

    private void OnDisable()
    {
        moveInput.action.performed -= Move;
        moveInput.action.canceled -= Move;

        throwInput.action.started -= Throw;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        GameMananger.Instance.onPause += OnPause;
    }


    private void OnPause(bool pauseActive)
    {
        enabled = !pauseActive;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (x == 0.0f)
        {
            rb.velocity = Vector3.zero;
            PlayState("Idle");
            return;
        }
        rb.AddForce(Vector3.right * x * speed * Time.deltaTime);
        if (x < 0)
        {
            PlayState("Strafe Left");
        }
        else
        {
            PlayState("Strafe Right");
        }
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        x = ctx.ReadValue<Vector2>().x;
    }
    private void Throw(InputAction.CallbackContext ctx)
    {
        Instantiate(foodPrefab, transform.position + transform.forward, Quaternion.identity);
        PlayState("Throw");
        AudioSource.PlayClipAtPoint(throwClip, Vector3.zero);
    }

    private void PlayState(string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            return;
        }
        animator.Play(stateName);
    }
}
