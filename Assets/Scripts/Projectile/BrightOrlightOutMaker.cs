using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private Light _light;
    private float _initialIntensity, _targetIntensity, _changeDuration, _intensityChangeRate;

    // Start is called before the first frame update
    void Start() {

        _light = GetComponent<Light>();

        _targetIntensity = 0.0f;
        _initialIntensity = 10000f;
        _changeDuration = .3f;
        _light.intensity = _targetIntensity;
        _intensityChangeRate = (_initialIntensity-_targetIntensity) / _changeDuration;

        _audioSource.pitch = Random.Range(0.75f, 1f);

        StartCoroutine(DecreaseIntensityOverTime());

    }

    private IEnumerator DecreaseIntensityOverTime() {

        yield return new WaitForSeconds((_changeDuration/3)-0.08f);
        _light.intensity = 10000f;

        float timer = 0.0f;

        while (timer < _changeDuration) {
            _light.intensity -= _intensityChangeRate * Time.deltaTime;
            // Debug.Log(_light.intensity);
            timer += Time.deltaTime;
            yield return null;
        }


    }


}
