﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateStringMouse : MonoBehaviour {

    public float amplitude;
    public float frequency;
	public float segmentLen; 

    private float startTime;
    private bool animated;
    public void startAnimation()
    {
        AudioSource audio = GetComponent<AudioSource>();
        AudioSource a = Camera.main.gameObject.AddComponent<AudioSource>();
        a.playOnAwake = false;
        a.clip = audio.clip;

        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.Play();
        }

        startTime = Time.time;
        animated = true;
        
        this.GetComponentInParent<SpawnForce>().triggered = true;
        this.GetComponentInParent<SpawnForce>().amplitude = amplitude;
		this.GetComponentInParent<SpawnForce>().waveLen = 2.0f * segmentLen; // because segment length is half size of wave
    }

    public void stopAnimation()
    {
        animated = false;
    }

	// Use this for initialization
	void Start () {
        int numPoints = transform.childCount;
        for (int i = 0; i < numPoints; i++)
        {
            GameObject c = transform.GetChild(i).gameObject;
			c.transform.localPosition = new Vector3(0, i * segmentLen, 0);
        }

        
    }
	
	// Update is called once per frame
	void Update () {
		if (animated)
        {
            float t = Time.time - startTime;
            int numPoints = transform.childCount;
			float x = amplitude * Mathf.Sin(frequency * 1.0f) * Mathf.Sin(Mathf.PI * t);
            float x_d = x * Mathf.Exp(-t / 2.0f);
            GameObject c = transform.GetChild(1).gameObject;
			c.transform.localPosition = new Vector3(x_d, 1 * segmentLen, 0);
        }
	}
}
