using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 7f)
        {
            if (this.gameObject.transform.parent != null)
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
