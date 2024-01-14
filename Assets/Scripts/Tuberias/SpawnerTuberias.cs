using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TuberiaData
{
    public float speed;
    public int lifeTime;
    [Tooltip("X es la altura minima, Y es la maxima")]
    public Vector2 minAndMaxHeight;
}
public class SpawnerTuberias : MonoBehaviour
{
    public GameObject platInicio;

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
        timer = spawnTime;
        platInicio.SetActive(true);
        StartCoroutine(DespawnStartPlatform(15));
    }

    // Update is called once per frame
    void Update()
    {
        platInicio.GetComponent<Rigidbody>().velocity = -Vector3.forward * tub_data.speed;

        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            GameObject tuberia = pool.GetFirstInactiveGameObject();
            if (tuberia != null)
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

    IEnumerator DespawnStartPlatform(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        platInicio.SetActive(false);
    }
}


