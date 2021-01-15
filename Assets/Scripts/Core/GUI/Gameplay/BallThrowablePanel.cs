using UnityEngine;

namespace SplitSpheres.Core.GUI
{
    /// <summary>
    /// DIsplays information related to the Throwable balls
    /// </summary>
    public class BallThrowablePanel : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI ballNumbersValue;
        
        public void DisplayNumberOfBalls(int number)
        {
            ballNumbersValue.text = number.ToString();
        }
        
    
        
    }
}
