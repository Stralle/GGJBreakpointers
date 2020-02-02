using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _characterPrefab = null;
    [SerializeField]
    Transform _characterSpawnLocation = null;

    public void SpawnCharacter()
    {
        GameObject _character = Instantiate(_characterPrefab, _characterSpawnLocation.position, _characterPrefab.transform.rotation);
    }
}
