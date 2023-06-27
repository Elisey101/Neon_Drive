using UnityEngine;
public class MoveLeft : MonoBehaviour
{
    private PlayerController _playerController;
    private Vector3 _startPos;
    private int _defaultSpeed = 25;
    private float _halfCollider;
    private float _objectSpeed;
    private float _commonSpeed;
    public float ObjectSpeed { get => _objectSpeed; set => _objectSpeed = value; }
    public int DefaultSpeed { get => _defaultSpeed; }
    public float CommonSpeed { get => _commonSpeed; }
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        _startPos = transform.position;
        _halfCollider = GetComponent<BoxCollider>().size.z / 2;
    }

    void Update()
    {
        Move(); 
        Repeat();
    }

    void Move()
    {
        if(!_playerController.GameOver) 
        {
            if(_objectSpeed + _defaultSpeed < 0) _commonSpeed = 0;
            else _commonSpeed = _objectSpeed + _defaultSpeed;
            transform.Translate(Vector3.back * Time.deltaTime * _commonSpeed);
            Debug.Log(_objectSpeed + _defaultSpeed);
        }
    }
    void Repeat()
    {
        if(transform.position.z < _startPos.z - _halfCollider)transform.position = _startPos;
    }
}
