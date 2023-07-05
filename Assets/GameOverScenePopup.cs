using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolitaireTripeaks
{
    public class GameOverScenePopup : MonoBehaviour
    {
        // Start is called before the first frame update
        public void oncloser()
        {
            LeveEndScene levelend = new LeveEndScene();
            levelend.NextClick();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}