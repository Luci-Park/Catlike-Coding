using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    public enum DisplayMode { FPS, MS }
    [SerializeField]
    DisplayMode displayMode = DisplayMode.FPS;

    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1f;

    int frames;
    float duration, bestduration = float.MaxValue, worstDuration = float.MinValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;
        if(frameDuration < bestduration)
        {
            bestduration = frameDuration;
        }
        if(frameDuration > worstDuration)
        {
            worstDuration = frameDuration;
        }
        if (duration >= sampleDuration)
        {
            if (displayMode == DisplayMode.FPS)
            {
                //0:0 = round the digits => 소숫점 1번 자리에서 반올림 => 정수화.
                display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", frames / duration, 1f / bestduration, 1f / worstDuration);
            }
            else
            {
                display.SetText("MS\n{0:0}\n{1:0}\n{2:0}", duration * 1000f, bestduration * 1000f, worstDuration * 1000f);
            }
            frames = 0;
            duration = 0f;
        }
    }
}
