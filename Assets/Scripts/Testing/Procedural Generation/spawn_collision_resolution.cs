using UnityEngine;
using System.Collections;

namespace Spawning {

    public class spawn_collision_resolution : MonoBehaviour {
        
        public enum resolution_outcome { destroy, move_destroy_if_collided, move_spawn_anyway};

        public static void Resolve_Collision(GameObject _gameobject_to_check, resolution_outcome _collision_resolution_type) {
            if (Is_Colliding(_gameobject_to_check)) {
                if(resolution_outcome.destroy == _collision_resolution_type) {
                    Destroy(_gameobject_to_check);
                }
                else if(resolution_outcome.move_destroy_if_collided == _collision_resolution_type) {
                    Move_Colliding_Object(_gameobject_to_check);
                }
                else if(resolution_outcome.move_spawn_anyway == _collision_resolution_type) {
                    Move_Colliding_Object(_gameobject_to_check);
                }
            }
        }

        static bool Is_Colliding(GameObject _gameobject_to_check) {
            Collider col = _gameobject_to_check.GetComponent<Collider>();
            Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, _gameobject_to_check.transform.rotation, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
            return colliders.Length > 1; //1 represents the GameObject we got the collider from
            
        }

        static void Move_Colliding_Object(GameObject _gameobject_to_move) {
            Collider col = _gameobject_to_move.GetComponent<Collider>();
            Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, _gameobject_to_move.transform.rotation, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
            for(int i = 0;i < colliders.Length; i++) {
                if(colliders[i].gameObject != _gameobject_to_move) {

                }
            }
        }
    }
}
