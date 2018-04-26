using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.HealthBar.Scripts.Framework;
using Assets.HealthBar.Scripts.Shield;
using Assets.HealthBar.Scripts.Skull;
using UnityEngine;

namespace Assets.HealthBar
{
    public class Controller : MonoBehaviour
    {
        public int MoveSpeed = 1;
        public RectTransform CanvasRectTransform;
        [HideInInspector]
        public RectTransform Rect;
        //每一种血条的数量
        public int InstanceBarNPT = 5; 
        //每10帧变化一次数值
        private int _rate;

        private List<RectTransform> _skulls = null;
        private List<RectTransform> _shield = null;
        private List<RectTransform> _sliderOnly = null;
        private List<RectTransform> _mulBar = null;
        void Start()
        {
            this.Log(CanvasRectTransform.rect.xMin +  " " + CanvasRectTransform.rect.yMin);
            Init();
        }

        void Init()
        {
            _rate = 0;
            InstanceSkulls();
        }

        void Update()
        {
            ++_rate;
            if (_rate % 30 == 0)
            {
                _rate = 0;
                UpdateBarData();
            }
            MoveBar();
        }

        #region  添加新血条 三个函数都要修改

        void UpdateBarData()
        {
            for (int i = 0; i < _skulls.Count; ++i)
            {
                SkullArgs args = new SkullArgs
                {
                    BloodValue = Random.Range(0, 1.0f),
                    BlueValue = Random.Range(0, 1.0f),
                    Skill1CdTime = Random.Range(0, 13),
                    Skill2CdTime = Random.Range(0, 2),
                };
                _skulls[i].GetComponent<HealthBarBase>().ChangeValue(args);
                MoveBarRd(_skulls[i]);
            }
            for (int i = 0; i < _shield.Count; ++i)
            {
                ShieldArgs args = new ShieldArgs
                {
                    BloodValue = Random.Range(0, 1.0f),
                    BlueValue = Random.Range(0, 1.0f),
                    Skill1CdTime = Random.Range(0, 13),
                    Skill2CdTime = Random.Range(0, 2),
                };
                _shield[i].GetComponent<HealthBarBase>().ChangeValue(args);
                MoveBarRd(_shield[i]);
            }
            for (int i = 0; i < _sliderOnly.Count; ++i)
            {
                SliderOnlyArgs args = new SliderOnlyArgs
                {
                    BloodValue = Random.Range(0, 1.0f),
                    BlueValue = Random.Range(0, 1.0f),
                    Skill1CdTime = Random.Range(0, 13),
                    Skill2CdTime = Random.Range(0, 2),
                };
                _sliderOnly[i].GetComponent<HealthBarBase>().ChangeValue(args);
                MoveBarRd(_sliderOnly[i]);
            }
            for (int i = 0; i < _mulBar.Count; ++i)
            {
                MulBarArgs args = new MulBarArgs
                {
                    BloodValue = Random.Range(0, 1.0f),
                    BlueValue = Random.Range(0, 1.0f),
                    Skill1CdTime = Random.Range(0, 13),
                    Skill2CdTime = Random.Range(0, 2),
                    GreenValue = Random.Range(0, 1.0f),
                    BottleGreenValue = Random.Range(0, 1.0f),
                    OrgValue = Random.Range(0, 1.0f),
                };
                _mulBar[i].GetComponent<HealthBarBase>().ChangeValue(args);
                MoveBarRd(_mulBar[i]);
            }
        }

        void MoveBar()
        {
            for (int i = 0; i < _skulls.Count; ++i)
            {
                MoveBarRd(_skulls[i]);
            }
            for (int i = 0; i < _shield.Count; ++i)
            {
                {
                    MoveBarRd(_shield[i]);
                }
            }
            for (int i = 0; i < _sliderOnly.Count; ++i)
            {
                {
                    MoveBarRd(_sliderOnly[i]);
                }
            }
            for (int i = 0; i < _mulBar.Count; ++i)
            {
                {
                    MoveBarRd(_mulBar[i]);
                }
            }
        }


        void InstanceSkulls()
        {
            _skulls = new List<RectTransform>();
            for (int i = 0; i < InstanceBarNPT; ++i)
            {
                GameObject obj = Resources.Load("HealthBar/Skull") as GameObject;
                obj = Instantiate(obj);
                obj.transform.SetParent(CanvasRectTransform.transform);
                Rect = obj.GetComponent<RectTransform>();
                Rect.anchoredPosition = GetBarRandomPos(Rect);
                _skulls.Add(Rect);
            }
            _shield = new List<RectTransform>();
            for (int i = 0; i < InstanceBarNPT; ++i)
            {
                GameObject obj = Resources.Load("HealthBar/Shield") as GameObject;
                obj = Instantiate(obj);
                obj.transform.SetParent(CanvasRectTransform.transform);
                Rect = obj.GetComponent<RectTransform>();
                Rect.anchoredPosition = GetBarRandomPos(Rect);
                _shield.Add(Rect);
            }
            _sliderOnly = new List<RectTransform>();
            for (int i = 0; i < InstanceBarNPT; ++i)
            {
                GameObject obj = Resources.Load("HealthBar/SliderOnly") as GameObject;
                obj = Instantiate(obj);
                obj.transform.SetParent(CanvasRectTransform.transform);
                Rect = obj.GetComponent<RectTransform>();
                Rect.anchoredPosition = GetBarRandomPos(Rect);
                _sliderOnly.Add(Rect);
            }
            _mulBar = new List<RectTransform>();
            for (int i = 0; i < InstanceBarNPT; ++i)
            {
                GameObject obj = Resources.Load("HealthBar/MulBar") as GameObject;
                obj = Instantiate(obj);
                obj.transform.SetParent(CanvasRectTransform.transform);
                Rect = obj.GetComponent<RectTransform>();
                Rect.anchoredPosition = GetBarRandomPos(Rect);
                _mulBar.Add(Rect);
            }
        }

        #endregion



        void MoveBarRd(RectTransform rect)
        {
            int leftOrRight = Random.Range(0, 2);
            int upOrDown = Random.Range(0, 2);

            leftOrRight = (leftOrRight == 0) ? -MoveSpeed : MoveSpeed;
            upOrDown = (upOrDown == 0) ? -MoveSpeed : MoveSpeed;

            float tempX = rect.anchoredPosition.x + leftOrRight * Time.deltaTime;
            float tempY = rect.anchoredPosition.y + upOrDown * Time.deltaTime;

            tempX = Mathf.Clamp(tempX, CanvasRectTransform.rect.xMin + rect.rect.xMax,
                CanvasRectTransform.rect.xMax - rect.rect.xMax);
            tempY = Mathf.Clamp(tempY, CanvasRectTransform.rect.yMin + rect.rect.yMax,
                CanvasRectTransform.rect.yMax - rect.rect.yMax);

            rect.anchoredPosition = new Vector2(tempX, tempY);
        }

        Vector2 GetBarRandomPos(RectTransform rect)
        {
            float tempX = CanvasRectTransform.sizeDelta.x - Rect.rect.xMax * 2;
            float tempY = CanvasRectTransform.sizeDelta.y - Rect.rect.yMax * 2;
            float x = Random.Range(0, tempX) - tempX / 2;
            float y = Random.Range(0, tempY) - tempY / 2;
            return new Vector2(x, y);
        }

        #region  测试Slider的变化速度

        void TestSliderSpeed()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SkullArgs args = new SkullArgs()
                {
                    BloodValue = 1,
                    BlueValue = 1,
                    Skill1CdTime = 100,
                    Skill2CdTime = 30f,
                };
                Rect.GetComponent<HealthBarBase>().ChangeValue(args);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                SkullArgs args = new SkullArgs()
                {
                    BloodValue = 0,
                    BlueValue = 0,
                    Skill1CdTime = 0,
                    Skill2CdTime = 1.1f,
                };
                Rect.GetComponent<HealthBarBase>().ChangeValue(args);
            }
        }

        #endregion

    }
}
