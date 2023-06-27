using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _cars;
    [SerializeField] private int _trafficDensity;
    private PlayerController _playerController;
    private MoveLeft _moveLeft;
    private int _spawnDist = 70;
    private int _distRange = 10;
    private int _zSpawnRange = 13;
    private const int _RoadLineNum = 4;
    private int _index;
    private int _childCount;
    private float _roadLine;
    private float _roadPrev;
    private float _zPoz;



    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        Invoke("RandomSpawn", 2);
    }

    void Update() 
    {
        _childCount = transform.childCount;
        RandomSpawn();
    }

    private void RandomSpawn()
    {
        if(_trafficDensity - _childCount > _childCount)
        {
            _index = Random.Range(0, _cars.Length);
            switch(GetRandomX())
            {
                case 0: _roadLine = 1.2f; break;
                case 1: _roadLine = 3.6f; break;
                case 2: _roadLine = -1.2f; break;
                case 3: _roadLine = -3.6f; break;
            }

            _zPoz = (GetRandomZ() * _distRange) + _spawnDist;

            if (!_playerController.GameOver && !(_roadLine == _roadPrev)) 
            {
                Instantiate(_cars[_index], SetSpawnPos(_roadLine, _zPoz), transform.rotation, transform);
            }
            _roadPrev = _roadLine;
        }
    }
    private int GetRandomX() => Random.Range(0, _RoadLineNum);
    private int GetRandomZ() => Random.Range(0, _zSpawnRange);
    private Vector3 SetSpawnPos(float value1, float value2) => new Vector3(value1, 0, value2);
}