using System;
using UnityEngine;
    
public enum Direction
{
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT
}

public class DirectionInfo : MonoBehaviour
{
    [HideInInspector] public Direction direction;
    [HideInInspector] public Vector2 lookDirection;
    [HideInInspector] public Vector2 moveDirection;
    
    public Vector2 GetDirectionVector(Vector2 v) {
        v.Normalize();
        var x = (Mathf.Abs(v.x) > 0.5f) ? Mathf.Sign(v.x) : 0;
        var y = (Mathf.Abs(v.y) > 0.5f) ? Mathf.Sign(v.y) : 0;
        return new Vector2(x, y);
    }

    private Direction GetDirectionFromVector(Vector2 v)
    {
        v = GetDirectionVector(v);
        switch (v.x)
        {
            case 0:
                if (v.y == 1)
                    return Direction.UP;
                if (v.y == -1)
                    return Direction.DOWN;
                if (v.y == 0)
                    return direction;
                break;
            case 1:
                if (v.y == 1)
                    return Direction.UP_RIGHT;
                if (v.y == -1)
                    return Direction.DOWN_RIGHT;
                return Direction.RIGHT;
                break;
            case -1:
                if (v.y == 1)
                    return Direction.UP_LEFT;
                if (v.y == -1)
                    return Direction.DOWN_LEFT;
                return Direction.LEFT;
                break;
        }
        Debug.LogWarning($"{v} could not be converted to a direction");
        return Direction.DOWN;
    }

    private void Update()
    {
        direction = moveDirection.magnitude > .3f ? GetMovementDirection() : GetLookDirection();
    }

    private Direction GetLookDirection()
    {
        Vector2 v = GetDirectionVector(lookDirection.normalized);
        return GetDirectionFromVector(v);
    }

    private Direction GetMovementDirection()
    {
        Vector2 v = GetDirectionVector(moveDirection.normalized);
        return GetDirectionFromVector(v);
    }
}
