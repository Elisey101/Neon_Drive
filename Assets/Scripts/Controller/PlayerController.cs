using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private int _maxMoveSpeed;
    [SerializeField] private float _accelerationMoveSpeed;
    [SerializeField] private float _decelerationMoveSpeed;
    [SerializeField] private AudioClip _music, _crashSound, _bumpSound;
    [SerializeField] private ParticleSystem _dust, _crash;
    [SerializeField] private bool _immortality;

    // GetInput(), Awake(), OnEnable(), OnDisable()
    private PlayerInput _input;
    private float _turn;
    private float _move;

    // Borders()
    private float _xRange = 3.7f;

    // Wiggle(), Tilt(), GetAngle()
    private int _wiggleRange = 18;
    private int _tiltRange = 5;
    private Quaternion _targetWiggle;
    private Quaternion _targetTilt;

    // GetPrimaryPos()
    private int _yRange = 0;

    // GetAngle()
    private float _interpolationRatio = 20;

    // GetTurn()
    private AudioSource _playerAudio => GetComponent<AudioSource>();
    private GameManager _gameManager;
    private MoveLeft _moveLeft;
    private bool _gameOver;

    // GetInput()
    private float _currentTurnVector;
    private float _currentMoveVector;
    private float _inputVelocity;
    private float _inputMoveVelocity;
    private float _smoothTurnSpeed = .16f;
    private float _velocityRateCoefficient;

    public bool GameOver { get => _gameOver; set => _gameOver = value; }
    public float AccelerationMoveSpeed { get => _accelerationMoveSpeed; }
    public float CurrentMoveVector { get => _currentMoveVector; }
    public float Move { get => _move; }


    private void Awake() 
    {
        _gameManager = FindObjectOfType<GameManager>();
        _moveLeft = FindObjectOfType<MoveLeft>();
        _input = new PlayerInput();
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() =>_input.Disable();

    void FixedUpdate()
    {
        GetInput();
        GetTurn(_currentTurnVector);
        GetMove(_currentMoveVector);
        Wiggle(_currentTurnVector);
        Borders();
    }
    private void GetInput() 
    {
        if(GameOver == false) 
        {
            _turn = _input.Player.Turn.ReadValue<float>();
            _move = _input.Player.Move.ReadValue<float>();
        }

        if(_move > 0) _velocityRateCoefficient = _accelerationMoveSpeed;
        else if(_move <= 0) _velocityRateCoefficient = _decelerationMoveSpeed;
        
        _currentTurnVector = Mathf.SmoothDamp(_currentTurnVector, _turn, ref _inputVelocity, _smoothTurnSpeed);
        _currentMoveVector = Mathf.SmoothDamp(_currentMoveVector, _move, ref _inputMoveVelocity, _velocityRateCoefficient);
    }

    private void GetTurn(float turmDirection) => transform.Translate(Vector3.right * turmDirection * (_turnSpeed + _currentMoveVector) * Time.deltaTime);
    private void GetMove(float moveDirection) => _moveLeft.ObjectSpeed = moveDirection * _maxMoveSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(_immortality == false){
                GameOver = true;
                _gameManager.GameOver();
                _playerAudio.PlayOneShot(_crashSound);
                _turn = 0;
                _dust.Stop();
            }
            // _crash.Play();
        }
    }

    private void Borders()
    {
        if(transform.position.x < -_xRange) transform.position = new Vector3(-_xRange, transform.position.y, transform.position.z);
        else if(transform.position.x > _xRange) transform.position = new Vector3(_xRange, transform.position.y, transform.position.z);
    }
    
    private void Wiggle(float turnDirection)
    {
        _targetWiggle = Quaternion.Euler(0, 0, -turnDirection * _wiggleRange);
        GetAngle();
        GetPrimaryPos();
    }

    private Quaternion GetAngle() => transform.rotation = Quaternion.Slerp(transform.rotation, _targetWiggle, Time.deltaTime * _interpolationRatio);
    private Vector3 GetPrimaryPos() => transform.position = new Vector3(transform.position.x, _yRange, transform.position.z); 
}

// if(other.CompareTag("Enemy") && _moveLeft.ObjectSpeed + _moveLeft.DefaultSpeed > _moveLeft.DefaultSpeed)
// {
//     if(_immortality == false){
//         GameOver = true;
//         _gameManager.GameOver();
//         _playerAudio.PlayOneShot(_crashSound);
//         _dust.Stop();
//     }
//     // _crash.Play();
// }
// else if(other.CompareTag("Enemy") && _moveLeft.ObjectSpeed + _moveLeft.DefaultSpeed < _moveLeft.DefaultSpeed)
// {
//     _playerAudio.PlayOneShot(_bumpSound);
// }