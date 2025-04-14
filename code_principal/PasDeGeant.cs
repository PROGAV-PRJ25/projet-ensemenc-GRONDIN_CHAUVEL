using System.Security.Cryptography.X509Certificates;

public class PasDeGeant : Obstacle
{
    public PasDeGeant () : base ("Pas de géant", "destructeur", "Tout", "Ecrase les plantations", 0.9f, 1)
    {}

    public override void Action()
    {
        
        Random random = new Random ();
        int PositionX = random.Next (0,Univers.Lignes-1);
        int PositionY = random.Next (0,Univers.Colonnes-1);
        Univers.TerrainVisuel [PositionX,PositionY] = 3;
        Univers.TerrainVisuel [PositionX+1,PositionY] = 3;
        Univers.TerrainVisuel [PositionX,PositionY+1] = 3;
        Univers.TerrainVisuel [PositionX-1,PositionY] = 3;
        Univers.TerrainVisuel [PositionX,PositionY-1] = 3;
        Univers.TerrainVisuel [PositionX+1,PositionY+1] = 3;
        Univers.TerrainVisuel [PositionX-1,PositionY-1] = 3;
        Univers.TerrainVisuel [PositionX+1,PositionY-1] = 3;
        Univers.TerrainVisuel [PositionX-1,PositionY+1] = 3;
    }
}