using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public int scoreValue = 100;
    public Color scoredColor;

    private bool scored;
    private Renderer renderer;
    private AudioSource audio;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "Floor" && !scored)
        {
            scored = true;
            renderer.material.color = scoredColor;
            GameController.instance.score += scoreValue;
            audio.Play();
        }
    }
}
