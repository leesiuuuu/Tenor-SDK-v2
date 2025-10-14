using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCheck : MonoBehaviour
{
	[SerializeField]
	private KeywordRandomGIFLoad loader;

	private InputField field;
	private void Start()
	{
		field = GetComponent<InputField>();
	}

	public void TagCheck(string value)
	{
		foreach(string s in loader.ResultObject.tags)
		{
			if (s.ToLower() == value.ToLower())
			{
				Debug.Log("정답!");
				field.text = "";
				field.ActivateInputField();
				loader.RandomSearchTenorGIF();
				return;
			}
		}
		Debug.Log("오답!");
		field.ActivateInputField();
	}
}
