using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Action.UI
{
    public class PhaseTextUI : UI
    {
        TextMeshProUGUI _text;
        public TextMeshProUGUI Text { get { return _text; } set { _text = value; } }

        protected override void Awake()
        {
            base.Awake();
            _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }
}

