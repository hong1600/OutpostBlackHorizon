using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    [SerializeField] FieldBuild fieldBuild;
    [SerializeField] TurretBuild turretBuild;
    [SerializeField] FieldSelector fieldSelector;

    protected override void Awake()
    {
        base.Awake();

        FieldBuild = fieldBuild;
        TurretBuild = turretBuild;
        FieldSelector = fieldSelector;
    }

    public FieldBuild FieldBuild { get; private set; }
    public TurretBuild TurretBuild { get; private set; }
    public FieldSelector FieldSelector { get; private set; }
}
