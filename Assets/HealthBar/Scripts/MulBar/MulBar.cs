using System.Collections;
using System.Collections.Generic;
using Assets.HealthBar.Scripts.Framework;
using UnityEngine;

public class MulBarArgs : HealthBarArgsBase
{
    public float GreenValue;
    public float OrgValue;
    public float BottleGreenValue;
}

public class MulBar : HealthBarBase
{

    public SliderBar GreenBar;
    public SliderBar OrgBar;
    public SliderBar BottleGreenBar;

    public override void Init(object arg = null)
    {
        base.Init(arg);
        string str = arg as string;
        if (str != null)
        {
            Name.text = str;
        }
    }

    public override void ChangeValue(object arg = null)
    {
        MulBarArgs args = arg as MulBarArgs;
        if (args != null)
        {
            if (Mathf.Abs(args.Skill1CdTime) < 1e-6 || args.Skill1CdTime < 0)
            {
                Skill1Mask.Disable();
            }
            else
            {
                Skill1Mask.SetValue(args.Skill1CdTime);
            }
            if (Mathf.Abs(args.Skill2CdTime) < 1e-6 || args.Skill2CdTime < 0)
            {
                Skill2Mask.Disable();
            }
            else
            {
                Skill2Mask.SetValue(args.Skill2CdTime);
            }
            BloodBar.TargetValue = args.BloodValue;
            BlueBar.TargetValue = args.BlueValue;
            GreenBar.TargetValue = args.GreenValue;
            BottleGreenBar.TargetValue = args.BottleGreenValue;
            OrgBar.TargetValue = args.OrgValue;
        }
    }

    void Update()
    {
        UpdateProgress(BloodBar);
        UpdateProgress(BlueBar);
        UpdateProgress(GreenBar);
        UpdateProgress(OrgBar);
        UpdateProgress(BottleGreenBar);

        UpdateSkillCD(Skill1Mask);
        UpdateSkillCD(Skill2Mask);
    }
}
