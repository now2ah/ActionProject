using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class SkillIconUI : UI
    {
        Image _attackImage;
        Image _dashImage;
        public Image AttackImage => _attackImage;
        public Image DashImage => _dashImage;

        protected override void Awake()
        {
            base.Awake();
            _attackImage = transform.GetChild(0).GetComponent<Image>();
            _dashImage = transform.GetChild(1).GetComponent<Image>();
        }

    }
}

