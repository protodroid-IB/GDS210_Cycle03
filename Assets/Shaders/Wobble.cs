using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serving
{
	public class Wobble : MonoBehaviour
	{
		Renderer rend;

		Vector3 lastVelocity, lastAngularVelocity, lastPos, lastRot;

		Vector3 velocity;

		Vector3 acceleration, angularAcceleration, angularVelocity;

		[Range(0, 1)]
		public float fillLevel;

		float fillAmount;

		[HideInInspector]
		public Ingredient ingredient;

		Color fillColor, foamColor;

		[SerializeField]
		bool unevenBase;

		[SerializeField]
		float centreFill, varianceUpright, varianceOnSide, varianceBetweenSides;

		[SerializeField]
		float maxFill = 1;

		float maxWobble = 2, wobbleSpeed = 5, wobbleSmooth = 0.02f, recovery = 0.1f;

		float wobbleAmountX, wobbleAmountZ;

		float totalWobbleX, totalWobbleZ;

		float velocityX, velocityZ;

		// Use this for initialization
		void Start()
		{
			rend = GetComponent<Renderer>();
			SetColours();
		}

		private void Update()
		{
			SetFillLevel();
			
			SetValues();


			if (fillLevel <= 0)
			{
				acceleration = Vector3.zero;
				angularAcceleration = Vector3.zero;
				totalWobbleX = 0;
				totalWobbleZ = 0;
			}

			totalWobbleX += Mathf.Clamp((acceleration.x + (angularAcceleration.z * 0.02f)) * maxWobble, -maxWobble, maxWobble);
			totalWobbleZ += Mathf.Clamp((acceleration.z + (angularAcceleration.x * 0.02f)) * maxWobble, -maxWobble, maxWobble);

			wobbleAmountX = SetSmooth(wobbleAmountX, totalWobbleX, ref velocityX);

			wobbleAmountZ = SetSmooth(wobbleAmountZ, totalWobbleZ, ref velocityZ);

			totalWobbleX = SetTotal(wobbleAmountX, totalWobbleX);

			totalWobbleZ = SetTotal(wobbleAmountZ, totalWobbleZ);

			rend.material.SetFloat("_WobbleX", wobbleAmountX);
			rend.material.SetFloat("_WobbleZ", wobbleAmountZ);
			rend.material.SetFloat("_FillAmount", fillAmount);
		}

		/// <summary>
		/// sets the smoothing of the floats to simulate gravity affecting the value changes
		/// </summary>
		/// <param name="wobbleAmount"></param>
		/// <param name="totalWobble"></param>
		/// <param name="velocity"></param>
		/// <returns>smoothed wobble amount</returns>
		float SetSmooth(float wobbleAmount, float totalWobble, ref float velocity)
		{
			wobbleAmount = Mathf.SmoothDamp(wobbleAmount, totalWobble, ref velocity, wobbleSmooth, wobbleSpeed, Time.deltaTime);
			return wobbleAmount;
		}

		public void SetColours()
		{
			if (ingredient)
			{
				fillColor = ingredient.colour;
				foamColor = fillColor + (Color.white * 0.1f);
				rend.material.SetColor("_Tint", fillColor);
				rend.material.SetColor("_FoamColor", foamColor);
				rend.material.SetColor("_TopColor", foamColor);
			}
		}

		void SetFillLevel()
		{
			float dot = Vector3.Dot(Vector3.up, transform.up);
			dot = Mathf.Abs(dot);
			float baseVariance = varianceOnSide;
			if (unevenBase)
			{
				float baseDot = Vector3.Dot(Vector3.up, transform.right);
				baseDot = Mathf.Abs(baseDot);
				baseVariance = Mathf.Lerp(varianceOnSide, varianceBetweenSides, baseDot);
			}
			float variance = Mathf.Lerp(baseVariance, varianceUpright, dot);
			fillLevel = Mathf.Clamp(fillLevel, 0, maxFill);
			fillAmount = (fillLevel - 0.5f) * (variance) + centreFill;
		}

		/// <summary>
		/// Set the values for velocity, acceleration and the angular equivalents
		/// </summary>
		void SetValues()
		{
			velocity = transform.position - lastPos;

			acceleration = velocity - lastVelocity;

			angularVelocity = transform.rotation.eulerAngles - lastRot;

			angularAcceleration = angularVelocity - lastAngularVelocity;

			lastPos = transform.position;

			lastRot = transform.rotation.eulerAngles;

			lastVelocity = velocity;

			lastAngularVelocity = angularVelocity;
		}

		/// <summary>
		/// set the wobble reversing depending on the current wobble amount and the total
		/// </summary>
		/// <param name="wobbleAmount"></param>
		/// <param name="totalWobble"></param>
		/// <returns>total amount of wobble</returns>
		float SetTotal(float wobbleAmount, float totalWobble)
		{
			if (wobbleAmount >= (totalWobble - (totalWobble / 10)) && totalWobble >= 0)
			{
				totalWobble = Mathf.Lerp(totalWobble, 0, recovery) * -1;
			}
			else if (wobbleAmount <= (totalWobble - (totalWobble / 10)) && totalWobble <= 0)
			{
				totalWobble = Mathf.Lerp(totalWobble, 0, recovery) * -1;
			}
			return totalWobble;
		}

		/// <summary>
		/// set the input parameters for testing purposes
		/// </summary>
		void InputFunctions()
		{

			if (Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate(Vector3.forward * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				transform.Translate(Vector3.forward * Time.deltaTime * -1);
			}

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(Vector3.left * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(Vector3.left * Time.deltaTime * -1);
			}
		}


	}
}