using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stepSounds;
    [SerializeField] private AudioClip[] _attackSounds;
    [SerializeField] private AudioClip[] _jumpSounds;
    [SerializeField] private int _concurrentSounds = 3;
    [SerializeField] private float _stepCD = 0.5f, _jumpCD = 1, _attackCD = 0.5f;

    private Rigidbody _rigidBody;
    private Vector3 _ignoreY = new(1, 0, 1);
    private AudioSource[] _sources;
    private float _stepTimer = 0, _attackTimer = 0, _jumpTimer = 0;
    private int _sourceIndex = 0;

    #region SETUP

    void OnEnable()
    {
        InputReader.jumpEvent += Jump; 
        InputReader.attackSlotEvent += Attack;
    }

    void OnDisable()
    {
        InputReader.jumpEvent -= Jump; 
        InputReader.attackSlotEvent -= Attack;
    }

    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();

        _sources = new AudioSource[_concurrentSounds];
        for (int i = 0; i < _concurrentSounds; i++)
        {
            _sources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    #endregion

    void Update()
    {
        UpdateTimers();

        if (_stepTimer > 0) return;

        var speed = Vector3.Scale(_rigidBody.velocity, _ignoreY).magnitude;
        if (speed > 1)
        {
            PlayRandomSound(_stepSounds);
            _stepTimer = Mathf.Clamp01(_stepCD / speed);
        }
    }

    void Jump()
    {
        if (_jumpTimer > 0) return;

        PlayRandomSound(_jumpSounds);
        _jumpTimer = _jumpCD;
    }

    void Attack()
    {
        if (_attackTimer > 0) return;

        PlayRandomSound(_attackSounds);
        _attackTimer = _attackCD;
    }

    void PlayRandomSound(AudioClip[] clips)
    {
        _sources[_sourceIndex].clip = clips[Random.Range(0, clips.Length)];
        _sources[_sourceIndex].pitch = Random.Range(0.95f, 1.3f);
        _sources[_sourceIndex].volume = Random.Range(0.08f, 0.12f);
        _sources[_sourceIndex].Play();
        _sourceIndex = (_sourceIndex + 1) % _sources.Length;
    }

    void UpdateTimers()
    {
        var duration = Time.deltaTime;
        if (_stepTimer > 0) _stepTimer -= duration;
        if (_jumpTimer > 0) _jumpTimer -= duration;
        if (_attackTimer > 0) _attackTimer -= duration;
    }
}
