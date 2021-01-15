using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplitSpheres
{
    public class AdsManager : MonoBehaviour
    {
        const string PlaystoreGameID = "3974739";
        
        const string IOSGameID = "3974738";
        
        bool testMode = true;

        void Start () {
            
            Advertisement.Initialize (AndroidGameID, testMode);
        }
        
    }
}
