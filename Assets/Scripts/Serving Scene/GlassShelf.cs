using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShelf : MonoBehaviour {

	[SerializeField]
	GameObject wineGlass, beerGlass, tumbler;

	[SerializeField]
	Transform topLeft, bottomRight, middle;

	[SerializeField]
	int numberOfGlasses = 1;

	GameObject[] glasses;
	int index = 0;

	float spacing;

	List<int> cache = new List<int>();

	// Use this for initialization
	void Start () {
		glasses = new GameObject[(numberOfGlasses + 1) * 3];
		spacing = (bottomRight.position.x - topLeft.position.x) / (numberOfGlasses);
		for(int i = 0; i <= numberOfGlasses; i++)
		{
			Vector3 glassPosition = new Vector3(topLeft.position.x + (spacing * i), topLeft.position.y, topLeft.position.z);
			SpawnNewGlass(wineGlass, glassPosition);
			glassPosition.y = middle.position.y;
			SpawnNewGlass(tumbler, glassPosition);
			glassPosition.y = bottomRight.position.y;
			SpawnNewGlass(beerGlass, glassPosition);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < glasses.Length; i++)
		{
			if (cache.Contains(i))
				break;

			float sqrVelocity = glasses[i].GetComponent<Rigidbody>().velocity.sqrMagnitude;

			if((glasses[i].transform.position.z - topLeft.position.z) >= 0.2f && glasses[i].transform.parent == transform)
			{
				glasses[i].transform.parent = null;
			}

			if (glasses[i].transform.parent != transform && sqrVelocity >= 0.1f)
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
		int glassVertical = glassNumber % 3;
		int glassHorisontal = glassNumber / 3;

		GameObject glass;
		Vector3 position = new Vector3(topLeft.position.x + (spacing * glassHorisontal), topLeft.position.y, topLeft.position.z);
		switch (glassVertical)
		{
			case 0:
				glass = wineGlass;
				position.y = topLeft.position.y;
				break;
			case 1:
				glass = tumbler;
				position.y = middle.position.y;
				break;
			case 2:
				glass = beerGlass;
				position.y = bottomRight.position.y;
				break;
			default:
				glass = wineGlass;
				break;
		}
		GameObject spawned = Instantiate(glass, position, Quaternion.identity, transform);
		glasses[glassNumber] = spawned;
		cache.Remove(glassNumber);
	}
}
