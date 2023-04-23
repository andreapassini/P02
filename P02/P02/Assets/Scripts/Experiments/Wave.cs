using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Wave : MonoBehaviour
{
    [SerializeField] private Transform parent;
    public Vector3 parentPos;
    public Vector3 parentPreviousPosition = new Vector3();

    private Vector3 _moveToPos;

    [Space]
    [SerializeField] private float timeDelay = .25f;

    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float speed = .25f;
    [SerializeField] private float stoppingDistance = .5f;
    private bool _move = false;
    void Start()
    {
        if(parent == null)
            return;

        parentPreviousPosition = new Vector3(parent.transform.position.x,
            parent.transform.position.y,
            parent.transform.position.z);
    }

    void Update()
    {
        if(parent == null)
            return;

        parentPos = parent.position;
        
        // When the parent moves, start a coroutine that change child vertical position to the one of the parent
        if (parent.transform.position == parentPreviousPosition)
        {
            return;
        }

        _moveToPos = new Vector3(parent.transform.position.x,
            parent.transform.position.y,
            parent.transform.position.z);
        
        StartCoroutine(DelayMovement());
    }

    private void FixedUpdate()
    {
        if(!_move)
            return;
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position,
            _moveToPos, step);
    }

    private IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(timeDelay);

        _move = true;

        while ((_moveToPos - transform.position).sqrMagnitude > (stoppingDistance*stoppingDistance))
        {
            yield return new WaitForFixedUpdate();
        }

        _move = false;
        
        parentPreviousPosition = new Vector3(parent.transform.position.x,
            parent.transform.position.y,
            parent.transform.position.z);
    }
}
