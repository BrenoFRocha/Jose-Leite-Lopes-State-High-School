using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour 
{
	private GridCreator manager;
	private bool thereIsOne;
	private int initial;
	private Pathfinder pathfinder;
	void Start () 
	{
		thereIsOne = false;
		pathfinder = GameObject.Find("Manager").GetComponent<Pathfinder>();
		manager = GameObject.Find("Manager").GetComponent<GridCreator>();
	}
	void Update () 
	{
		if(GameObject.Find("Camera").GetComponent<GridPosition>().setting)
		{
			thereIsOne = false;
		}
		WhileDontStart();
	}
	
	void WhileDontStart()
	{
		for(int i = 0; i < manager.statusID.Length; i++) 
		{
			if(manager.statusID[i] == 1)
			{
				thereIsOne = true;
				initial = i;
			}
		}
		try
		{
			if (!pathfinder.start && thereIsOne)
			{
				transform.position = new Vector3(manager.cellAux[initial].transform.position.x, manager.cellAux[initial].transform.position.y, transform.position.z);
			}
			else if (!pathfinder.start && !thereIsOne)
			{
				gameObject.SetActive(false);
			}
		}
		catch
		{
			gameObject.SetActive(false);
		}
		transform.Rotate(new Vector3(1f, 1f, 0f) * Time.deltaTime * 60f);
		initial = -1;
		thereIsOne = false;
	}
}
