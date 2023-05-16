using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerMovement : MonoBehaviour
{
    public float velocity = 0;
    private Rigidbody rb;
    private float moveX;
    private float moveZ;

    private int count;
    public TextMeshProUGUI countText;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveZ = movementVector.y;
        Debug.Log(movementVector);
    }

    void FixedUpdate()
    {
        Vector3 force = new Vector3(moveX, 0.0f, moveZ);
        rb.AddForce(force*velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("PenaltyWall"))
        {
            UImanager ui = canvas.GetComponent<UImanager>();
            ui.deductTimer();
            count--;
        }
    }
    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
	}
}
