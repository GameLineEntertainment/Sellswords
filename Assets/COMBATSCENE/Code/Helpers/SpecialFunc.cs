using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatScene
{
    public class SpecialFunc : IUpdate, IAwake
    {
        private SpecialFuncManager.Settings _special;

        public void OnAwake()
        {
            _special = Object.FindObjectOfType<Manager>().SpecialFuncSettings.Function;
        }

        public void OnUpdate()
        {
            if (_special.IsUseSlowMotion) SlowNotion();
        }

        private void SlowNotion()
        {
            if (Input.GetKeyDown(_special.SlowMotionKey))
            {
                Time.timeScale = _special.SlowSpeed;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
            if (Input.GetKeyUp(_special.SlowMotionKey))
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
        }
    }
}
