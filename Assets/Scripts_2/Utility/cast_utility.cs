using UnityEngine;
using System.Collections;

public class cast_utility : MonoBehaviour {

    public static T Cast_For_Component<T>(Vector3 _origin, Vector3 _direction, float _distance)
    {
        RaycastHit[] hit_objects = Cast_For_Object(_origin, _direction, _distance);
        for (int i = 0; i < hit_objects.Length; i++)
        {
            if (null != hit_objects[i].collider)
            {
                if (null != hit_objects[i].collider.gameObject.GetComponent<T>())
                {
                    return hit_objects[i].collider.gameObject.GetComponent<T>();
                }
            }
        }

        return default(T);
        
    }

    public static RaycastHit[] Cast_For_Object(Vector3 _origin, Vector3 _direction, float _distance)
    {
        RaycastHit[] hit_result;
        Ray ray = new Ray(_origin, _direction);
        hit_result = Physics.RaycastAll(ray, _distance);
        return hit_result;
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
