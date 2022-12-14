using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class PassangerChar : MonoBehaviour
{
    public Animation canvasAnim;
    public TMP_Text moneyText;

    [SerializeField] GameObject[] type;

    Animator anim;


    private void Start()
    {
        int randomize = Random.Range(0, type.Length);
        type[randomize].SetActive(true);
        anim = type[randomize].GetComponent<Animator>();
    }
    private void Update()
    {
        moneyText.text = "$"+ MoneyManager.instance.GetIncomeValue();
    }
    public void Jump()
    {
        for (int i = 0; i < type.Length; i++)
        {
            if (type[i].activeInHierarchy)
                anim =type[i].GetComponent<Animator>();
        }
        anim.SetTrigger("Jump");
    }
    public void JumpSound()
    {
        GetComponent<AudioSource>().Play();
    }

}
