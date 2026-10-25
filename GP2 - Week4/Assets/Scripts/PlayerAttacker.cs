using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public float attackPower = 25;

    private void Update()
    {
        // 0 = LMB
        // 1 = RMB
        // 2 = MMB
        //If we click on enemies they will get damaged
        //Left click triggers ray cast attack from camera
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                //We search exclusively for the Actor component on the hit object, if it exists we call the takeDamage method on it
                Actor targetActor = hitInfo.collider.GetComponent<Actor>();
                if (targetActor != null)
                {
                    //Polymorphism in Action: Unity automatically calls the child override.
                    targetActor.takeDamage(attackPower);
                    targetActor.performAttack();
                }
            }
        }
    }
}
