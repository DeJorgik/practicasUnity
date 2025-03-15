using UnityEngine;

public class playerScript : MonoBehaviour {
    private void Start() {
        EventManager.OnPlayerOutcome += reachFinal;
    }

    public void reachFinal(GameConstants.gameResult result) {
        EventManager.TriggerPlayerOutcome(result);
    }
    
    public void stopMovement() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void respawnAt(Vector3 position) {
        transform.position = position;
    }
}
