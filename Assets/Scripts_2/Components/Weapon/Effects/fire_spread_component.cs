using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class fire_spread_component : MonoBehaviour {
    //comment to commit
    public float life_time;
    public GameObject origin;
    public float distance_from_start_origin;
    Vector3[] spread_directions;
    Vector3 spread_direction;

    public GameObject fire_prefab;
    BoxCollider fire_collider;
    public float fire_spread_time;
    float spawn_distance;
    int spawn_times = 0;
    int cycles = 0;

    bool hit_flamable = false;

    Vector3 instantiation_point;
    GameObject parent_object;

    Vector3 start_position;
    
    List<health_component> overlapping_health;

    // Use this for initialization
    void Start() {
        overlapping_health = new List<health_component>();
        spread_directions = new Vector3[] { this.transform.forward, -this.transform.forward, this.transform.right, -this.transform.right };
        fire_collider = GetComponent<BoxCollider>();
        spawn_distance = fire_collider.bounds.extents.magnitude;
        instantiation_point = this.transform.position;
        Get_Overlapping_Objects_Health();
        StartCoroutine(Wait_Spread());
    }

    private void Update()
    {
        for(int i = 0; i < overlapping_health.Count; i++)
        {
            if (null != overlapping_health[i])
            {
                overlapping_health[i].On_Modify_Health(-10 * Time.deltaTime);
                if (null != character_score_component.character_score)
                {
                    character_score_component.character_score.Modify_Score(10);
                }
            }
        }
    }

    void Get_Overlapping_Objects_Health()
    {
        RaycastHit[] overlapping_objects = Physics.BoxCastAll(this.transform.position, fire_collider.bounds.extents / 2, this.transform.forward);
        for(int i = 0; i < overlapping_objects.Length; i++)
        {
            health_component health = overlapping_objects[i].collider.GetComponent<health_component>();
            if(null != health)
            {
                overlapping_health.Add(health);
            }
        }
        
    }

    IEnumerator Wait_Spread()
    {
        while (spawn_times < 2 && cycles < 3)
        {
            yield return new WaitForSeconds(fire_spread_time);
            spread_direction = spread_directions[Random.Range(0, spread_directions.Length)];
            if(false == In_Range())
            {
                break;
            }
            if (true == Check_Spread_Direction())
            {
                Spread_Fire(spread_direction);
                spawn_times++;
            }
            else
            {
                StartCoroutine(Wait_Spread());
            }
            cycles++;
        }
        StartCoroutine(Die());
    }

    void Spread_Fire(Vector3 _direction)
    {
        GameObject fire = Instantiate(fire_prefab, instantiation_point + (_direction * spawn_distance), Quaternion.identity) as GameObject;
        if(null != parent_object)
        {
            fire.transform.parent = parent_object.transform;
        }
        fire_spread_component comp = fire.GetComponent<fire_spread_component>();
        if(null != origin)
        {
            comp.origin = origin;
        }
        else
        {
            comp.origin = this.gameObject;
        }
        if(true == hit_flamable)
        {
            comp.origin = comp.gameObject;
        }
        comp.distance_from_start_origin = (comp.transform.position - comp.origin.transform.position).magnitude;
    }

    bool In_Range()
    {
        if(5 > distance_from_start_origin)
        {
            return true;
        }
        return false;
    }

    bool Check_Spread_Direction()
    {
        parent_object = null;
        hit_flamable = false;
        Vector3 start_position = this.transform.position;
        Ray object_ray = new Ray(start_position, spread_direction);
        RaycastHit object_hit;
        Physics.Raycast(object_ray, out object_hit, spawn_distance * 1.75f);

        if(null == object_hit.collider)
        {
            Vector3 turn_point = object_ray.GetPoint(spawn_distance);
            Ray floor_ray = new Ray(turn_point, -this.transform.up);
            RaycastHit floor_hit;
            Physics.Raycast(floor_ray, out floor_hit, 50);
            if(null != floor_hit.collider)
            {
                instantiation_point = floor_hit.point;
                instantiation_point.y += fire_collider.bounds.extents.y;
                return true;
            }
            return false;
        }
        else
        {
            if(true == object_hit.collider.CompareTag("flammable"))
            {
                hit_flamable = true;
                parent_object = object_hit.collider.gameObject;
                object_hit.collider.tag = "inflammable";

                return true;
            }
            return false;
        }

    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(life_time);
        Destroy(this.gameObject);
    }
}
