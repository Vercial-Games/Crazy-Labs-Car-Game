using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerChar : MonoBehaviour
{
    public Animator anim;

    [SerializeField] GameObject[] type;

    private void Start()
    {
        int randomize = Random.Range(0, type.Length);
        type[randomize].SetActive(true);
    }
}
