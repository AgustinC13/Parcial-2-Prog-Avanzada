public class ZRápido : Zombie
{
    private void Start()
    {
        float multiplicadorVelocidad = 1.65f;

        agente.speed *= velocidadZ * multiplicadorVelocidad;
        velocidadOriginal = agente.speed;
        vidaZ *= .5f;
        CooldownAtaque /= velocidadZ * multiplicadorVelocidad;
        animator.speed *= velocidadZ * multiplicadorVelocidad;
    }
}