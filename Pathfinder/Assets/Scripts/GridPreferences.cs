using UnityEngine;
using System.Collections;

public class GridPreferences : MonoBehaviour
{
    public int id_i = -1;
    public int id_j = -1;
	public string foot;
    public bool CanWalk, PassedHere;

    void Start()
    {
        CanWalk = true;
        PassedHere = false;
    }

	public void Init(int id_i, int id_j, bool canWalk, bool passedHere)
    {
        this.id_i = id_i;
        this.id_j = id_j;
        CanWalk = canWalk;
		PassedHere = passedHere;
    }
}