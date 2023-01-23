using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{  
    public void muteToggle(bool muted){
        if(muted == true){
            AudioListener.volume = 0;
        }
        else{
            AudioListener.volume = 1;
        }
    }
}