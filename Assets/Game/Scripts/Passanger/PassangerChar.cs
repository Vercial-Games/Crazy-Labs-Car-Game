using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerChar : MonoBehaviour
{
    public Animator anim;
    public Animation canvasAnim;
    [SerializeField] GameObject[] type;

    private void Start()
    {
        int randomize = Random.Range(0, type.Length);
        type[randomize].SetActive(true);
    }
}
