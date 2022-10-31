using UnityEngine;

public class HitRegister : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // For each child of the enemy
            foreach (Transform child in collision.gameObject.transform)
            {
                // Check if the child is a mesh renderer
                if (child.GetComponent<MeshRenderer>())
                {
                    // Set the mesh renderer to enabled
                    child.GetComponent<MeshRenderer>().material.SetColor("_Color_1", Color.red);
                }
            }
        }
    }
}
