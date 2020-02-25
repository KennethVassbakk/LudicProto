/**
 *  PoolManager for Unity3D
 *  Author: Kenneth "MrSpectacle" Vassbakk (kenneth.vassbakk@gmail.com)
 *  Version: 1.0
 *  Updates:
 *      -- Initial Version
 *  
 *  Usage:
 *      There's no special need for setup.
 *      
 *      Instead of using GameObject.Instantiate();
 *      Use: PoolManager.Spawn(GameObject prefab, Vector3 Position, Quaternion Rotation);
 *      
 *      Instead of using GameObject.Destroy();
 *      Use: PoolManager.Despawn(GameObject prefab)
 *      
 *      You can preload a set number of GameObjects in the pool using:
 *      PoolManager.Preload(GameObject prefab, int Quantity);
 *      
 *      If for some reason you want to decrease the number of objects in a pool you can use:
 *      PoolManager.Clear(GameObject prefab, int NumberOfRemainingObjects);
 *      
 *      If for some reason you want to remove all objects in a pool you can use:
 *      PoolManager.Clear(GameObject prefab);
 *      
 *      If for some reason you want to remove all pools in the PoolManager, use:
 *      PoolManager.Clear();
 *      
 *  Variables:
 *      If you want to change the name of the GameObject in the scene that holds all the pooled objects,
 *      change the: const string PARENT_NAME variable.
 *      
 *      Depending on what you want, you can choose to create a hierarchy for the created pools.
 *      This can be changed using the const bool NESTED_OBJECTS variable.
 *          Examples of this:
 *              - PoolManager
 *                  - Some_Prefab Pool
 *                      - Some_Prefab (1)
 *                      - Some_Prefab (2)
 *      
 *  Notes:
 *      - Using the Clear() functions will not remove any active objects, only pooled objects that are hidden.
 *      - On Start() {} will only run the first time an object is instantiated, 
 *        if you want to run something (i.e. reset HP, etc, or anything you'd normally place in start) use void OnEnable(); instead
 *        as well as OnDisable() for when the object is removed.
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable SuggestVarOrType_Elsewhere
#pragma warning disable 162

// ReSharper disable InconsistentNaming

namespace PoolManager {
    public static class PoolManager {
        // You can avoid resizing the size of the list by setting this to a number equal or greater than
        // What you expect mos of your pool sizes to be
        // You can also use Preload() to set the initial size of a pool -- this can be handy.
        // ReSharper disable once InconsistentNaming
        private const int DEFAULT_POOL_SIZE = 3;

        // A dictionary of all our _pools
        private static Dictionary<GameObject, Pool> _pools;

        // Parent name
        private const string PARENT_NAME = "PoolManager";

        // The parent which holds all the instantiated objects. 
        public static Transform parent;

        // Nest pooled objects into transforms according to their GameObject name.
        // This takes a tiny bit more memory, but is more organized in the editor
#if UNITY_EDITOR
        private const bool NESTED_OBJECTS = true;
#else
        private const bool NESTED_OBJECTS = false;
#endif

        /// <summary>
        /// Initialize our dictionary.
        /// </summary>
        /// <param name="prefab">GameObject Prefab</param>
        /// <param name="qty">int Quantity</param>
        private static void init(GameObject prefab = null, int qty = DEFAULT_POOL_SIZE) {
            if (_pools == null) {
                _pools = new Dictionary<GameObject, Pool>();

                if (parent == null) {
                    GameObject p = GameObject.Find(PARENT_NAME) ?? new GameObject(PARENT_NAME);

                    parent = p.transform;
                }
            }

            if (prefab != null && _pools.ContainsKey(prefab) == false) {
                _pools[prefab] = new Pool(prefab, qty);

                if (parent == null) return;

                if (NESTED_OBJECTS) {
                    Transform nParent = parent.Find(prefab.name + " Pool");

                    if (nParent == null) {
                        GameObject obj = new GameObject {name = prefab.name + " Pool"};
                        obj.transform.SetParent(parent);
                        obj.transform.position = Vector3.zero;
                        nParent = obj.transform;
                    }

                    _pools[prefab].Parent = nParent;
                } else {
                    _pools[prefab].Parent = parent;
                }

            }
        }

        /// <summary>
        /// If you want to preload a few copies of an object at the start of a scene,
        /// you can use this. Really not needed unless your going from zero instances to more than ten.
        /// </summary>
        /// <param name="prefab">GameObject Prefab</param>
        /// <param name="qty">int Quantity</param>
        public static void Preload(GameObject prefab, int qty = 1) {
            // Initialize the prefab into our dictionary of _pools.
            // Does nothing if it's already in there.
            init(prefab, qty);

            // Make an array to grab the objects we're about to pre spawn
            GameObject[] objs = new GameObject[qty];
            for (int i = 0; i < qty; i++) {
                objs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
            }

            // Now despawn them all!
            for (int i = 0; i < qty; i++) {
                Despawn(objs[i]);
            }
        }

        /// <summary>
        /// Method to clear the entire PoolManager
        /// </summary>
        public static void Clear() {
            while (_pools.Count > 0) {
                _pools.FirstOrDefault().Value.Clear(0);
            }

            if (_pools.Count == 0) {
                _pools = null;
            }

            if (parent.childCount != 0) return;
            Object.Destroy(parent.gameObject);
            parent = null;
        }

        /// <summary>
        /// A method to reduce the amount of pooled objects in a pool
        /// will remove available objects until it reaches the Qty.
        /// Pooled objects that are in use are ignored.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="Qty"></param>
        public static void Clear(GameObject prefab, int Qty) {
            if (_pools.ContainsKey(prefab))
                _pools[prefab].Clear(Qty);
        }

        /// <summary>
        /// Clear an entire object pool
        /// </summary>
        /// <param name="prefab">GameObject Prefab</param>
        public static void Clear(GameObject prefab) {
            if (_pools.ContainsKey(prefab))
                _pools[prefab].Clear(0);
        }

        /// <summary>
        /// Removes a pool from the PoolManager
        /// </summary>
        /// <param name="prefab">GameObject prefab</param>
        public static void Dispose(GameObject prefab) {
            if (_pools[prefab].Parent.childCount == 0) {
                _pools[prefab].Parent.SetParent(null);
                Object.Destroy(_pools[prefab].Parent.gameObject);
            }

            _pools.Remove(prefab);
        }

        /// <summary>
        /// Spawns a copy of the specified prefab (instantiating one if required).
        /// NOTE: Remember that Awake() and/or Start() will only run on the very first spawn,
        ///       and that it's variables wont be reset. Use OnEnable() and OnDisable() instead.
        /// </summary>
        /// <param name="prefab">GameObject Prefab</param>
        /// <param name="pos">Vector3 Position</param>
        /// <param name="rot">Quaternion Rotation</param>
        /// <returns>Returns pooled Object</returns>
        public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot) {
            // Initialize the prefab to the _pools.
            // Does nothing if it's already in there.
            init(prefab);
            return _pools[prefab].Spawn(pos, rot);
        }

        /// <summary>
        /// Despawns a object in the pool.
        /// If the item is not created from a pool, it will be destroyed.
        /// </summary>
        /// <param name="obj"></param>
        public static void Despawn(GameObject obj) {
            PoolItem pi = obj.GetComponent<PoolItem>();
            if (pi == null) {
                Debug.Log("PoolManager - Object '" + obj.name + "' wasn't spawned from a pool. Destroying it instead.");
                Object.Destroy(obj);
            } else {
                pi.myPool.Despawn(obj);
            }
        }

        /// <summary>
        /// The Pool class is created for each different type of GameObject that is 
        /// put into the PoolManager.
        /// </summary>
        public class Pool {
            // We append an ID to the spawned GameObjects
            // Just for organizational benefits.
            private int nextId;

            // The structure containing our available objects.
            // Using stack instead of a list, because we'll never need to pluck
            // an object from the start or middle of the array.
            // We'll always just grab the last one, which eliminates
            // any need to shuffle the objects around in memory
            private readonly Stack<GameObject> available;

            // The prefab we are pooling
            private readonly GameObject prefab;

            // The parent we should be setting our spawned objects to.
            public Transform Parent { get; set; }

            // Constructor
            public Pool(GameObject prefab, int initQty) {
                this.prefab = prefab;

                // Create the stack of available GameObjects.
                available = new Stack<GameObject>(initQty);
            }
            // TASK: Something


            /// <summary>
            /// Spawn an object from our pool,
            /// or generate a new one.
            /// </summary>
            /// <param name="pos">Vector3 Position</param>
            /// <param name="rot">Quaternion Rotation</param>
            /// <returns>GameObject Pooled Object</returns>
            public GameObject Spawn(Vector3 pos, Quaternion rot) {
                GameObject obj;

                if (available.Count == 0) {
                    // We don't have an available object in our pool
                    // so we have to instantiate a new one.
                    obj = (GameObject)Object.Instantiate(prefab, pos, rot);
                    obj.name = prefab.name + " (" + (nextId++) + ")";

                    // we add a PoolItem component so we know which pool we belong to
                    obj.AddComponent<PoolItem>().myPool = this;

                    // Set the parent of the object, if we have one.
                    if (Parent)
                        obj.transform.SetParent(Parent);
                } else {
                    // We have an available object!
                    obj = available.Pop();

                    if (obj == null) {
                        // The available object we expected to find no longer exists.
                        // Causes might be:
                        //  - Someone calling Destroy() on our object
                        //  - A scene change (which will destroy all objects)
                        //    NOTE: This could be prevented with a DontDestroyOnLoad if you really don't want this to happen
                        // Spawn a new object!

                        return Spawn(pos, rot);
                    }
                }

                // Time to set the properties of the object, and set it to active!
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);
                return obj;
            }

            /// <summary>
            /// Despawns an object in our pool,
            /// setting it to disabled.
            /// </summary>
            /// <param name="obj">GameObject obj</param>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public void Despawn(GameObject obj) {
                obj.SetActive(false);

                // Push the object back into the available stack.
                available.Push(obj);
            }

            /// <summary>
            /// Removes objects from pool until it has less than Qty
            /// </summary>
            /// <param name="qty">Int Quantity</param>
            public void Clear(int qty) {
                while (available.Count > qty) {
                    GameObject obj = available.Pop();

                    // Removing the parent of the object, so that (if no children) we can remove the parent.
                    // This won't be able to happen otherwise due to GarbageCollection.
                    obj.transform.SetParent(null);
                    Object.Destroy(obj);
                }

                // If the quantity was set to zero, destroy this pool.
                if (qty == 0) {
                    Dispose(prefab);
                }
            }
        }

        /// <summary>
        /// This class is added to spawned items, so that we know which pool
        /// the item belongs to.
        /// </summary>
        public class PoolItem : MonoBehaviour {
            public Pool myPool;
        }
    }
}