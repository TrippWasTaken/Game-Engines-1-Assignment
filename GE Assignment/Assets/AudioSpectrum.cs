using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    AudioSource _audiosource;
    float[] _samples = new float[512];
    float[] _freqBand = new float[8];
    float[] _bandBuffer = new float[8];
    float[] _bufferDec = new float[8];
    private int _frequency;
    public static float _frequencyData;
    [SerializeField] private float beatThreshold = .5f;
    public bool beatDetected { get; set; }

    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];


    void Start()
    {
        _audiosource = GetComponent<AudioSource>();

    }

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFreqBands();
        BandBuffer();
        CreateAudioBands();
        DetectBeat();
    }

    void DetectBeat()
    {
        _frequencyData = _samples[_frequency] + _samples[_frequency + 1] + _samples[_frequency + 2] / 3;
        if (_frequencyData > beatThreshold)
        {
            beatDetected = true;
        }
        else
        {
            beatDetected = false;
        }
    }
    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = 0.0001f;
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }

            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audiosource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFreqBands()
    {
        float average = 0;
        int c = 0;
        for (int i = 0; i < 8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            for (int x = 0; x < sampleCount; x++)
            {
                average += _samples[c] * (c + 1);
                c++;
            }
            average /= c;
            _freqBand[i] = average * 10;

        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBand[i];
                _bufferDec[i] = 0.005f;
            }
            if (_freqBand[i] < _bandBuffer[i])
            {
                _bandBuffer[i] -= _bufferDec[i];
                _bufferDec[i] *= 1.2f;
            }
        }
    }
}
