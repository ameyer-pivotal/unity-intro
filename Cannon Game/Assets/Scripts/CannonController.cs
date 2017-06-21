using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float minShootForce = 1000f;
    public float shootForceRange = 1000f;
    public int startingBallCount = 5;
    public Transform cannon;
    public Transform spawnPoint;
    public GameObject ballPrefab;
    public Text powerText;
    public Text ballCountText;
    public GameObject gameOverPanel;

    private Camera cam;
    private AudioSource audio;

    private float _power;
    private float power
    {
        get { return _power; }
        set
        {
            _power = value;
            powerText.text = "Power: " + (int)(power * 100f) + "%";
        }
    }

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
                Invoke("ShowGameOverPanel", 2f);
            }
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
        power = 0.5f;
        ballCount = startingBallCount;
    }

    void Update()
    {
        if (!gameOverPanel.activeSelf)
        {
            CheckKeys();
            CheckMouse();
        }
    }

    private void CheckKeys()
    {
        if (Input.GetKey(KeyCode.A) && power < 1)
        {
            power += 0.25f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Z) && power > 0)
        {
            power -= 0.25f * Time.deltaTime;
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
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        rb.AddForce(mouseRay.direction * (minShootForce + shootForceRange * power));
        ballCount -= 1;
        audio.Play();
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
