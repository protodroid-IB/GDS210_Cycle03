using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShelf : MonoBehaviour {

	[SerializeField]
	GameObject[] spawnGlasses;

	[SerializeField]
	Transform leftTransform, rightTransform;

	[SerializeField]
	int numberOfGlasses = 1;

	GameObject[] glasses;
	int index = 0;

	float spacing;

	List<int> cache = new List<int>();

	// Use this for initialization
	void Start () {
		glasses = new GameObject[numberOfGlasses];
		spacing = (rightTransform.position.x - leftTransform.position.x) / (numberOfGlasses);
		for(int i = 0; i < numberOfGlasses; i++)
		{
			int mod = i % spawnGlasses.Length;
			Vector3 glassPosition = new Vector3(leftTransform.position.x + (spacing * i), leftTransform.position.y, leftTransform.position.z);
			SpawnNewGlass(spawnGlasses[mod], glassPosition);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < glasses.Length; i++)
		{
			if (cache.Contains(i))
				break;

			if((glasses[i].transform.position.z - leftTransform.position.z) >= 0.2f && glasses[i].transform.parent == transform)
			{
				glasses[i].transform.parent = transform.parent;
			}

			if (glasses[i].transform.parent != transform)
			{
				cache.Add(i);
				GameObject newGlass = glasses[i];
				StartCoroutine(SpawnGlass(i));
			}
		}
	}

	void SpawnNewGlass(GameObject glass, Vector3 position)
	{
		GameObject spawned = Instantiate(glass, position, Quaternion.identity, transform);
		glasses[index] = spawned;
		index++;
	}

	IEnumerator SpawnGlass(int glassNumber)
	{
		yield return new WaitForSeconds(2);

		int mod = glassNumber % spawnGlasses.Length;
		GameObject glass;
		Vector3 position = new Vector3(leftTransform.position.x + (spacing * glassNumber), leftTransform.position.y, leftTransform.position.z);

		GameObject spawned = Instantiate(spawnGlasses[mod], position, Quaternion.identity, transform);
		glasses[glassNumber] = spawned;
		cache.Remove(glassNumber);
	}
}
