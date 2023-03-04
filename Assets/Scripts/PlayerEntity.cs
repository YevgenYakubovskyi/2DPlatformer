using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    
    private Rigidbody2D _rigitbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
