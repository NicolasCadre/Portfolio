using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSkullScript : MonoBehaviour
{
    [SerializeField] private float cooldownFallSkull = 0f;
    [Header("Cadence maximale d'apparition des skulls a fixer en fonction du challenge")]
    [SerializeField] private float cooldownFallSkullMax = 2f;
    public float cooldownFallSkullRange;
    [Space(20)]

    [SerializeField] private int nbSkull = 1;
    public int nbSkullMax;
    [Header("Poser les differents prefabs Skulls ici")]
    public GameObject[] skull;
    [Header("Nommer le GameObject en question SkullSpawnHeight pour qu'Unity le trouve dans le Start")]
    [SerializeField] private GameObject skullSpawn;
    [Space(20)]
    private float skullFallXMax, skullFallXMin, skullFallZMax, skullFallZMin, skullFallYMin, skullFallYMax;

    [Header("Radius autour du skullSpawn A fixer en fonction de la taille du niveau")]
    [SerializeField] private float maxRadiusX, maxRadiusZ, maxHeight;


    void Start()
    {
        //Fixe les points d'apparitions possibles des skulls
        skullSpawn = GameObject.Find("SkullSpawnHeight");
        skullFallYMin = skullSpawn.transform.position.y;
        SetupSpawn(skullSpawn);
    }

    void FixedUpdate()
    {
        //Fait apparaître des skulls à la fin du cooldown
        cooldownFallSkull += Time.deltaTime;

        if (cooldownFallSkull >= cooldownFallSkullMax)
        {
            for (int i =0; i < nbSkull; i++)
            {
                SpawnSkull();
            }
            cooldownFallSkullMax = Random.Range(0.5f, cooldownFallSkullRange);
            cooldownFallSkull = 0f;
            nbSkull = Random.Range(1, nbSkullMax);
        }
    }

    // Initialise les valeurs minimales et maximales de point d'apparitions des skulls utilisé dans SpawnSkull
    void SetupSpawn(GameObject skullSpawn)
    {
        skullFallXMax = skullSpawn.transform.position.x + maxRadiusX;
        skullFallXMin = skullSpawn.transform.position.x - maxRadiusX;
        skullFallZMax = skullSpawn.transform.position.z + maxRadiusZ;
        skullFallZMin = skullSpawn.transform.position.z - maxRadiusZ;
        skullFallYMax = skullSpawn.transform.position.y + maxHeight;
    }

    void SpawnSkull()
    {
        //Génère les skulls à une position et hauteur aléatoire du niveau
        int skullChosenType;
        float RandomX = Random.Range(skullFallXMin, skullFallXMax);
        float RandomY = Random.Range(skullFallYMin, skullFallYMax);
        float RandomZ = Random.Range(skullFallZMin, skullFallZMax);
        int skullValue = Random.Range(0, 10);
        if(skull.Length>4)
        {
            if (skullValue <=2)
            {
                 skullChosenType = Random.Range(0, 4);
            }
            else
            {
                 skullChosenType = Random.Range(4, skull.Length);
            }
        }
        else
        {
             skullChosenType = Random.Range(0, skull.Length);
        }
        GameObject currentSkull = Instantiate(skull[skullChosenType], new Vector3(RandomX, RandomY, RandomZ), Random.rotation);
    }
    

}
