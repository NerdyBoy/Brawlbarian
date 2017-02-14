using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class fire_spread_component : MonoBehaviour {
    //comment to commit
    /*public float life_time;
    public GameObject origin;
    public float distance_from_start_origin;
    public float max_range = 5;
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
            //fire.transform.parent = parent_object.transform;
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
        if(max_range > distance_from_start_origin)
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
    }*/

    public float lifetime = 5;
    public float grow_speed = 2;
    SphereCollider sphere_collider;
    List<health_component> health_components;
    List<GameObject> fire_objects;
    public GameObject fire_object;
    public GameObject fire_spread_object;
    ParticleSystem flame;
    bool has_spread = false;
    int fires_per_ring;

    void Start()
    {
        fire_objects = new List<GameObject>();
        fire_spread_object = this.gameObject;
        health_components = new List<health_component>();
        sphere_collider = GetComponent<SphereCollider>();
        if(sphere_collider == null)
        {
            sphere_collider = this.gameObject.AddComponent<SphereCollider>();
        }
        sphere_collider.isTrigger = true;
        flame = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(Spread());
    }

    IEnumerator Spread()
    {
        float running_time = 0.0f;
        float end_time = Time.fixedTime + lifetime;
        while(Time.fixedTime < end_time)
        {
            running_time += Time.deltaTime;
            sphere_collider.radius += grow_speed * Time.deltaTime;
            if(running_time > grow_speed)
            {
                Spawn_Particle_Systems(this.transform.position, sphere_collider.radius);
                running_time = 0;
            }
            for(int i = 0; i < health_components.Count; i++)
            {
                if (health_components[i] != null)
                {
                    health_components[i].On_Modify_Health(-30);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        //Destroy(this.gameObject);
        StartCoroutine(Destroy_Object());
    }

    void Spawn_Particle_Systems(Vector3 _center, float _radius)
    {
        for(int i = 0; i < 360; i += 15)
        {
            Vector3 position = new Vector3(
                    _center.x + _radius * Mathf.Sin(i * Mathf.Deg2Rad),
                    _center.y,
                    _center.z + _radius * Mathf.Cos(i * Mathf.Deg2Rad)
            );
            GameObject fire = Instantiate(fire_object, position, flame.transform.rotation) as GameObject;
            fire.transform.parent = this.transform;
            fire_objects.Add(fire);
        }
    }    

    IEnumerator Destroy_Object()
    {
        while(fire_objects.Count > 0)
        {
                Destroy(fire_objects[0]);
                fire_objects.RemoveAt(0);
                yield return new WaitForSeconds(0.025f);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider _col)
    {
        if(_col.CompareTag("flammable") == true)
        {
            health_component component = _col.gameObject.GetComponent<health_component>();
            if(component != null)
            {
                health_components.Add(component);
            }
        }
    }
}
