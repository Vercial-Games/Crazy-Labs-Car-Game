using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StationController : MonoBehaviour
{
    public bool isTakeStation;

    [SerializeField] Transform dropPosition = null;
    [SerializeField] Transform[] randomPlaces = null;

    Gate gate;
    GameObject car;
    CarMover mover;
    CarController carController;
    CarType carType;
    PassangerStation passangerStation;

    private void Start()
    {
        gate = FindObjectOfType<Gate>();
        passangerStation = FindObjectOfType<PassangerStation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarMover>() && car == null)
        {
            GetCarInfo(other);

            mover.onStation = true;

            if (isTakeStation)
                StartCoroutine(TakePassangers());
            else if (carController.PassangersCount > 0)
                StartCoroutine(DropPassangers());
            else
            {
                RemoveCarInfo();
            }
        }
    }

    private void GetCarInfo(Collider other)
    {
        car = other.gameObject;
        mover = other.GetComponent<CarMover>();
        carController = other.GetComponent<CarController>();
        carType = other.GetComponent<CarType>();
    }

    private void RemoveCarInfo()
    {
        mover.onStation = false;
        car = null;
        mover = null;
        carController = null;
        carType = null;
    }

    IEnumerator DropPassangers()
    {
        for (int i = 0; i < carController.PassangersCount; i++)
        {
            GameObject passanger = PassangerPool.instance.GetPooledObject();
            passanger.SetActive(true);
            passanger.transform.position = car.transform.position;
            passanger.transform.DOJump(dropPosition.position, 0.5f, 1, 1);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(PassangerRandomMove(passanger));
            MoneyManager.instance.IncreaseCurrentMoney(MoneyManager.instance.GetIncomeValue());
        }

        yield return new WaitForSeconds(1);
        RemoveCarInfo();
    }
    IEnumerator TakePassangers()
    {
        carController.PassangersCount = 0;

        gate.OpenGate();

        yield return new WaitForSeconds(gate.speed);

        for (int i = 0; i < carType.PassangerCapacity;)
        {
            carController.PassangersCount++;
            passangerStation.passangers[i].transform.DOJump(car.transform.position, 0.5f, 1, 1);

            yield return new WaitForSeconds(0.2f);

            passangerStation.fullArea[i] = false;
            StartCoroutine(ClosePassangerMesh(passangerStation.passangers[i]));
            i++;
        }
        yield return new WaitForSeconds(1f);

        gate.CloseGate();
        passangerStation.PassangersMoveForward(carType.PassangerCapacity);

        yield return new WaitForSeconds(1);

        RemoveCarInfo();
    }
    IEnumerator ClosePassangerMesh(GameObject passanger)
    {
        Vector3 passangerScale = new Vector3(0.173201054f, 0.173201054f, 0.173201054f);

        passanger.transform.DOScale(0, 1f);
        yield return new WaitForSeconds(1f);
        passanger.SetActive(false);
        passanger.transform.DOScale(passangerScale, 0.1f);

    }
    IEnumerator PassangerRandomMove(GameObject passanger)
    {
        yield return new WaitForSeconds(0.5f);

        int random = Random.Range(0, randomPlaces.Length);
        passanger.transform.DOMove(randomPlaces[random].position,2);

        yield return new WaitForSeconds(4);
        ClosePassangerMesh(passanger);


    }
}
