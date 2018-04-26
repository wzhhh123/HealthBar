using UnityEngine;

namespace Assets.Framework.Scripts
{
    public class Driver : MonoBehaviour {

        void Awake()
        {
            Init();
        }

        void Init()
        {
            Debuger.EnableLog = true;
            Debuger.EnableTime = true;
            this.Log("Init()");
        }
    }
}
