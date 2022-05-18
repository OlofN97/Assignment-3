using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Toggle objectPoolToggle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawner;
    float outs = 5;

    void Awake()
    {
        player.SetActive(false);
        spawner.SetActive(false);
    }

    public void startButtonOnPressed()
    {

        InitializeObjectPools(); //this needs to be first
        player.SetActive(true);
        spawner.SetActive(true);
    }

    public void Update()
    {

        if (1 / Time.deltaTime <= 5)
        {
            outs--;
            if (outs == 0)
            {
                DisableEverything();
            }

        }
        else
        {
            outs = 5;
        }
    }

    public void resetButtonOnPressed()
    {
        SceneManager.LoadScene("Main");
    }

    private void InitializeObjectPools()
    {
        spawner.GetComponent<Spawner>().UseObjectPool = objectPoolToggle.isOn;
        player.GetComponent<PlayerScript>().UseObjectPool = objectPoolToggle.isOn;

    }
    private void DisableEverything()
    {

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> objectsToDisable = new List<GameObject>(allObjects);
        int amountOfObj = 0;
        foreach (GameObject a in objectsToDisable)
        {
            amountOfObj++;
            if (a != gameObject)
                a.SetActive(false);
        }

        Debug.Log("Bullets spawned" + player.GetComponent<PlayerScript>().objSpawned);
        Debug.Log("Units spawned" + spawner.GetComponent<Spawner>().objSpawned);
        Time.timeScale = 0;
        Debug.Break();

    }
}
