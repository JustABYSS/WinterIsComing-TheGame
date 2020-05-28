using UnityEngine;

public class Eagle : Enemy
{
    private Collider2D coll;
    private AudioSource dead;
    public void JumpedOn()
    {
        anim.SetTrigger("Death");
    }
    private void death()
    {
        Destroy(this.gameObject);

    }

    private void Dead()
    {
        dead = GetComponent<AudioSource>();
        dead.Play();
    }
    

}
