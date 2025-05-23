public abstract class Plante
{
    public string Nom { get; set; } //pour différencier les différentes plantes 
    public enum TypePlante { Fruit, Legume, Fleur, Herbe} //  0 = Fruit, 1 = Légume, 2 = Fleur,..
    public TypePlante Type { get; set; }
    public string MeteoPref { get; private set; }
    public string TypeSolNecessaire { get; private set; }
    public float HumiditeNecessaire { get; private set; }
    public float TemperatureNecessaire { get; private set; }
    public int LuminositeNecessaire { get; private set; }
    public int EspaceNecessaire { get; private set; } // "Necessaire" permet de mettre en évidence que ce sont des conditions spécifiques à la plante  
    public enum EtatSante {EnBonneSante, Malade, Morte} // pour différencier les différents états de la plante 
    public EtatSante Sante {get; set;}
    public int EsperanceDeVie { get; private set; }
    public Terrain? Terrain { get; set; } // association avec le terrain où est plantée la plante
    public float Croissance {get; set;}
    public bool EstArrosee = false;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int EmojiAvantMaladie { get; set; } // pour restaurer l’emoji après guérison


    public Plante(string nom, string meteoPref, TypePlante type, string typeSolNecessaire, float humiditeNecessaire, float temperatureNecessaire, int luminositeNecessaire, int espaceNecessaire, int esperanceDeVie, float croissance = 0) // utilisation d'une constante t peu importe pour enum
    {
        Nom = nom;
        MeteoPref = meteoPref;
        Type = type;
        TypeSolNecessaire = typeSolNecessaire;
        HumiditeNecessaire = humiditeNecessaire;
        TemperatureNecessaire = temperatureNecessaire;
        LuminositeNecessaire = luminositeNecessaire;
        EspaceNecessaire = espaceNecessaire;
        Croissance = croissance;
        Sante = EtatSante.EnBonneSante;
        EsperanceDeVie = esperanceDeVie;
    }

    public float CroissanceSelonConditions(Terrain terrain)
    {
        int nbConditionsTotal = 6;
        int conditionsOk = 0;
        if (terrain.Meteo == MeteoPref)
        {
            conditionsOk++;
        }
        if (terrain.TypeSol == TypeSolNecessaire)
        {
            conditionsOk++;
        }
        if (Math.Abs(terrain.Humidite - HumiditeNecessaire) <= 3) // fonction qui renvoie la valeur absolue avec une marge de +/-3
        {
            conditionsOk++;
        }
        if (Math.Abs(terrain.Luminosite - LuminositeNecessaire) <= 3)
        {
            conditionsOk++;
        }
        if (Math.Abs(terrain.Temperature - TemperatureNecessaire) <= 5)
        {
            conditionsOk++;
        }
        if (terrain.Surface >= EspaceNecessaire)
        {
            conditionsOk++;
        }
        return (float)(conditionsOk / (double)nbConditionsTotal) * 100;
    }
    public void AfficherEvolutionPlantes()
{
    if (Sante == EtatSante.Morte)
    {
        return; 
    }

    if (Croissance <= 0.5f)
    {
        Console.WriteLine($"{Nom} vient de germer");
    }
    else if (Croissance < 1.3f)
    {
        Console.WriteLine($"{Nom} est en croissance");
    }
    else if (Croissance >= 1.5f)
    {
        Console.WriteLine($"{Nom} est mature");
        AtteindreEtatFinal();
    }
}
    public void MettreAJourCroissance() // sert à faire évoluer la plante à chaque tour en fonction des conditions du terrain et si la plante a été arrosée 
    {

         float pourcentageConditions = CroissanceSelonConditions(Terrain);


        if(EstArrosee)
        {
            Croissance += 1.5f;// 0.5 pour que ce soit plus rapide
            EstArrosee = false;
        }
        else if(pourcentageConditions < 75)
        {
            Croissance += 0.4f;
            Console.WriteLine($"{Nom} pousse bien ! {pourcentageConditions}%");
        }
        else
        {
            Croissance += 0.5f;
            Console.WriteLine($"{Nom} pousse VITE !! {pourcentageConditions}%");
        }
        AfficherEvolutionPlantes();
    }

        public void ArroserPlantes() //Sert à arroser une plante une seule fois 
    {
        if (!EstArrosee)// si non arrosée => on veut qu'elle soit arrosée
        {
            EstArrosee = true;
            Console.WriteLine($"{Nom} est arrosée 💧 !!");
        }
        else
        {
            Console.WriteLine("Cette plante a déjà été arrosée récemment"); // si arrosée est true alors on ne peut pas arroser une nouvelle fois 
        }
    }

    public abstract void AtteindreEtatFinal();

    public void Contaminer(string cause)
    {
        if (Sante == EtatSante.EnBonneSante)
        {
            EmojiAvantMaladie = Terrain.T[PositionX, PositionY]; // on sauvegarde l'état
            Sante = EtatSante.Malade;
            Console.WriteLine($"{Nom} a été contaminée par {cause} !");
        }
    }

    public void Soigner(Maladie m)
{
    if (Sante == EtatSante.Malade)
    {
        Sante = EtatSante.EnBonneSante;
        Console.WriteLine($"{Nom} a été soignée par {m.Nom} ! ");
        
        // Restaurer l’emoji sauvegardé
        if (Terrain != null)
        {
            Terrain.T[PositionX, PositionY] = EmojiAvantMaladie;
        }
    }
}
    public void ExposerAuSoleil() // permet d'exposer la plante au soleil (augmente légèrement la croissance)
    {
        Croissance += 0.1f;
        Console.WriteLine($"{Nom} a profité du soleil ☀️!");
    }
    
    public override string ToString()
    {
        return $"Nom : {Nom}, Type : {Type}, Santé : {Sante}, Croissance : {Croissance}";
    }


}