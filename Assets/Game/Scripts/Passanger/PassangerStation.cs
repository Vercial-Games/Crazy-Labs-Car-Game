using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PassangerStation : MonoBehaviour
{
    #region VARIABLES
    public List<Transform> passangerOrder;
    public List<GameObject> passangers;
    public bool[] fullArea;
    #endregion

    #region METHODS
    private void Start()
    {
        SortPassanger();
    }
    void SortPassanger()
    {
        for (int i = 0; i < passangerOrder.Count; i++)
        {
            if (!fullArea[i])
            {
                GameObject passanger = PassangerPool.instance.GetPooledObject();
                if(passanger != null)
                {
                    passanger.transform.localScale = new Vector3(0.173201054f, 0.173201054f, 0.173201054f);
                    passanger.transform.eulerAngles = Vector3.zero;
                    passangers.Add(passanger);
                    passanger.SetActive(true);
                    passanger.transform.parent = null;
                    fullArea[i] = true;
                    passanger.transform.position = passangerOrder[i].position;
                }
            }
        }
    }
    public void PassangersMoveForward(int range)
    {
        passangers.RemoveRange(0, range);

        for (int i = 0; i < fullArea.Length; i++)
        {
            if (!fullArea[i] && i < passangers.Count)
            {
                passangers[i].transform.DOMove(passangerOrder[i].position, 1);
                passangers[i].GetComponent<PassangerChar>().Jump();
                passangers[i].transform.localScale = new Vector3(0.173201054f, 0.173201054f, 0.173201054f);
                fullArea[i] = true;
                fullArea[i + 1] = false;
            }
            else
            {
                fullArea[i] = false;
            }
        }
        SortPassanger();
    }
    #endregion
}
