using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private GameObject _roadLight;
    private int _roadNum = 16;
    private int _roadOffset = 10;
    private int _lightNum = 8;
    private int _lightOffset = 20;
    void Start()
    {
        Spawn(_roadPrefab, _roadNum, _roadOffset); //Road
        Spawn(_roadLight, _lightNum, _lightOffset); //Lamps
    }

    void Spawn(GameObject objPrefab, int objNumValue, int objOffsetValue)
    {
        for(int objNum = 0, objOffset = 0; objNum < objNumValue; objNum++, objOffset += objOffsetValue){
            Vector3 objSpawnPos = new Vector3(0, 0, objOffset);
            Instantiate(objPrefab, objSpawnPos, objPrefab.transform.rotation, transform);
        }
    }
}
