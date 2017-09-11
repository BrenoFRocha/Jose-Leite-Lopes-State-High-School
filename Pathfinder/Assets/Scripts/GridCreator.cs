using UnityEngine;
using System.Collections;

public class GridCreator : MonoBehaviour 
{
	
	private GameObject cell;
	public GameObject[] cellAux;
	private Transform managerT;
	private Pathfinder finder;
	private int[,] vectors;
	public int sizeX, sizeY, order, sizeGrid;
	public int[] statusID;
	public bool makeAgain;
	public static bool verify;
	public GameObject ball;
	public GridPreferences[,] cells;
	
	void Update()
	{
		if(makeAgain)
		{
			finder = gameObject.GetComponent<Pathfinder>();
			verify = false;
			if (sizeX % 2 != 0 && sizeY % 2 != 0)
			{
				transform.position = new Vector3(-sizeX / 2, -sizeY / 2, 0f);
			}
			else if (sizeX % 2 != 0 && sizeY % 2 == 0)
			{
				transform.position = new Vector3(-sizeX / 2, -sizeY / 2 + 0.5f, 0f);
			}
			else if (sizeX % 2 == 0 && sizeY % 2 == 0)
			{
				transform.position = new Vector3(-sizeX / 2 + 0.5f, -sizeY / 2 + 0.5f, 0f);
			}
			else
			{
				transform.position = new Vector3(-sizeX / 2 + 0.5f, -sizeY / 2, 0f);
			}
			
			order = 0;
			sizeGrid = sizeX * sizeY;
			cellAux = new GameObject[sizeGrid];
			cells = new GridPreferences[sizeX, sizeY];
			managerT = gameObject.transform;
			cell = Resources.Load("Prefabs/Cell", typeof(GameObject)) as GameObject;
			vectors = new int[sizeX, sizeY];
			Creator(vectors);
			statusID = new int[cellAux.Length];
			for (int i = 0; i < cellAux.Length; i++)
			{
				statusID[i] = 0;
				cellAux[i].GetComponent<GridPreferences>().CanWalk = true;
			}
			makeAgain = false;
		}
		FindAndActivateBall();
	}
	void Creator(int[,] SizeXZ)
	{
		int x;
		int y;
		for (x = 0; x < SizeXZ.GetLength(0); x++)
		{
			for (y = 0; y < SizeXZ.GetLength(1); y++)
			{
				cellAux[order] = Instantiate(cell, new Vector3(transform.position.x + x, transform.position.y + y + 1, 0f),Quaternion.identity) as GameObject;
				cellAux[order].transform.SetParent(managerT);
				cellAux[order].name = "X: "+ x +" Y: "+y; 
				cellAux[order].GetComponent<CellBehavior>().number = order;
				cellAux[order].GetComponent<CellBehavior>().line = x;
				cellAux[order].GetComponent<CellBehavior>().column = y;
				GridPreferences grid = cellAux[order].AddComponent<GridPreferences>();
				grid.Init(x, y, true, false);
				cells[x, y] = grid;
				order += 1;
			}
		}
	}
	void FindAndActivateBall()
	{
		for (int i = 0; i < sizeX * sizeY; i++)
		{
			if (!finder.start)
			{
				if (statusID[i] == 1)
				{
					ball.transform.position = new Vector3(cellAux[i].transform.position.x, cellAux[i].transform.position.y, ball.transform.position.z);
					ball.SetActive(true);
				}
			}
		}
	}
}