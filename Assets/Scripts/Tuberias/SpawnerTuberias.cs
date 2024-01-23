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
    public float margin;

    public TuberiaData tub_data;
    public Texture2D[] textures;

    [Header("Pool")]
    public GameObject tuberia;
    public GameObjPool pool;

    float relativeHeight = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
        spawnTime = Mathf.Abs(spawnTime);
        platInicio.SetActive(true);

        Invoke(nameof(DespawnStartPlatform), 8);
    }

    // Update is called once per frame
    void Update()
    {
        //Hace que las tuberias spawneen mas rapido segun cuantos puntos tengamos, con un maximo de 40 puntos
        float spawnTimeMapped = (GameManager.instance.points <= 40 ? CoolFunctions.MapValues(GameManager.instance.points, 0, 40, spawnTime, spawnTime / 1.8f) : spawnTime / 1.8f);

        if (!GameManager.instance.playerDied)
        {
            //La plataforma de inicio donde spawneamos
            platInicio.GetComponent<Rigidbody>().velocity = -Vector3.forward * tub_data.speed * GameManager.instance.gameSpeed;

            //El tiempo de spawn va acorde a la velocidad del juego
            timer += Time.deltaTime * GameManager.instance.gameSpeed;
            if (timer > spawnTimeMapped)
            {
                //Coje el primer objeto inactivo de la pool
                GameObject tuberia = pool.GetFirstInactiveGameObject();
                if (tuberia != null)
                {
                    //Activa la tuberia y accede a su script
                    tuberia.SetActive(true);
                    Tuberias tubScript = tuberia.GetComponent<Tuberias>();

                    //Cambia su velocidad y tiempo de vida
                    tubScript.tub_speed = -tub_data.speed;
                    tubScript.tub_life = tub_data.lifeTime;

                    //Cambia la altura de la siguiente tuberia que spawnee para que no aparezca muy apartada de la anterior
                    float tubHeight;

                    do 
                    { 
                        tubHeight = Random.Range(tub_data.minAndMaxHeight.x, tub_data.minAndMaxHeight.y); 
                    }
                    while (Mathf.Abs(tubHeight - relativeHeight) > margin);
                    
                    relativeHeight = tubHeight;

                    tuberia.transform.position =
                        transform.position +
                        Vector3.up * tubHeight;

                    //randomiza las texturas de las tuberias
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
            //Pone la velocidad de la plataforma de inicio a 0
            platInicio.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }

    void DespawnStartPlatform()
    {
        platInicio.SetActive(false);
    }
}


