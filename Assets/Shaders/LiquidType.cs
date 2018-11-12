using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Liquids/New Liquid")]
public class LiquidType : ScriptableObject {

	public float MaxWobble = 0.03f;
	public float WobbleSpeed = 1f;
	public float Recovery = 1f;
	public float wobbleSmooth;
	public Color liquidColor;
	public Color foamColor;
	public float maxFill, minFill;
}
