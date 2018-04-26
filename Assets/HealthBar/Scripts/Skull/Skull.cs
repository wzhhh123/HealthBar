using Assets.HealthBar.Scripts.Framework;
using UnityEngine;

namespace Assets.HealthBar.Scripts.Skull
{


    public class SkullArgs : HealthBarArgsBase { }


    public class Skull : HealthBarBase {
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
            SkullArgs args = arg as SkullArgs;
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
            }
        }

        void Update()
        {
            UpdateProgress(BloodBar);
            UpdateProgress(BlueBar);
            UpdateSkillCD(Skill1Mask);
            UpdateSkillCD(Skill2Mask);
        }


    }
}
