using UnityEngine;

public class RockRespawn : MonoBehaviour
{
    public RockMine rockMineScript;
    public Gem gemScript;

    public GameObject rockObject;

    private float respawnTimer;
    private int timeToRespawn = 15;
    void Update()
    {
        if(gemScript.GemPickedUp)
        {
            respawnTimer += Time.deltaTime;

            if(respawnTimer >= timeToRespawn)
            {
                respawnTimer = 0;
                gemScript.GemPickedUp = false;
                rockMineScript.shake = false;
                rockMineScript.rockMineTime = 0;
                rockMineScript.audioTime = 0;
                rockMineScript.mineTimeText.text = "";
                rockObject.SetActive(true);
                rockObject.transform.localPosition = Vector3.zero;
                rockMineScript.RockMesh.SetActive(true);
            }
        }
    }
}
