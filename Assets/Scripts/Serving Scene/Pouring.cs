﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{

	[RequireComponent(typeof(ParticleSystem))]
	public class Pouring : MonoBehaviour {

		public ParticleSystem pourParticle;
		ParticleSystem.TrailModule trail;
		ParticleSystem.MainModule main;
		public ParticleSystem.EmissionModule emission;

		
		List<ParticleCollisionEvent> particleCollisions = new List<ParticleCollisionEvent>();

		[SerializeField]
		List<GameObject> collisions = new List<GameObject>();

		float streamWidth;

		[SerializeField]
		float minWidth = 0.25f, maxWidth = 0.75f;

		float streamDensity;

		[SerializeField]
		float minDensity = 0.075f, maxDensity = 1f;


		public float strength;

		[SerializeField]
		Ingredient ingredient;

		public Color streamColour;

		// Use this for initialization
		void Start() {
			pourParticle = GetComponent<ParticleSystem>();
			trail = pourParticle.trails;
			main = pourParticle.main;
			emission = pourParticle.emission;
			streamColour = ingredient.colour;

			Drink[] glassesColliders = FindObjectsOfType<Drink>();
			for(int i = 0; i < glassesColliders.Length; i++)
			{
				pourParticle.trigger.SetCollider(i, glassesColliders[i].GetComponent<Collider>());
			}
			
		}

		private void OnParticleCollision(GameObject other)
		{
			collisions.Add(other);
			if(pourParticle.GetCollisionEvents(other, particleCollisions) > 0)
			{
				Drink glass = other.GetComponent<Drink>();
				if (glass)
				{
					glass.AddIngredient(ingredient, Time.deltaTime);
				}
			}
		}

		// Update is called once per frame
		void Update() {
			SetStrength();
			trail.widthOverTrail = streamWidth;
			//trail.lifetime = streamDensity;
			emission.rate = streamDensity;
			main.startColor = streamColour;
		}

		void SetStrength()
		{
			strength = Mathf.Clamp01(strength);
			float widthVariance = maxWidth - minWidth;
			streamWidth = strength * widthVariance + minWidth;

			float densityVariance = maxDensity - minDensity;
			streamDensity = strength * densityVariance + minDensity;
		}
	}
}