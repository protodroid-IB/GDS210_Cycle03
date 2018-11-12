using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour {

	List<Ingredient> ingredients = new List<Ingredient>();

	bool ice;

	enum MixMethod { Shaken, Stirred, Mixed }
	MixMethod method;

	public void AddIngredient(Ingredient ingredient)
	{
		ingredients.Add(ingredient);
	}

	public void Clear()
	{
		ingredients.Clear();
	}

	public void AddIce()
	{
		ice = true;
	}

	void Combine(MixMethod mixMethod)
	{
		method = mixMethod;
	}



}
