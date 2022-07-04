using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(DirectionInfo))]
public class PlayerVisuals : MonoBehaviour
{
    private DirectionInfo _dir;
    private SpriteRenderer _sr;
    private Animator _anim;

    public float Scale
    {
        get { return transform.localScale.y; }
        set { transform.localScale = new Vector3(value, value, value); }
    }

    private void SetFacingLeft(bool value)
    {
        
        if (value)
            transform.localScale = new Vector3(-Scale, Scale, Scale);
        else
            transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    public void Start()
    {
        Scale = 1;
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _dir = GetComponent<DirectionInfo>();
        
        
    }

    private void Update()
    {
        
        if (_dir.direction <= Direction.DOWN_RIGHT && _sr.flipX == true)
            _sr.flipX = false;
        else if (_dir.direction > Direction.DOWN_RIGHT && _sr.flipX == false)
            _sr.flipX = true;
        
        SetAnimation();
    }

    private void SetAnimation()
    {
        Direction dir = _dir.direction;
        // Debug.Log("dir: " + dir);
        string stateName = "viking";
        if (_dir.moveDirection.magnitude > .1f)
            stateName += "Walk";
        else
            stateName += "Idle";

        switch (dir)
        {
            case Direction.UP:
                stateName += "Up";
                break;
            case Direction.RIGHT:
                stateName += "Side";
                break;
            case Direction.LEFT:
                stateName += "Side";
                break;
            case Direction.DOWN:
                stateName += "Down";
                break;
            case Direction.UP_RIGHT:
                stateName += "Up";
                break;
            case Direction.UP_LEFT:
                stateName += "Up";
                break;
            case Direction.DOWN_RIGHT:
                stateName += "Down";
                break;
            case Direction.DOWN_LEFT:
                stateName += "Down";
                break;
        }

        if (_dir.moveDirection.magnitude > .1f &&
            (dir == Direction.UP_RIGHT || dir == Direction.UP_LEFT || dir == Direction.DOWN_RIGHT ||
             dir == Direction.DOWN_LEFT))
            stateName += "Side";

        _anim.Play(stateName);

    }

}
