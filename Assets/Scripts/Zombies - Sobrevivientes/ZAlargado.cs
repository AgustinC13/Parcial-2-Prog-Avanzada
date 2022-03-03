public class ZAlargado : Zombie
{
    private void Start()
    {
        float multiplicadorVelocidad = 0.75f;

        agente.speed *= velocidadZ * multiplicadorVelocidad;
        velocidadOriginal = agente.speed;
        dañoZ *= 1.5f;
        vidaZ *= 0.75f;
        rangoZ *= 1.75f;
        CooldownAtaque /= velocidadZ * multiplicadorVelocidad;
        animator.speed *= velocidadZ * multiplicadorVelocidad;
    }
}