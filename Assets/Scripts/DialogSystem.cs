using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
	[Header("UI组件")]
	public Text textLabel;
	public Image faceImage;

	[Header("文本文件")]
	public TextAsset textFile;

	[Header("头像")]
	public Sprite face01, face02;

	private List<string> textList = new List<string>();
	private int index = 0;
	private bool textFinished;
	private bool cancelTyping;

	private void Awake()
	{
		CreatFromText(textFile);
		textFinished = true;
	}

	private void OnEnable()
	{
		StartCoroutine(SetTextUI());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			// 当指针达到最后的一个时，关闭对话框
			if(index == textList.Count)
			{
				gameObject.SetActive(false);
				index = 0;
				return;
			}else if(textFinished && !cancelTyping)
			{
				// 如果对话结束并且不是快进的情况下
				StartCoroutine(SetTextUI());
			}else if(!textFinished && !cancelTyping)
			{
				cancelTyping = !cancelTyping;
			}
		}
	}

	/// <summary>
	/// 将TextAsset中的文本按换行符分割开来
	/// </summary>
	/// <param name="text"></param>
	private void CreatFromText(TextAsset text)
	{
		textList.Clear();
		var lineData =  text.text.Split('\n');

		foreach(string line in lineData)
		{
			textList.Add(line);
			Debug.Log(line + "count:"+ line.Length);
		}
	}

	private IEnumerator SetTextUI()
	{
		textLabel.text = "";
		textFinished = false;

		switch (textList[index])
		{
			case "A":
				faceImage.sprite = face01;
				index++;
				break;
			case "B":
				faceImage.sprite = face02;
				index++;
				break;
		}
		
		int letter = 0;
		while(!cancelTyping && letter < textList[index].Length)
		{
			textLabel.text += textList[index][letter];
			letter++;
			yield return new WaitForSeconds(0.1f);
		}
		textLabel.text = textList[index];
		cancelTyping = false;
		textFinished = true;
		index++;
	}
}
