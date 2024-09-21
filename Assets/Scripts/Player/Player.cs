using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void ForwardMove()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime);
    }
}
