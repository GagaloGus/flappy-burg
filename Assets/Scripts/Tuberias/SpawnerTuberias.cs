using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TuberiaData
{
    public float speed;
    public float lifeTime;
    [Tooltip("X es la altura minima, Y es la maxima")]
    public Vector2 minAndMaxHeight;
}
public class SpawnerTuberias : MonoBehaviour
{
    [Header("Tuberias")]
    public uint spawnTime;
    float timer;
    public TuberiaData tub_data;

    [Header("Pool")]
    public GameObject tuberia;
    public GameObjPool pool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime )
        {
            GameObject tuberia = pool.GetFirstInactiveGameObject();
            if( tuberia != null )
            {
                tuberia.SetActive(true);
                Tuberias tubScript = tuberia.GetComponent<Tuberias>();

                tubScript.tub_speed = -tub_data.speed;
                tubScript.tub_life = tub_data.lifeTime;

                tuberia.transform.position = 
                    transform.position + 
                    Vector3.up * Random.Range(tub_data.minAndMaxHeight.x, tub_data.minAndMaxHeight.y);
            }

            timer = 0;
        }
    }
}


