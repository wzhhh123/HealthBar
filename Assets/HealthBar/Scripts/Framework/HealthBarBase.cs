using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HealthBar.Scripts.Framework
{
    [Serializable]
    public class SliderBar
    {
        public Slider Tar;
        public Image ProgressMask;
        [HideInInspector]
        public float TargetValue;
    }
    [Serializable]
    public class SkillMask
    {
        public Image SkillImageMask;
        public Text SkillCdText;
        [HideInInspector]
        public float CurVal;
        public void Disable()
        {
            if (SkillImageMask != null)
            {
                SkillImageMask.gameObject.SetActive(false);
            }
        }

        public void Enable()
        {
            if (SkillImageMask != null)
            {
                SkillImageMask.gameObject.SetActive(true);
            }
        }

        public void SetValue(float dt)
        {
            if(!SkillImageMask.gameObject.activeSelf) Enable();
            SkillCdText.text = dt.ToString("0.0");
            CurVal = dt;
        }
    }


    public class HealthBarArgsBase
    {
        public float Skill1CdTime;
        public float Skill2CdTime;
        public float BlueValue;
        public float BloodValue;
    }


    public abstract class HealthBarBase : MonoBehaviour
    {
        //滑动条数值变化的速度
        public float SliderValChangeSpeed = 5f;
        //缓存条数值的变化速度
        public float SliderMaskValChangeSpeed = 2f;
        public Text Name;
        public SliderBar BloodBar;
        public SliderBar BlueBar;
        public SkillMask Skill1Mask;
        public SkillMask Skill2Mask;

        public virtual void Init(object arg = null)
        {
            this.Log("Init() arg:{0}", arg);
        }

        public abstract void ChangeValue(object arg = null);



        public void UpdateSkillCD(SkillMask skillMask)
        {
            if (Mathf.Abs(skillMask.CurVal) < 1e-6 || skillMask.CurVal < 0)
            {
                skillMask.CurVal = 0;
                skillMask.Disable();
            }
            else
            {
                skillMask.SetValue(skillMask.CurVal - Time.deltaTime);
            }
        }


        public void UpdateProgress(SliderBar bar)
        {
            bar.Tar.value = Mathf.Lerp(bar.Tar.value, bar.TargetValue,
                Time.deltaTime * SliderValChangeSpeed);
            if (bar.ProgressMask != null)
            {
                //例如是扣血 则显示缓冲，加血则没必要计算缓冲
                if (bar.TargetValue < bar.Tar.value)
                {
                    bar.ProgressMask.fillAmount = Mathf.Lerp(bar.ProgressMask.fillAmount, bar.TargetValue,
                        Time.deltaTime * SliderMaskValChangeSpeed);
                }
                else
                {
                    bar.ProgressMask.fillAmount = bar.Tar.value;
                }
            }
        }
    }
}
