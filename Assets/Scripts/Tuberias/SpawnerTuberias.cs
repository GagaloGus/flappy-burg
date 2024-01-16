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
    public float spawnTime;
    float timer;
    public TuberiaData tub_data;
    public Texture2D[] textures;

    [Header("Pool")]
    public GameObject tuberia;
    public GameObjPool pool;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
        spawnTime = Mathf.Abs(spawnTime);
        platInicio.SetActive(true);
        StartCoroutine(DespawnStartPlatform(8));
    }

    // Update is called once per frame
    void Update()
    {
        float spawnTimeMapped = CoolFunctions.MapValues(GameManager.instance.points, 0, 20, spawnTime, spawnTime / 2);

        if (!GameManager.instance.playerDied)
        {
            platInicio.GetComponent<Rigidbody>().velocity = -Vector3.forward * tub_data.speed * GameManager.instance.gameSpeed;

            timer += Time.deltaTime * GameManager.instance.gameSpeed;
            if (timer > spawnTimeMapped)
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

                    Material matArriba = tuberia.transform.Find("tubo arriba").GetComponent<MeshRenderer>().material;
                    Material matAbajo = tuberia.transform.Find("tubo abajo").GetComponent<MeshRenderer>().material;
                    int rndTexture = Random.Range(0, textures.Length);

                    matArriba.SetTexture("_MainTex", textures[rndTexture]);
                    matAbajo.SetTexture("_MainTex", textures[rndTexture]);
                }

                timer = 0;
            }
        }
        else
        {
            platInicio.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StopAllCoroutines();
        }

    }

    IEnumerator DespawnStartPlatform(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        platInicio.SetActive(false);
    }
}


