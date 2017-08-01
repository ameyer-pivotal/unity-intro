using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public float moveDegPerSec = 90f;
    public float shootForce = 1500f;
    public int startingBallCount = 5;
    public Transform platform;
    public Transform cannon;
    public Transform spawnPoint;
    public GameObject ballPrefab;
    public Text ballCountText;

    private Camera cam;
    private AudioSource audio;

    private int _ballCount;
    private int ballCount
    {
        get { return _ballCount; }
        set
        {
            _ballCount = value;
            ballCountText.text = "Balls: " + ballCount;

            if (ballCount == 0)
            {
                GameController.instance.Invoke("GameOver", 2f);
            }
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
        ballCount = startingBallCount;
    }

    void Update()
    {
        if (!GameController.instance.gameOver)
        {
            CheckKeys();
            CheckMouse();
        }
    }

    private void CheckKeys()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
			transform.RotateAround(platform.position, Vector3.up, moveDegPerSec * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
			transform.RotateAround(platform.position, Vector3.up, -moveDegPerSec * Time.deltaTime);
        }
    }

    private void CheckMouse()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        cannon.rotation = Quaternion.LookRotation(mouseRay.direction);

        if (Input.GetMouseButtonDown(0) && ballCount > 0)
        {
            ShootBall();
        }
    }

    private void ShootBall()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        rigidbody.AddForce(mouseRay.direction * shootForce);
        ballCount -= 1;
        audio.Play();
    }
}
