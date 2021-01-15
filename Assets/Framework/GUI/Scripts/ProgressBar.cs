using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SplitSpheres.Framework.GUI.Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        public Image foreGroundImage;
        public TextMeshProUGUI valueText;

        public virtual void Progress(float maxValue, float newValue)
        {
            var ammount = newValue / maxValue;
            foreGroundImage.fillAmount = ammount;
            
            var percent = (ammount * 100);

            if (percent > 100)
            {
                percent = 100;
            }
            valueText.text = percent.ToString("0.") + "%";

        }
    }
}
