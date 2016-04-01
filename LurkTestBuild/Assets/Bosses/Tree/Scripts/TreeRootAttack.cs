using UnityEngine;
using System.Collections;

public class TreeRootAttack : MonoBehaviour {

    public void animate()
    {
        GetComponent<Animator>().SetBool("Rise", false);
    }
}
