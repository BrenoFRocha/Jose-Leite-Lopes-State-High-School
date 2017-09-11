using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GridPosition : MonoBehaviour
{
	public bool setting;
	public GameObject[] objectsPath, objectsMenu, objectsIntro;
	public GameObject warning;
	private InputField input_i, input_j;
	public static bool allowClick;
	
	float targetZoom = 4;
	void Awake()
	{
		setting = true;
	}
	void Start ()
	{
		allowClick = true;
		input_i = GameObject.Find("SizeI").GetComponent<InputField>();
		input_j = GameObject.Find("SizeJ").GetComponent<InputField>();
	}
	
	float lerp(float a, float b, float f) 
	{
		return (a * (1.0f - f)) + (b * f);
	}
	
	void Update ()
	{
		if(setting)
		{
			
			for(int i = 0; i < objectsPath.Length; i++)
			{
				if(i < 5)
				{
					objectsMenu[i].SetActive(true);
				}
				objectsPath[i].SetActive(false);
			}
		}
		else
		{
			for (int i = 0; i < objectsPath.Length; i++)
			{
				if (i < 5)
				{
					objectsMenu[i].SetActive(false);
				}
				objectsPath[i].SetActive(true);
			}
		}
		gameObject.GetComponent<Camera>().orthographicSize = lerp(gameObject.GetComponent<Camera>().orthographicSize, targetZoom, .2f * 45f * Time.deltaTime);
	}
	public void More()
	{
		if(targetZoom > 1)
		{
			targetZoom -= 1;
		}
	}
	public void Less()
	{
		if (targetZoom < 50)
		{
			targetZoom += 1;
		}
	}
	public void Get()
	{
		if (int.Parse(input_i.text) < 2 || int.Parse(input_j.text) < 2)
		{
			if(!warning.activeSelf)
			{
				warning.SetActive(true);
			}
		}
		else
		{
			for(int i = 0; i < objectsIntro.Length; i ++)
			{
				objectsIntro[i].SetActive(false);
			}
			if (warning.activeSelf)
			{
				warning.SetActive(false);
			}
			setting = false;
			objectsPath[0].GetComponent<GridCreator>().sizeX = int.Parse(input_i.text);
			objectsPath[0].GetComponent<GridCreator>().sizeY = int.Parse(input_j.text);
			objectsPath[0].GetComponent<GridCreator>().makeAgain = true;
		}
		allowClick = true;
	}
	public void Set()
	{
		targetZoom = 4;
		objectsPath[0].GetComponent<Pathfinder>().start = false;
		try
		{
			if(GameObject.Find("Ball").activeSelf)
			{
				GameObject.Find("Ball").SetActive(false);
			}
			if(GameObject.Find("WarningPath").activeSelf)
			{
				GameObject.Find("WarningPath").SetActive(false);
			}
		}
		catch { }
		foreach (Transform child in objectsPath[0].transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		GameObject.Find("TextPath").GetComponent<Text>().text = "Start";
		for (int i = 0; i < objectsIntro.Length; i++)
		{
			objectsIntro[i].SetActive(true);
		}
		setting = true;
		allowClick = true;
	}
	public void OnEnter()
	{
		allowClick = false;
	}
	public void OnExit()
	{
		allowClick = true;
	}
}
