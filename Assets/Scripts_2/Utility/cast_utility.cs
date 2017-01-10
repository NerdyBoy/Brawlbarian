using UnityEngine;
using System.Collections;

public class cast_utility : MonoBehaviour {

    public static T Cast_For_Component<T>(Vector3 _origin, Vector3 _direction, float _distance)
    {
        GameObject game_object = Cast_For_Object(_origin, _direction, _distance);
        if (null != game_object)
        {
            if (null != game_object.GetComponent<T>())
            {
                return game_object.GetComponent<T>();
            }
            else
            {
                return default(T); //depending on type of T returns either NULL or 0
            }
        }
        else
        {
            return default(T);
        }
    }

    public static GameObject Cast_For_Object(Vector3 _origin, Vector3 _direction, float _distance)
    {
        RaycastHit hit_result;
        Ray ray = new Ray(_origin, _direction);
        Physics.Raycast(ray, out hit_result, _distance);
        if (null != hit_result.collider)
        {
            return hit_result.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public static T Spherecast_For_Component<T>(Vector3 _origin, Vector3 _direction, float _distance, float _radius)
    {
        GameObject game_object = Spherecast_For_Object(_origin, _direction, _distance, _radius);
        if (null != game_object)
        {
            if (null != game_object.GetComponent<T>())
            {
                return game_object.GetComponent<T>();
            }
            else
            {
                return default(T); //depending on type of T returns either NULL or 0
            }
        }
        else
        {
            return default(T);
        }
    }

    public static GameObject Spherecast_For_Object(Vector3 _origin, Vector3 _direction, float _distance, float _radius)
    {
        RaycastHit hit_result;
        Ray ray = new Ray(_origin, _direction);
        Physics.SphereCast(ray, _radius, out hit_result, _distance);
        if (null != hit_result.collider)
        {
            return hit_result.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
