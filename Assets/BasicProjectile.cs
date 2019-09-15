using UnityEngine;

public enum ProjectileType{
    BALLISTIC,ENERGY,ROCKET
}
public class BasicProjectile : MonoBehaviour {
    protected float speed;
    protected int damage;
    protected Vector2 direction;
    protected bool playerAligned;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected AudioClip a;
    public float Speed{
        get{return speed;}
    }
    public float Direction{
        get{return direction.x;}
    }
    void Awake(){
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    void Update(){
        transform.rotation = Quaternion.LookRotation(Vector3.forward,direction);
        transform.Translate(transform.up*speed*Time.deltaTime,Space.World);
    }

    public void InitializeProjectile(float s, Vector2 dir, int d){
        direction=dir; damage=d;
        speed=s; playerAligned=false;
    }

    public void FlipAlignment(float scl){
        playerAligned=!playerAligned;
        spriteRenderer.color = playerAligned?Color.green:Color.red;
        if(playerAligned) damage = 100;
        direction*=-scl;
        direction.Normalize();
    }

    //BALLISTICS - can be SLASHED REFLECTED DEFLECTED and BLOCKED
    //ENERGY - can be ESLASHED EREFLECTED and BLOCKED
    //ROCKETS - can be BASHED DEFLECTED and BLOCKED, will detonate if SLASHED
    void OnTriggerEnter2D(Collider2D other){
        if(!playerAligned && other.gameObject.layer ==12) return;
        if(playerAligned && other.gameObject.layer==11) return;
        if(other.gameObject.layer==2) return;
        if(other.gameObject.layer==10) return;
        EquipmentEffect e = other.GetComponent<EquipmentEffect>();
        if(e!=null){
            switch(e.State){
                case(EffectState.DEFLECT):
                direction += Random.Range(-0.5f,0.5f) * Vector2.up;
                FlipAlignment(2f);
                return;
                case(EffectState.REFLECT): e.PlaySound(a);
                FlipAlignment(4f); return;
                case(EffectState.SLASH):
                case(EffectState.ESLASH):
                case(EffectState.BLOCK): gameObject.SetActive(false); return; 
                default: return;
            }
        }
        else{
            if(other.GetComponent<IDamageable>()!=null) 
                other.GetComponent<IDamageable>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}