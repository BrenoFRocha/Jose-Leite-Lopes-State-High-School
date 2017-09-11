using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pathfinder : MonoBehaviour 
{
	private GridCreator gridCreator;
	private Text currentText;
	private float waiter;
	public int pos_i, pos_j, p_i, p_j, target_i, target_j, t_i, t_j, counter;
	private int aH,bH,cH,dH,eH,fH,gH,hH, index, auxIndex, totalCells;
	private bool canAnimate, changed, thereIsOne, thereIsTwo;
	public bool start;
	public Transform target;
	public Transform[] way1, way2, rightWay;
	public GameObject warning,up,down,right,left,upright,upleft,downright,downleft;
    public GameObject[] allFeet, orderFeet, feetToDestroy;

	void Start()
	{
		gridCreator = GameObject.Find("Manager").GetComponent<GridCreator>();
		canAnimate = false;
		currentText = GameObject.Find("TextPath").GetComponent<Text>();
	}
	
	void Update()
	{
		if(canAnimate)
		{
			AnimateBall();
		}
	}
	
	public void OnClickStart()
	{

		while(counter < gridCreator.statusID.Length)
		{
			if(gridCreator.statusID[counter] == 1)
			{
				thereIsOne = true;
			}
			if(gridCreator.statusID[counter] == 2)
			{
				thereIsTwo = true;
			}
			counter++;
		}
		
		if(thereIsOne && thereIsTwo)
		{
			if(warning.activeSelf)
			{
				warning.SetActive(false);
			}
			start = !start;
			if (start)
			{
				counter = 0;
				index = 0;
				for(int i = 0; i < gridCreator.sizeGrid; i++)
				{
					gridCreator.cellAux[i].GetComponent<CellBehavior>().defineF = false;
				}
				way1 = new Transform[gridCreator.sizeGrid];
				way2 = new Transform[gridCreator.sizeGrid];
                orderFeet = new GameObject[gridCreator.sizeGrid];
				SearchGo();
				thereIsOne = false;
				thereIsTwo = false;
				currentText.text = "Stop";
			}
			else
			{
				rightWay = new Transform[gridCreator.sizeGrid];
                foreach (GameObject n in feetToDestroy)
                {
                    Destroy(n);
                }
				target = null;				
                canAnimate = false;
				changed = false;
				currentText.text = "Start";
				for(int i = 0; i < gridCreator.statusID.Length; i++)
				{
					if(gridCreator.cellAux[i].GetComponent<CellBehavior>().number != 3 && !gridCreator.cellAux[i].GetComponent<GridPreferences>().CanWalk)
					{
						gridCreator.cellAux[i].GetComponent<GridPreferences>().CanWalk = true;
					}
					gridCreator.cellAux[i].GetComponent<GridPreferences>().PassedHere = false;
				}
			}
		}
		else
		{
			warning.SetActive(true);
			counter = 0;
			thereIsOne = false;
			thereIsTwo = false;
		}
	}
	
	void SearchGo()
	{
        try
        {
            if (start)
            {
                gridCreator.cells[pos_i, pos_j].CanWalk = false;
                gridCreator.cells[pos_i, pos_j].PassedHere = true;

                int ini_i = pos_i;
                int ini_j = pos_j;

                if (pos_i > 0)
                {
                    if (gridCreator.cells[pos_i - 1, pos_j] != null && gridCreator.cells[pos_i - 1, pos_j].CanWalk)
                    {
                        aH = gridCreator.cells[pos_i - 1, pos_j].GetComponent<CellBehavior>().F + 10;
                    }
                    else
                    {
                        aH = 1000000;
                    }
                }
                else
                {
                    aH = 1000000;
                }
                if (pos_j > 0)
                {
                    if (gridCreator.cells[pos_i, pos_j - 1] != null && gridCreator.cells[pos_i, pos_j - 1].CanWalk)
                    {
                        bH = gridCreator.cells[pos_i, pos_j - 1].GetComponent<CellBehavior>().F + 10;
                    }
                    else
                    {
                        bH = 1000000;
                    }
                }
                else
                {
                    bH = 1000000;
                }
                if (pos_i + 1 <= gridCreator.sizeX - 1)
                {
                    if (gridCreator.cells[pos_i + 1, pos_j] != null && gridCreator.cells[pos_i + 1, pos_j].CanWalk)
                    {
                        cH = gridCreator.cells[pos_i + 1, pos_j].GetComponent<CellBehavior>().F + 10;
                    }
                    else
                    {
                        cH = 1000000;
                    }
                }
                else
                {
                    cH = 1000000;
                }
                if (pos_j + 1 <= gridCreator.sizeY - 1)
                {
                    if (gridCreator.cells[pos_i, pos_j + 1] != null && gridCreator.cells[pos_i, pos_j + 1].CanWalk)
                    {
                        dH = gridCreator.cells[pos_i, pos_j + 1].GetComponent<CellBehavior>().F + 10;
                    }
                    else
                    {
                        dH = 1000000;
                    }
                }
                else
                {
                    dH = 1000000;
                }
                try
                {
                    if (pos_i > 0 && pos_j > 0)
                    {
                        if (gridCreator.cells[pos_i - 1, pos_j - 1] != null && gridCreator.cells[pos_i - 1, pos_j - 1].CanWalk)
                        {
                            if (!gridCreator.cells[pos_i - 1, pos_j].CanWalk || !gridCreator.cells[pos_i, pos_j - 1].CanWalk)
                            {
                                eH = 1000000;
                            }
                            else
                            {
                                eH = gridCreator.cells[pos_i - 1, pos_j - 1].GetComponent<CellBehavior>().F + 14;
                            }
                        }
                        else
                        {
                            eH = 1000000;
                        }
                    }
                    else
                    {
                        eH = 1000000;
                    }
                }
                catch
                {
                    eH = 1000000;
                }
                try
                {
                    if (pos_i + 1 <= gridCreator.sizeX - 1 && pos_j > 0)
                    {
                        if (gridCreator.cells[pos_i + 1, pos_j - 1] != null && gridCreator.cells[pos_i + 1, pos_j - 1].CanWalk)
                        {
                            if (!gridCreator.cells[pos_i, pos_j - 1].CanWalk || !gridCreator.cells[pos_i + 1, pos_j].CanWalk)
                            {
                                fH = 1000000;
                            }
                            else
                            {
                                fH = gridCreator.cells[pos_i + 1, pos_j - 1].GetComponent<CellBehavior>().F + 14;
                            }
                        }
                        else
                        {
                            fH = 1000000;
                        }
                    }
                    else
                    {
                        fH = 1000000;
                    }
                }
                catch
                {
                    fH = 1000000;
                }
                try
                {
                    if (pos_i > 0 && pos_j + 1 <= gridCreator.sizeY - 1)
                    {
                        if (gridCreator.cells[pos_i - 1, pos_j + 1] != null && gridCreator.cells[pos_i - 1, pos_j + 1].CanWalk)
                        {
                            if (!gridCreator.cells[pos_i, pos_j + 1].CanWalk || !gridCreator.cells[pos_i - 1, pos_j].CanWalk)
                            {
                                gH = 1000000;
                            }
                            else
                            {
                                gH = gridCreator.cells[pos_i - 1, pos_j + 1].GetComponent<CellBehavior>().F + 14;
                            }
                        }
                        else
                        {
                            gH = 1000000;
                        }
                    }
                    else
                    {
                        gH = 1000000;
                    }
                }
                catch
                {
                    gH = 1000000;
                }
                try
                {
                    if (pos_i + 1 <= gridCreator.sizeX - 1 && pos_j + 1 <= gridCreator.sizeY - 1)
                    {
                        if (gridCreator.cells[pos_i + 1, pos_j + 1] != null && gridCreator.cells[pos_i + 1, pos_j + 1].CanWalk)
                        {
                            if (!gridCreator.cells[pos_i, pos_j + 1].CanWalk || !gridCreator.cells[pos_i + 1, pos_j].CanWalk)
                            {
                                hH = 1000000;
                            }
                            else
                            {
                                hH = gridCreator.cells[pos_i + 1, pos_j + 1].GetComponent<CellBehavior>().F + 14;
                            }
                        }
                        else
                        {
                            hH = 1000000;
                        }
                    }
                    else
                    {
                        hH = 1000000;
                    }
                }
                catch
                {
                    hH = 1000000;
                }

                if (aH + bH + cH + dH + eH + fH + gH + hH == 8000000)
                {
                    try
                    {
                        if (way1[auxIndex - 1] != null)
                        {
                            changed = true;
                            auxIndex -= 1;
                            pos_i = way1[auxIndex].GetComponent<CellBehavior>().line;
                            pos_j = way1[auxIndex].GetComponent<CellBehavior>().column;
                            aH = 0;
                            bH = 0;
                            cH = 0;
                            dH = 0;
                            eH = 0;
                            fH = 0;
                            gH = 0;
                            hH = 0;
                        }
                    }
                    catch { }
                }
                else
                {
                    if (eH <= aH && eH <= bH && eH <= cH && eH <= dH &&
                        eH <= fH && eH <= gH && eH <= hH && eH <= 1000 && pos_i > 0 && pos_j > 0)
                    {
                        pos_i -= 1;
                        pos_j -= 1;
                    }
                    else if (fH <= aH && fH <= bH && fH <= cH && fH <= dH &&
                        fH <= eH && fH <= gH && fH <= hH && fH <= 1000 && pos_i + 1 <= gridCreator.sizeX - 1 && pos_j > 0)
                    {
                        pos_i += 1;
                        pos_j -= 1;
                    }
                    else if (gH <= aH && gH <= bH && gH <= cH && gH <= dH &&
                        gH <= eH && gH <= fH && gH <= hH && gH <= 1000 && pos_i > 0 && pos_j + 1 <= gridCreator.sizeY - 1)
                    {
                        pos_i -= 1;
                        pos_j += 1;
                    }
                    else if (hH <= aH && hH <= bH && hH <= cH && hH <= dH &&
                        hH <= eH && hH <= fH && hH <= gH && hH <= 1000 && pos_i + 1 <= gridCreator.sizeX - 1 && pos_j + 1 <= gridCreator.sizeY - 1)
                    {
                        pos_i += 1;
                        pos_j += 1;
                    }
                    else if (aH <= bH && aH <= cH && aH <= dH && aH <= eH &&
                        aH <= fH && aH <= gH && aH <= hH && aH <= 1000 && pos_i > 0)
                    {
                        pos_i -= 1;
                    }
                    else if (bH <= aH && bH <= cH && bH <= dH && bH <= eH &&
                        bH <= fH && bH <= gH && bH <= hH && bH <= 1000 && pos_j > 0)
                    {
                        pos_j -= 1;
                    }
                    else if (cH <= aH && cH <= bH && cH <= dH && cH <= eH &&
                        cH <= fH && cH <= gH && cH <= hH && cH <= 1000 && pos_i + 1 <= gridCreator.sizeX - 1)
                    {
                        pos_i += 1;
                    }
                    else if (dH <= aH && dH <= bH && dH <= cH && dH <= eH &&
                        dH <= fH && dH <= gH && dH <= hH && dH <= 1000 && pos_j + 1 <= gridCreator.sizeY - 1)
                    {
                        pos_j += 1;
                    }
                    if (changed)
                    {
                        changed = false;
                        index = auxIndex;
                        way1[index] = gridCreator.cells[pos_i, pos_j].transform;
                    }
                    else
                    {
                        if (ini_i != pos_i || ini_j != pos_j)
                        {
                            way1[index] = gridCreator.cells[pos_i, pos_j].transform;
                        }
                    }
                    index += 1;
                    auxIndex = index;
                }

                if (pos_i != target_i || pos_j != target_j)
                {
                    SearchGo();
                }
                else
                {
                    index = 0;
                    auxIndex = 0;
                    for (int i = 0; i < way1.Length; i++)
                    {
                        try
                        {
                            way1[i].GetComponent<GridPreferences>().CanWalk = true;
                        }
                        catch { }
                    }
                    SearchBack();
                }
            }
        }
        catch { Debug.Log("Sem Saida"); }
	}
	
	void SearchBack()
	{
		if(start)
		{
			if(way2[0] == null)
			{
				way2[index] = gridCreator.cells[p_i, p_j].transform;
				index += 1;
			}
			gridCreator.cells[p_i, p_j].CanWalk = false;
			gridCreator.cells[p_i, p_j].PassedHere = false;
			if (p_i > 0)
			{
				if (gridCreator.cells[p_i - 1, p_j] != null && gridCreator.cells[p_i - 1, p_j].PassedHere)
				{
					aH = gridCreator.cells[p_i - 1, p_j].GetComponent<CellBehavior>().I + 10;
				}
				else
				{
					aH = 1000000;
				}
			}
			else
			{
				aH = 1000000;
			}
			if (p_j > 0)
			{
				if (gridCreator.cells[p_i, p_j - 1] != null && gridCreator.cells[p_i, p_j - 1].PassedHere)
				{
					bH = gridCreator.cells[p_i, p_j - 1].GetComponent<CellBehavior>().I + 10;
				}
				else
				{
					bH = 1000000;
				}
			}
			else
			{
				bH = 1000000;
			}
			if (p_i + 1 <= gridCreator.sizeX - 1)
			{
				if (gridCreator.cells[p_i + 1, p_j] != null && gridCreator.cells[p_i + 1, p_j].PassedHere)
				{
					cH = gridCreator.cells[p_i + 1, p_j].GetComponent<CellBehavior>().I + 10;
				}
				else
				{
					cH = 1000000;
				}
			}
			else
			{
				cH = 1000000;
			}
			if (p_j + 1 <= gridCreator.sizeY - 1)
			{
				if (gridCreator.cells[p_i, p_j + 1] != null && gridCreator.cells[p_i, p_j + 1].PassedHere)
				{
					dH = gridCreator.cells[p_i, p_j + 1].GetComponent<CellBehavior>().I + 10;
				}
				else
				{
					dH = 1000000;
				}
			}
			else
			{
				dH = 1000000;
			}
			if (p_i > 0 && p_j > 0)
			{
				if (gridCreator.cells[p_i - 1, p_j - 1] != null && gridCreator.cells[p_i - 1, p_j - 1].PassedHere)
				{
                    if (!gridCreator.cells[p_i - 1, p_j].CanWalk || !gridCreator.cells[p_i, p_j - 1].CanWalk)
                    {
                        eH = 1000000;
                    }
                    else
                    {
					    eH = gridCreator.cells[p_i - 1, p_j - 1].GetComponent<CellBehavior>().I + 14;
				    }
                }
				else
				{
					eH = 1000000;
				}
			}
			else
			{
				eH = 1000000;
			}
			if (p_i + 1 <= gridCreator.sizeX - 1 && p_j > 0)
			{
				if (gridCreator.cells[p_i + 1, p_j - 1] != null && gridCreator.cells[p_i + 1, p_j - 1].PassedHere)
				{
                    if (!gridCreator.cells[p_i + 1, p_j].CanWalk || !gridCreator.cells[p_i, p_j - 1].CanWalk)
                    {
                        fH = 1000000;
                    }
                    else
                    {
					    fH = gridCreator.cells[p_i + 1, p_j - 1].GetComponent<CellBehavior>().I + 14;
				    }
                }
				else
				{
					fH = 1000000;
				}
			}
			else
			{
				fH = 1000000;
			}
			if (p_i > 0 && p_j + 1 <= gridCreator.sizeY - 1)
			{
				if (gridCreator.cells[p_i - 1, p_j + 1] != null && gridCreator.cells[p_i - 1, p_j + 1].PassedHere)
				{
                    if (!gridCreator.cells[p_i - 1, p_j].CanWalk || !gridCreator.cells[p_i, p_j + 1].CanWalk)
                    {
                        gH = 1000000;
                    }
                    else
                    {
					    gH = gridCreator.cells[p_i - 1, p_j + 1].GetComponent<CellBehavior>().I + 14;
				    }
                }
				else
				{
					gH = 1000000;
				}
			}
			else
			{
				gH = 1000000;
			}
			if (p_i + 1 <= gridCreator.sizeX - 1 && p_j + 1 <= gridCreator.sizeY - 1)
			{
				if (gridCreator.cells[p_i + 1, p_j + 1] != null && gridCreator.cells[p_i + 1, p_j + 1].PassedHere)
				{
                    if (!gridCreator.cells[p_i + 1, p_j].CanWalk || !gridCreator.cells[p_i, p_j + 1].CanWalk)
                    {
                        hH = 1000000;
				    }
                    else
                    {
                        hH = gridCreator.cells[p_i + 1, p_j + 1].GetComponent<CellBehavior>().I + 14;
                    }
                }
				else
				{
					hH = 1000000;
				}
			}
			else
			{
				hH = 1000000;
			}
			if (eH <= aH && eH <= bH && eH <= cH && eH <= dH &&
			    eH <= fH && eH <= gH && eH <= hH && eH <= 1000 && p_i > 0 && p_j > 0)
			{
				p_i -= 1;
				p_j -= 1;
                orderFeet[index-1] = upright;
			}
			else if (fH <= aH && fH <= bH && fH <= cH && fH <= dH &&
			         fH <= eH && fH <= gH && fH <= hH && fH <= 1000 && p_i + 1 <= gridCreator.sizeX - 1 && p_j > 0)
			{
				p_i += 1;
				p_j -= 1;
                orderFeet[index - 1] = upleft;
			}
			else if (gH <= aH && gH <= bH && gH <= cH && gH <= dH &&
			         gH <= eH && gH <= fH && gH <= hH && gH <= 1000 && p_i > 0 && p_j + 1 <= gridCreator.sizeY - 1)
			{
				p_i -= 1;
				p_j += 1;
                orderFeet[index - 1] = downright;
			}
			else if (hH <= aH && hH <= bH && hH <= cH && hH <= dH &&
			         hH <= eH && hH <= fH && hH <= gH && hH <= 1000 && p_i + 1 <= gridCreator.sizeX - 1 && p_j + 1 <= gridCreator.sizeY - 1)
			{
				p_i += 1;
				p_j += 1;
                orderFeet[index - 1] = downleft;
			}
			else if (aH <= bH && aH <= cH && aH <= dH && aH <= eH &&
			         aH <= fH && aH <= gH && aH <= hH && aH <= 1000 && p_i > 0)
			{
				p_i -= 1;
                orderFeet[index - 1] = right;
			}
			else if (bH <= aH && bH <= cH && bH <= dH && bH <= eH &&
			         bH <= fH && bH <= gH && bH <= hH && bH <= 1000 && p_j > 0)
			{
				p_j -= 1;
                orderFeet[index - 1] = up;
			}
			else if (cH <= aH && cH <= bH && cH <= dH && cH <= eH &&
			         cH <= fH && cH <= gH && cH <= hH && cH <= 1000 && p_i + 1 <= gridCreator.sizeX - 1)
			{
				p_i += 1;
                orderFeet[index - 1] = left;
			}
			else if (dH <= aH && dH <= bH && dH <= cH && dH <= eH &&
			         dH <= fH && dH <= gH && dH <= hH && dH <= 1000 && p_j + 1 <= gridCreator.sizeY - 1)
			{
				p_j += 1;
                orderFeet[index - 1] = down;
			}
			way2[index] = gridCreator.cells[p_i, p_j].transform;
			index += 1;
			
			if(p_i != t_i || p_j != t_j)
			{
				SearchBack();
			}
			else
			{
				rightWay = new Transform[index];
				for(int i = 0; i < way2.Length; i++)
				{
					if(way2[i] != null)
					{
						rightWay[i] = way2[i];
					}
				}
                allFeet = new GameObject[rightWay.Length];
                feetToDestroy = new GameObject[rightWay.Length];
                for (int i = 0; i < orderFeet.Length; i++)
                {
                    if (orderFeet[i] != null)
                    {
                        allFeet[i] = orderFeet[i];
                    }
                }
                System.Array.Reverse(allFeet);
				System.Array.Reverse(rightWay);
				canAnimate = true;
				index = 0;
			}
		}
	}
	
	void AnimateBall()
	{
		if (target != null)
		{
			gridCreator.ball.transform.position = Vector3.Lerp(gridCreator.ball.transform.position, new Vector3(target.position.x, target.position.y, gridCreator.ball.transform.position.z), .5f);
		}
		if (waiter < 0.3f)
		{
			waiter += Time.deltaTime;
		}
		else if (index < rightWay.Length) 
		{
			if (rightWay[index] == null)
			{
				index += 1;
			}
			else
			{
				target = rightWay[index];
                if (index < rightWay.Length - 1)
                {
                    feetToDestroy[index] = Instantiate(allFeet[index + 1], new Vector3(target.transform.position.x, target.position.y, -0.55f), Quaternion.identity) as GameObject;
                }
				index += 1;
				waiter = 0;
			}
		}
	}
}