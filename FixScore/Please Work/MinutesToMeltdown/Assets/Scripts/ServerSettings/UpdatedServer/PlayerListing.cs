using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    #region Private Variables
    /// <summary>
    /// Just getting the playername and creating the text variable that shows it
    /// </summary>
    [SerializeField]
    private Text _playerName;
    private Text PlayerName
    {
        get { return _playerName; }
    }
    #endregion

    #region Public Variables
    /// <summary>
    /// Getting the photon player and settng it, used for naming
    /// </summary>
    public PhotonPlayer PhotonPlayer { get; private set; }
    #endregion

    #region Public Methods
    /// <summary>
    /// Sets the argument to the variable photon player and sets the playername to the photon player name nickname
    /// </summary>
    /// <param name="photonPlayer"></param>
    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;
    }
    #endregion
}
