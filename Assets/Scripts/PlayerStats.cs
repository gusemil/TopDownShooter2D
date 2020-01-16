public class PlayerStats
{
    private int hp;
    private int damageMultiplier;
    private bool isHexDamageUp;
    private bool isDashing;
    private bool isInvulnerable;
    private bool isRespawning;
    private bool isInfiniteAmmoUp;
    private bool isShieldUp;
    private bool isInfiniteDashUp;
    private bool isGodModeUp;

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public int DamageMultiplier
    {
        get { return damageMultiplier; }
        set { damageMultiplier = value; }
    }

    public bool IsHexDamageUp
    {
        get { return isHexDamageUp; }
        set { isHexDamageUp = value; }
    }

    public bool IsDashing
    {
        get { return isDashing; }
        set { isDashing = value; }
    }

    public bool IsInvulnerable
    {
        get { return isInvulnerable; }
        set { isInvulnerable = value; }
    }

    public bool IsRespawning
    {
        get { return isRespawning; }
        set { isRespawning = value; }
    }

    public bool IsInfiniteAmmoUp
    {
        get { return isInfiniteAmmoUp; }
        set { isInfiniteAmmoUp = value; }
    }

    public bool IsShieldUp
    {
        get { return isShieldUp; }
        set { isShieldUp = value; }
    }

    /*
    public float InvulnerabilityTime
    {
        get { return invulnerabilityTime; }
        set { invulnerabilityTime = value; }
    }
    */

    public bool IsInfiniteDashUp
    {
        get { return isInfiniteDashUp; }
        set { isInfiniteDashUp = value; }
    }

    public bool IsGodModeUp
    {
        get { return isGodModeUp; }
        set { isGodModeUp = value; }
    }

    public PlayerStats()
    {
        hp = 1;
        damageMultiplier = 1;
        //invulnerabilityTime = 0.35f;
        isHexDamageUp = false;
        isInfiniteAmmoUp = false;
        isDashing = false;
        isInvulnerable = false;
        isRespawning = false;
        isShieldUp = false;
        isInfiniteDashUp = false;
        isGodModeUp = false;
    }

    public void TakeDamage(int dmg)
    {
            hp -= dmg;

            if (hp <= 0)
            {
                GameManager.instance.LoseLife();
            }
    }
}
