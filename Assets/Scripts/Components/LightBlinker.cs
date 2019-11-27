using UnityEngine;

public class LightBlinker : MonoBehaviour
{
    [SerializeField]
    private Light _light = null;
    [SerializeField]
    private AnimationCurve _blinkSequenceCurve = null;

    private float _fullLightIntensity;
    private float _sequenceLength;
    private float _currentSequenceTime;

    private void Start()
    {
        _fullLightIntensity = _light.intensity;
        _sequenceLength = _blinkSequenceCurve[_blinkSequenceCurve.length - 1].time;
        _currentSequenceTime = Random.Range(0.0f, _sequenceLength);
    }

    private void Update()
    {
        float currentIntensity = _blinkSequenceCurve.Evaluate(_currentSequenceTime) * _fullLightIntensity;
        _light.intensity = currentIntensity;
        
        _currentSequenceTime += Time.deltaTime;
        if (_currentSequenceTime > _sequenceLength)
        {
            _currentSequenceTime %= _sequenceLength;
        }
    }

    public void SetLightColor(Color color)
    {
        _light.color = color;
    }

    public Color GetLightColor()
    {
        return _light.color;
    }
}
