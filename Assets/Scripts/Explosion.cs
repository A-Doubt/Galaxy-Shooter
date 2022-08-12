using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionSource;

    void Start()
    {
        _explosionSource = GetComponent<AudioSource>();
        if (_explosionSource == null)
        {
            Debug.LogError("Enemy, _explosionSource is null");
        }
        _explosionSource.Play();
        Destroy(this.gameObject, 3.0f);
    }

}
