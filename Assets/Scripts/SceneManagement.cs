using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public static SceneManagement sceneManagement;

    [SerializeField] GameObject vrPlayer;
    [SerializeField] GameObject playerCollider;

    [SerializeField] GameObject optionMenus;

    GameObject instancedObjects;

    // List of objects to be kept when the next scene is loaded.
    Dictionary<Transform, bool> ignore = new Dictionary<Transform, bool>();

    Transform doNotDisable;

    Vector3 playerSpawnPos;

    private void Awake()
    {
        if (sceneManagement != null)
        {
            Destroy(gameObject);
            return;
        }

        sceneManagement = this;

        playerSpawnPos = GameManager.spawnPosition;

        // The player will spawn in their current position when they restart the scene.
        vrPlayer.transform.position = playerSpawnPos;

        // Creates a parent to store all objects in the hubworld instance.
        instancedObjects = new GameObject();
        instancedObjects.name = "instanced_" + SceneManager.GetActiveScene().name;

        //Create a parent to store the objects that will not be destroyed.
        doNotDisable = new GameObject().transform;
        doNotDisable.name = "DoNotDisable_";
        vrPlayer.transform.SetParent(doNotDisable);
        playerCollider.transform.SetParent(doNotDisable);

        // Setup ignore objects.
        ignore[transform] = true;
        ignore[vrPlayer.transform] = true;
        ignore[doNotDisable] = true;
       // ignore[optionMenus.transform] = true;

        MakeNewInstanceObjects(SceneManager.GetActiveScene().name);

        if (GameManager.minigameRestarting)
        {
            DisableHubworldInstance();
            GameManager.minigameRestarting = false;
        }
    }

    // Sets the objects in the scene to be instanced.
    void MakeNewInstanceObjects(string loadScene)
    {
        // Creates a transform to store all gameobjects that should only appear in the HubWorldMaster scene.
        instancedObjects = new GameObject();
        instancedObjects.name = "instanced__" + loadScene;

        Transform[] allObjects = FindObjectsOfType<Transform>();
        foreach (Transform t in allObjects)
        {

            if (ignore.ContainsKey(t)) continue;

            if (t.parent == null)
            {
                t.SetParent(instancedObjects.transform);
            }
        }
    }

    // Disable the HubworldMaster sceene gameobjects and loads the scene.
    public void LoadScene(string scene)
    {
        instancedObjects.SetActive(false);
        StartCoroutine(LoadSceneObjects(scene));
    }

    public IEnumerator LoadSceneObjects(string scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        yield return null;
    }

    // Turn instanced objects off.
    public void DisableHubworldInstance()
    {
        instancedObjects.SetActive(false);
    }

    // Function to restart the minigame.
    public void RestartMiniGame(string minigame)
    {
        GameManager.spawnPosition = playerCollider.transform.position;
        GameManager.spawnPosition.y = 0;

        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(minigame, LoadSceneMode.Additive);

        GameManager.minigameRestarting = true;
    }
}


