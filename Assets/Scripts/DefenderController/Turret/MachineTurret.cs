using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTurret : Defender
{
    TableTurret.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableTurret.GetTurretData(401);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath);
    }
}
