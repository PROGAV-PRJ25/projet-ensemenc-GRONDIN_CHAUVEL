public class Fraise : Fruit
{
    public Fraise() : base("Fraise", "Chaud", TypePlante.Fruit, "Limoneux", 40f, 20f, 6, 30, 3) {}
    public override int EtatFinal()
    {
        return 15;
    }
}