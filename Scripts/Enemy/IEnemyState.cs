using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{

    void UpdateState();

    void ToSearchState();

    void ToWaitState();

    void ToWalkState();

    void ToAttackState();
}
