using UnityEngine;
using System.Collections;


public enum SampleRates
{
    Hz128 = 128,
    Hz256 = 256,
    Hz512 = 512,
    Hz1024 = 1024,
    Hz2048 = 2048
}


[System.Serializable]
public struct AnalyzerSettings
{   //structs
    public PillarSettings pillar;
    public SpectrumSettings spectrum;
}



[System.Serializable]
public struct SpectrumSettings
{
    public FFTWindow FffWindowType;
    public SampleRates sampleRate;

    public void Reset()
    {
        FffWindowType = FFTWindow.BlackmanHarris;
        sampleRate = SampleRates.Hz2048;
    }
}



[System.Serializable]
public struct PillarSettings
{
    public int amount;
    public float sensitivity;
    public float speed;
    public void Reset()
    {
        sensitivity = 40;
        amount = 64;
        speed = 5;
    }
}