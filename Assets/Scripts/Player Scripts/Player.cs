using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Avatars;

namespace punk_vs_robots 
{
    public class Player : MonoBehaviour
    {
        void Update()
        {
            //Getting avatar reference
            var avatar = VRAvatar.Active;
            if (avatar == null)
            {
                return;
            }

            Debug.Log("Checking avatar... " + avatar);
        }
    }

}
