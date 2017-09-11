using UnityEngine;
using System.Collections;

public class CellBehavior : MonoBehaviour 
{
	public Material[] optionsM;
	public int number, line, column, F, I;
	public bool defineF;
	private bool thereIsOne,thereIsTwo;
	private GridCreator manager;
	private Pathfinder pathfinder;
	private int counter;
	void Start () 
	{
		F = 0;
		I = 0;
		optionsM = new Material[5];
		manager = GameObject.Find("Manager").GetComponent<GridCreator>();
		pathfinder = GameObject.Find("Manager").GetComponent<Pathfinder>();
		for(int i = 0;i < 5; i++)
		{
			optionsM[i] = Resources.Load("Materials/Material"+i,typeof(Material)) as Material;
		}
	}
	void OnMouseDown()
	{
        if (!pathfinder.start)
        {
            for (int i = 0; i < manager.statusID.Length; i++)
            {
                if (manager.statusID[i] == 1)
                {
                    thereIsOne = true;
                }
                if (manager.statusID[i] == 2)
                {
                    thereIsTwo = true;
                }
            }
            if (manager.statusID[number] < 4)
            {
                manager.statusID[number] += 1;
            }
            else
            {
                manager.statusID[number] = 0;
            }
            if (manager.statusID[number] == 1 && thereIsOne)
            {
                manager.statusID[number] = 2;
            }
            if (manager.statusID[number] == 2 && thereIsTwo)
            {
                manager.statusID[number] = 3;
            }
            while (counter < manager.statusID.Length)
            {
                if (manager.statusID[counter] == 1)
                {
                    thereIsOne = true;
                }
                if (manager.statusID[counter] == 2)
                {
                    thereIsTwo = true;
                }
                counter++;
            }
            if (thereIsOne && thereIsTwo)
            {
                for (int i = 0; i < manager.sizeGrid; i++)
                {
                    manager.cellAux[i].GetComponent<CellBehavior>().defineF = true;
                }
            }
            thereIsOne = false;
            thereIsTwo = false;
        }
	}
	void Update () 
	{
		if(!pathfinder.start)
		{
			ConfigureCell();
		}
		if(defineF)
		{
			F = ((Mathf.Abs(pathfinder.target_i - line) * 10) + (Mathf.Abs(pathfinder.target_j - column) * 10));
			I = ((Mathf.Abs(pathfinder.pos_i - line) * 10) + (Mathf.Abs(pathfinder.pos_j - column) * 10));
		}
	}
	void ConfigureCell()
	{
		if (manager.statusID[number] == 1 && !pathfinder.start)
		{
			pathfinder.pos_i = line;
			pathfinder.pos_j = column;
			pathfinder.t_i = line;
			pathfinder.t_j = column;
		}
		else if (manager.statusID[number] == 2 && !pathfinder.start)
		{
			pathfinder.target_i = line;
			pathfinder.target_j = column;
			pathfinder.p_i = line;
			pathfinder.p_j = column;
		}
        if (manager.statusID[number] == 3 && gameObject.GetComponent<GridPreferences>().CanWalk)
        {
            gameObject.GetComponent<GridPreferences>().CanWalk = false;
        }
        else if (manager.statusID[number] != 3 && !gameObject.GetComponent<GridPreferences>().CanWalk)
        {
            gameObject.GetComponent<GridPreferences>().CanWalk = true;
        } 
		gameObject.GetComponent<Renderer>().material = optionsM[manager.statusID[number]];
	}
}