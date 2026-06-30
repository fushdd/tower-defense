using System.Collections.Generic;
using UnityEngine;

public interface ITowerAttack
{
    void Attack(List<GameObject> enemies, GameObject attackUI, float damage);
}
