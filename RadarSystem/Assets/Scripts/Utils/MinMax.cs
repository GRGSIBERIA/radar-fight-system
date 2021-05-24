/*
 The MIT License (MIT)

Copyright (c) 2015 yumi nunoura

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct MinMax
{

	public float min;
	public float max;

	public float randomValue { get { return Random.Range(min, max); } }

	public MinMax(float min, float max)
	{
		this.min = min;
		this.max = max;
	}

	public float Clamp(float value)
	{
		return Mathf.Clamp(value, min, max);
	}
}

public class MinMaxRangeAttribute : PropertyAttribute
{

	public float minLimit;
	public float maxLimit;

	public MinMaxRangeAttribute(float minLimit, float maxLimit)
	{

		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
public class MinMaxRangeDrawer : PropertyDrawer
{

	const int numWidth = 50;
	const int padding = 5;

	MinMaxRangeAttribute minMaxAttribute { get { return (MinMaxRangeAttribute)attribute; } }

	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
	{

		EditorGUI.BeginProperty(position, label, prop);

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		Rect minRect = new Rect(position.x, position.y, numWidth, position.height);
		Rect sliderRect = new Rect(minRect.x + minRect.width + padding, position.y, position.width - numWidth * 2 - padding * 2, position.height);
		Rect maxRect = new Rect(sliderRect.x + sliderRect.width + padding, position.y, numWidth, position.height);

		SerializedProperty minProp = prop.FindPropertyRelative("min");
		SerializedProperty maxProp = prop.FindPropertyRelative("max");

		float min = minProp.floatValue;
		float max = maxProp.floatValue;
		float minLimit = minMaxAttribute.minLimit;
		float maxLimit = minMaxAttribute.maxLimit;

		min = Mathf.Clamp(EditorGUI.FloatField(minRect, min), minLimit, max);
		max = Mathf.Clamp(EditorGUI.FloatField(maxRect, max), min, maxLimit);
		EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, minLimit, maxLimit);

		minProp.floatValue = min;
		maxProp.floatValue = max;

		EditorGUI.EndProperty();
	}
}
#endif