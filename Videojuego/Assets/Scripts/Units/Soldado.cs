using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : UnitBehaviour
{

    protected override void OnAnimation()
    {
        getAnimator().SetBool("running", IsMoving());
    }
}
