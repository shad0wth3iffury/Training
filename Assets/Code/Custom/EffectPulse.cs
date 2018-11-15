using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPulse : MonoBehaviour {

    public List<ParticleSystem> ParticleList = new List<ParticleSystem>();
    public Color ParticleColor = Color.white;
    public float AlphaMaximum = 1;
    public float AlphaMinimum = 0;

	// Update is called once per frame
	void Update () {

        ParticleColor.a = SineFunction(ParticleColor.a, Time.time);

        for(int i = 0; i < ParticleList.Count; i++)
        {
            ParticleList[i].startColor = ParticleColor;
        }

	}

    static float SineFunction(float x, float t)
    {
        float pi = Mathf.PI;
        float y;
        y = Mathf.Sin(pi * (x + t * 0.4f)) - 0.1f;
        return y;
    }
}
