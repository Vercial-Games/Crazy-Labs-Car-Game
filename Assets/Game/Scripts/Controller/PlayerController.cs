using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using DG.Tweening;
using UnityEngine.Audio;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] GameObject carPrefab;
    [SerializeField] List<GameObject> currentCars;
    [SerializeField] List<CarController> mergeCars;

    public int TaxiCount;
    public int UberCount;
    public int BusCount;

    public int carPrice = 20;
    public int mergePrice = 20;
    public int incomePrice = 20;

    public bool isMerging;

    public float BuffCooldown;

    float CdTimer;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        AddCar();
    }
    private void Update()
    {
        UIManager.instance.addCarButton.interactable = AddCarControl();
        UIManager.instance.mergeButton.interactable = MergeControl();
        UIManager.instance.incomeButton.interactable = IncomeControl();

        if (!GameManager.instance.gamePaused)
        {
            SpeedBuff();
            CooldownTimer();
        }
            
    }
    private void SpeedBuff()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HapticManager.instance.SoftHaptic();
            Time.timeScale = 4;
            SetCoolDown(0);
        }
    }
    public float SetCoolDown(float value)
    {
        return CdTimer = value;
    }
    private void CooldownTimer()
    {
        CdTimer += Time.deltaTime;
        if (CdTimer >= BuffCooldown)
        {
            SmoothResetSpeed(3, 1);
        }
    }
    public float SmoothResetSpeed(float value, float equal)
    {
        if (Time.timeScale > equal)
            return Time.timeScale -= value * Time.deltaTime;

        return Time.timeScale = equal;
    }

    #region AddCar

    private bool AddCarControl()
    {
        if (MoneyManager.instance.GetCurrentMoney() < carPrice) return false;

        if (currentCars.Count > 7) return false;

        return true;
    }
    public void AddCar()
    {
        carPrice += 10;
        GameObject car = Instantiate(carPrefab);
        car.GetComponent<PathFollower>().pathCreator = FindObjectOfType<PathCreator>();
        currentCars.Add(car);
    }



    #endregion

    #region Merge
    private bool MergeControl()
    {
        if (MoneyManager.instance.GetCurrentMoney() < mergePrice) return false;

        if (isMerging) return false;

        mergeCars.RemoveRange(0, mergeCars.Count); //Clear List

        if (TaxiCount >= 3)
        {
            for (int i = 0; i < currentCars.Count; i++)
            {
                CarType type = currentCars[i].GetComponent<CarType>();

                if (type.carType.ToString() == "Taxi")
                    mergeCars.Add(currentCars[i].GetComponent<CarController>());
            }
            return true;
        }
        else if (UberCount >= 3)
        {
            for (int i = 0; i < currentCars.Count; i++)
            {
                CarType type = currentCars[i].GetComponent<CarType>();

                if (type.carType.ToString() == "Uber")
                    mergeCars.Add(currentCars[i].GetComponent<CarController>());
            }
            return true;
        }
        else if (BusCount >= 3)
        {
            for (int i = 0; i < currentCars.Count; i++)
            {
                CarType type = currentCars[i].GetComponent<CarType>();

                if (type.carType.ToString() == "Bus")
                    mergeCars.Add(currentCars[i].GetComponent<CarController>());
            }
            return true;
        }

        return false;

    }
    public void Merge()
    {
        mergePrice += 10;
        StartCoroutine(Merging());
    }
    IEnumerator Merging()
    {
        isMerging = true;

        mergeCars[0].DestroyCar();
        mergeCars[1].DestroyCar();
        mergeCars[2].DestroyCar();

        mergeCars[0].transform.DOScale(0, 1);
        mergeCars[1].transform.DOScale(0, 1);
        mergeCars[2].transform.DOScale(0, 1);

        yield return new WaitForSeconds(0.7f);

        mergeCars[0].gameObject.SetActive(false);
        mergeCars[1].gameObject.SetActive(false);

        currentCars.Remove(mergeCars[0].gameObject);
        currentCars.Remove(mergeCars[1].gameObject);

        mergeCars[2].LevelUp();
        mergeCars[2].transform.DOScale(0.3f, 1);

        Destroy(mergeCars[0]);
        Destroy(mergeCars[1]);

        mergeCars.RemoveRange(0, mergeCars.Count);

        for (int i = 0; i < currentCars.Count; i++)
        {
            currentCars[i].GetComponent<CarController>().forwardCar = null;
        }

        yield return new WaitForSeconds(0.1f);

        isMerging = false;
    }
    #endregion

    #region Income

    private bool IncomeControl()
    {
        if (MoneyManager.instance.GetCurrentMoney() < incomePrice) return false;

        return true;
    }
    public void IncomeUpgrade()
    {
        incomePrice += 10;

        MoneyManager.instance.incomeSpeed += 0.5f;

        MoneyManager.instance.IncreaseIncomeValue(2);
    }

    #endregion
}
