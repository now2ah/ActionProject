using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class WaveCamUI : UI
    {
        float _time;
        RectTransform _rectTransform;

        public override void Initialize()
        {
        }

        IEnumerator _ShowForTime(float time)
        {
            _rectTransform.anchoredPosition = new Vector3(710, 315);
            //transform.position = new Vector3(710, 315);
            yield return new WaitForSeconds(time);
            Hide();
        }

        protected override void Awake()
        {
            base.Awake();
            _time = 15.0f;
            _rectTransform = GetComponent<RectTransform>();
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(_ShowForTime(_time));
        }
    }
}
