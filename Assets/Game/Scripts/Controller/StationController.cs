using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StationController : MonoBehaviour
{
    #region VARIABLES
    public bool isTakeStation;

    [SerializeField] Transform dropPosition = null;
    [SerializeField] Transform[] randomPlaces = null;

    Gate gate;
    GameObject car;
    CarMover mover;
    CarController carController;
    CarType carType;
    PassangerStation passangerStation;
    #endregion

    #region METHODS
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
            {
                carController.PassangersCount = 0;
                StartCoroutine(TakePassangers());
                carController.PlaySound(1);
            }
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
    #endregion

    #region ENUMERATORS
    IEnumerator DropPassangers()
    {
        Vector3 passangerScale = new Vector3(0.173201054f, 0.173201054f, 0.173201054f);

        for (int i = 0; i < carController.PassangersCount; i++)
        {
            GameObject passanger = PassangerPool.instance.GetPooledObject();
            if (passanger != null)
            {
                passanger.transform.localScale = passangerScale;
                passanger.transform.position = car.transform.position;
                passanger.transform.DOJump(dropPosition.position, 0.5f, 1, 1f);
                passanger.SetActive(true);
                passanger.GetComponent<PassangerChar>().JumpSound();
            }
            HapticManager.instance.SoftHaptic();

            yield return new WaitForSeconds(0.4f);

            StartCoroutine(PassangerRandomMove(passanger));

        }

        yield return new WaitForSeconds(1);

        carController.PassangersCount = 0;

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
            passangerStation.passangers[i].transform.DOJump(car.transform.position, 0.5f, 1, 1.5f);
            passangerStation.passangers[i].GetComponent<PassangerChar>().Jump();
            passangerStation.passangers[i].GetComponent<PassangerChar>().JumpSound();
            HapticManager.instance.SoftHaptic();

            yield return new WaitForSeconds(0.4f);

            passangerStation.fullArea[i] = false;
            StartCoroutine(ClosePassangerMesh(passangerStation.passangers[i]));
            i++;
        }
        yield return new WaitForSeconds(1.6f);

        gate.CloseGate();
        passangerStation.PassangersMoveForward(carType.PassangerCapacity);

        yield return new WaitForSeconds(1);

        RemoveCarInfo();
    }
    IEnumerator ClosePassangerMesh(GameObject passanger)
    {
        Vector3 passangerScale = new Vector3(0.173201054f, 0.173201054f, 0.173201054f);
        passanger.transform.DOScale(0, 2f);
        yield return new WaitForSeconds(1f);
        passanger.transform.DOScale(passangerScale, 0.1f);
        passanger.SetActive(false);
    }
    IEnumerator PassangerRandomMove(GameObject passanger)
    {
        yield return new WaitForSeconds(0.6f);
        Vector3 passangerScale = new Vector3(0.173201054f, 0.173201054f, 0.173201054f);
        passanger.transform.localScale = passangerScale;
        MoneyManager.instance.IncreaseCurrentMoney(MoneyManager.instance.GetIncomeValue());
        passanger.GetComponent<PassangerChar>().canvasAnim.Play();

        UIManager.instance.MoneyColorAnim();

        int random = Random.Range(0, randomPlaces.Length);

        passanger.transform.DOMove(randomPlaces[random].position, 4);
        passanger.transform.DOLookAt(randomPlaces[random].position, 1);

        yield return new WaitForSeconds(4);
        StartCoroutine(ClosePassangerMesh(passanger));


    }
    #endregion
}
