using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
	[Header("UI���")]
	public Text textLabel;
	public Image faceImage;

	[Header("�ı��ļ�")]
	public TextAsset textFile;

	[Header("ͷ��")]
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
			// ��ָ��ﵽ����һ��ʱ���رնԻ���
			if(index == textList.Count)
			{
				gameObject.SetActive(false);
				index = 0;
				return;
			}else if(textFinished && !cancelTyping)
			{
				// ����Ի��������Ҳ��ǿ���������
				StartCoroutine(SetTextUI());
			}else if(!textFinished && !cancelTyping)
			{
				cancelTyping = !cancelTyping;
			}
		}
	}

	/// <summary>
	/// ��TextAsset�е��ı������з��ָ��
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
