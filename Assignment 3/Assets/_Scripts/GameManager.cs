
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Toggle objectPoolToggle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawner;

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

    public void resetButtonOnPressed()
    {
        SceneManager.LoadScene("Main");
    }

    private void InitializeObjectPools()
    {
        spawner.GetComponent<Spawner>().UseObjectPool = objectPoolToggle.isOn;
        player.GetComponent<PlayerScript>().UseObjectPool = objectPoolToggle.isOn;

    }


}
