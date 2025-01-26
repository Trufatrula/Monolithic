using UnityEngine;
using System.Collections;

public class TeclaFlecha : TeclaItem
{
    public Quaternion GetOriginalRotation()
    {
        return collectRotation;
    }

    public string GetDirection()
    {
        Vector3 direction = collectRotation * Vector3.up;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? "Down" : "Up";
        }
        else
        {
            return direction.y > 0 ? "Right" : "Left";
        }
    }
}
