using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement sceneManagement;

    [SerializeField] GameObject vrPlayer;
    [SerializeField] GameObject playerCollider;

    [SerializeField] GameObject hubWorldObjects;

    GameObject instancedObjects;

    Dictionary<Transform, bool> ignore = new Dictionary<Transform, bool>();

    Transform hubWorld;

    bool gameStarted = false;

    private void Awake()
    {
        if (sceneManagement != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(vrPlayer);

        vrPlayer.transform.position = GameManager.spawnPosition;

        sceneManagement = this;

        // Creates a parent to store all objects in the hubworld instance.
        instancedObjects = new GameObject();
        instancedObjects.name = "instanced_" + SceneManager.GetActiveScene().name;

        //Create a parent to store the objects that will not be destroyed.
        hubWorld = new GameObject().transform;
        hubWorld.name = "DoNotDisable_";
        vrPlayer.transform.SetParent(hubWorld);
        hubWorldObjects.transform.SetParent(hubWorld);
        playerCollider.transform.SetParent(hubWorld);

        // Setup ignore objects.
        ignore[transform] = true;
        ignore[vrPlayer.transform] = true;
        ignore[hubWorld] = true;

        MakeNewInstanceObjects(SceneManager.GetActiveScene().name);
    }

    // Sets the objects in the scene to be instanced.
    void MakeNewInstanceObjects(string loadScene)
    {
        // Create the catalogue
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

    // Turn the instanced objects off.
    public void LoadScene(string scene)
    {
        instancedObjects.SetActive(false);
        StartCoroutine(LoadSceneObjects(scene));
    }

    IEnumerator LoadSceneObjects(string scene)
    {

        // SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        yield return null;
    }

    // Turn instanced objects back on.
    public void ActivateHubworldInstance()
    {
        instancedObjects.SetActive(true);
        print("Active hubworld instance");
    }

}


