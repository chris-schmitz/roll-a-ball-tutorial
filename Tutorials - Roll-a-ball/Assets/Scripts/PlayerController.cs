using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 8.0f;
    public Text countText;
    public Text winText;
    public GameObject pickupCollection;
    private Rigidbody rigidBody;
    private int count;
    private int totalPickups;

    private SerialPort sp = new SerialPort("/dev/tty.usbmodem14201", 9600);

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        totalPickups = pickupCollection.transform.childCount;

        // foreach (string str in SerialPort.GetPortNames())
        // {
        //     Debug.Log(string.Format("Port: {0}", str));
        // }

        sp.Open();
        sp.ReadTimeout = 100;
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                int message = sp.ReadByte();
                Debug.Log(message);
            }
            catch (System.Exception e)
            {
                // Debug.Log("Exception");
                Debug.Log(e);
            }

        }
        else
        {
            Debug.Log("Serial port is closed");
        }
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        rigidBody.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.gameObject.CompareTag("pick-up"))
        {
            collidingObject.gameObject.SetActive(false);
            Debug.Log("Collided with a pick-up");
            count++;
            SetCountText();
            if (count >= totalPickups)
            {
                winText.text = "Oh hey! You won!";
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
