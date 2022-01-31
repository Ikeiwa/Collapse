using System;
using UnityEngine;
using System.Collections.Generic;


public class SpectrumAnalyzer : MonoBehaviour
{
    public AnalyzerSettings settings;

    private AudioSource audioSource;
    private float[] spectrum; //Audio Source data
    private Texture2D spectrumTex;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        InitBuffers();
    }

    private void InitBuffers()
    {
        spectrumTex = new Texture2D((int)settings.pillar.amount, 1, TextureFormat.RFloat, false);
        spectrumTex.filterMode = FilterMode.Point;
        Shader.SetGlobalTexture("_MusicSpectrumTex", spectrumTex);
        spectrum = new float[(int)settings.spectrum.sampleRate];
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, settings.spectrum.FffWindowType);


        for (int i = 0; i < settings.pillar.amount; i++)
        {
            float level = spectrum[i]*settings.pillar.sensitivity*Time.deltaTime*1000; //0,1 = l,r for two channels

            float previousScale = spectrumTex.GetPixel(i, 0).r;
            previousScale = Mathf.Lerp(previousScale, level, settings.pillar.speed*Time.deltaTime);
            spectrumTex.SetPixel(i, 0, new Color(previousScale, previousScale, previousScale));
        }

        spectrumTex.Apply();
    }
    
    private void Reset()
    {
        settings.pillar.Reset();
        settings.spectrum.Reset();
    }

    #region Dynamic floats and for UI sliders

    public int Amount
    {
        get { return settings.pillar.amount; }
        set
        {
            settings.pillar.amount = Mathf.Clamp(value, 4, 128);
            
        }
    }


    public float Sensitivity
    {
        get { return settings.pillar.sensitivity; }
        set { settings.pillar.sensitivity = Mathf.Clamp(value, 1, 50); }
    }

    public float PillarSpeed
    {
        get { return settings.pillar.speed; }
        set { settings.pillar.speed = Mathf.Clamp(value, 1, 30); }
    }


    public float SampleMethod
    {
        get { return (int) settings.spectrum.FffWindowType; }
        set
        {
            //set with UI Slider
            int num = (int)Mathf.Clamp(value, 0, 6); 
            settings.spectrum.FffWindowType = (FFTWindow) num;
        }
    }

    public float SampleRate
    {
        get { return (int) settings.spectrum.sampleRate; }
        set
        {
            //set with UI Slider
            int num = (int) Mathf.Pow(2, 7 + value);//128,256,512,1024,2056
            settings.spectrum.sampleRate = (SampleRates) num;
            InitBuffers();
        }
    }

    #endregion
}