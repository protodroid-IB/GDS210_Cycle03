using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
    public static Vector3 spawnPosition = Vector3.zero;

    [SerializeField] GameObject[] tmProTextFeields;
    [SerializeField] Transform player;

    void Awake () {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Update()
    {
        foreach(GameObject tmProTextField in tmProTextFeields)
        {
            tmProTextField.transform.LookAt(2 * tmProTextField.transform.position - player.transform.position);
        }
    }
}
