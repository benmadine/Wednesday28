using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Bedlamk.Minutes2Metldown
{
    ///<summary>
    ///Player name input Field. Lets user have an input name
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Variables
        //Store the PlayerPref Key to avoid typos
        static string playerNamePrefKey = "PlayerName";
        #endregion

        #region Monobehvaiour CallBacks
        private void Start()
        {
            string defaultName = "";
            InputField _inputField = this.GetComponent<InputField>();
            if(_inputField != null)
            {
                if(PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
            PhotonNetwork.playerName = defaultName;
        }
        #endregion


        #region Public Methods
        ///<summary>
        ///Sets the name of the player and saves it in PlayerPrefs
        ///</summary>
        public void SetPlayerName(string value)
        {
            PhotonNetwork.playerName = value + " "; //FOrce a trailing space string incase value is an empty string

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }
}
