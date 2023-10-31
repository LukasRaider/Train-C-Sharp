using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrainStat;

namespace TrainStat
{
    
    class Locomotive {
        public Person Driver { get; set; }
        public Engine Engine { get; set; }
        

        public Locomotive(Person driver, Engine engine)
        {
            Driver = driver;
            Engine = engine;
        }
        public override String ToString()
        {
            return $"Lokomotiva ma ridice {Driver} a motor {Engine}";
        }
    }
    class Engine {
        private String type;
        public String Type { get; set; }
        public Engine(String type)
        { 
            Type = type;
        }
        public override String ToString()
        {
            return $"Lokomotiva ma {type}";
        }
    }
    class Person
    {
        private String firstName;
        private String lastName;

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public Person(String firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public override String ToString()
        {
            return $"Jmeno osoby je {FirstName} {LastName}";
        }
    }
    class Chair {
        private bool nearWindow;
        private int number;
        private bool reserved;

        public bool NearWindow {get;set;}
        public int Number { get; set; }
        public bool Reserved {  get; set; }
        public Chair(int number, bool nearWindow)
        { 
            Number = number;
            NearWindow = nearWindow;
            Reserved = false;
        }
    }
    class Bed {
        private int number;
        private bool reserved;

        public bool Reserved { get;set;} 
        public int Number { get; set; }
        public Bed(int number)
        { 
            Number = number;
            Reserved = false;
        }
    }
    class Door {
        private double height;
        private double width;
        public double Height { get; set; }
        public double Width { get; set; }
        public Door(double height, double width) 
        {
            Width = width;
            Height = height;
        }
    }
    class PersonalWagon {
        public List<Door> Doors { get; set; }
        public List<Chair> Sits { get; set; }
        public int NumberOfChairs { get; set; }
        public PersonalWagon(int numberOfChairs)
        { 
            NumberOfChairs = numberOfChairs;
            Doors = new List<Door>();
            Sits = new List<Chair>();
            for (int i = 0; i < NumberOfChairs; i++)
            {   int numberOfSeat = i+1;
                Sits.Add(new Chair(numberOfSeat, false)); }
        }
        //zde bych dal metodu rezervace pouze osobni vozy maji sedadla
        //public override string ToString() { return $"V ekonomickem voze je {numberOfChairs}";
    }
    interface IWagon
    {
        void ConnectToTrain(Train train);
        void DisconnectFromTrain(Train train);
        
    }
    class Economy : PersonalWagon, IWagon
    {
        public Economy(int numberOfChairs) : base(numberOfChairs)
        {
            NumberOfChairs = numberOfChairs;
        }

        public void ConnectToTrain(Train train)
        {
            if (train != null && train.Locomotive != null && train.Locomotive.Engine.Type != "parni" && train.Wagons.Count < 5)
            {
                train.AddWagon(this);
            }
        }

        public void DisconnectFromTrain(Train train)
        {
            if (train != null && train.Wagons.Contains(this))
            {
                train.RemoveWagon(this);
            }
        }
        
        
    }
    //public override string ToString(){return $"V ekonomickem voze je {numberOfChairs}";}
    class Business : PersonalWagon, IWagon 
    {
        private Person steward;
        public Person Steward { get; set; }
        public Business(Person steward, int numberOfChairs) : base(numberOfChairs) {
            NumberOfChairs = numberOfChairs;
            this.steward = steward;
        }
        public void ConnectToTrain(Train train)
        {
            if (train != null && train.Locomotive != null && train.Locomotive.Engine.Type != "parni" && train.Wagons.Count < 5)
            {
                train.AddWagon(this);
            }
        }

        public void DisconnectFromTrain(Train train)
        {
            if (train != null && train.Wagons.Contains(this))
            {
                train.RemoveWagon(this);
            }
        }
        
    }

   
            //public override String ToString() { return $"V Bussiness voze je {numberOfChairs} a {steward}"; }
    }
    class NightWagon : PersonalWagon, IWagon
        {
        public Bed[] Beds { get; set; }
        public int NumberOfBeds { get; set; } 
        

        public NightWagon(int numberOfChairs, int numberOfBeds) : base(numberOfChairs)
        {
            NumberOfChairs = numberOfChairs;
            NumberOfBeds = numberOfBeds;
            Beds = new Bed[numberOfChairs];
        }
    public void ConnectToTrain(Train train)
    {
        if (train != null && train.Locomotive != null && train.Locomotive.Engine.Type != "parni" && train.Wagons.Count < 5)
        {
            train.AddWagon(this);
        }
    }

    public void DisconnectFromTrain(Train train)
    {
        if (train != null && train.Wagons.Contains(this))
        {
            train.RemoveWagon(this);
        }
    }
    

//public override String ToString() { return $"Spaci vuz ma {numberOfBeds} posteli a {numberOfChairs} sedadel"; }
    }
    class Hopper : IWagon
    {
        private double loadingCapacity;
        public double LoadingCapacity { get; set; }
        public Hopper(double tonnage) { this.loadingCapacity = tonnage; }
    public void ConnectToTrain(Train train)
    {
        if (train != null && train.Locomotive != null && train.Locomotive.Engine.Type != "parni" && train.Wagons.Count < 5)
        {
            train.AddWagon(this);
        }
    }

    public void DisconnectFromTrain(Train train)
    {
        if (train != null && train.Wagons.Contains(this))
        {
            train.RemoveWagon(this);
        }
    }
 
    public override String  ToString() { return $"Nakladni vlak ma kapacitu {loadingCapacity}"; }
    }

class Train
{
    public Locomotive Locomotive { get; set; }
    public List<IWagon> Wagons { get; set;}

    public Train()
    {
        Wagons = new List<IWagon>();
    }
    public Train(Locomotive locomotive)
    {
        Locomotive = locomotive;
        Wagons = new List<IWagon>();
    }

    public Train(Locomotive locomotive, List<IWagon> wagons)
    {
        Locomotive = locomotive;
        Wagons = wagons;
    }

    public void AddWagon(IWagon wagon)
    {
        Wagons.Add(wagon);
    }

    public void RemoveWagon(IWagon wagon)
    {
        Wagons.Remove(wagon);
    }
    /*
    public void ReserveChairS(int wagonNumber, int chairNumber)
    {

        if (wagonNumber >= 0 && wagonNumber < Wagons.Count)
        {
            if (!(Wagons[wagonNumber - 1] is Hopper))
            {
                Wagons[wagonNumber]. ReserveChair(wagonNumber, chairNumber);
                //IWagon[wagonNumber].ReserveChair(wagonNumber,chairNumber);
                
            }
            else
            {
                Console.WriteLine("Nelze rezervovat");
            }
        }
        else
        {
            Console.WriteLine($"Vagón {wagonNumber} neexistuje.");
        }
    }
    */
    public void ReserveChair(int numberOfWagon, int numberOfChair)
    {

        if (numberOfWagon <= Wagons.Count)
        {
            if (!(Wagons[numberOfWagon - 1] is Hopper))
            {
                PersonalWagon personalWagon = ((PersonalWagon)Wagons[numberOfWagon - 1]);

                if (numberOfChair <= personalWagon.NumberOfChairs)
                {

                    if (!personalWagon.Sits[numberOfChair - 1].Reserved)
                    {
                        personalWagon.Sits[numberOfChair - 1].Reserved = true;
                        Console.WriteLine("Sedadlo {0} ve vagónu číslo {1} bylo zarezervováno", numberOfWagon, numberOfChair);
                    }
                    else
                    {
                        Console.WriteLine("Sedadlo {0} ve vagónu číslo {1} JE JIŽ OBSAZENO, rezervace neproběhla", numberOfWagon, numberOfChair);
                    }
                }
                else
                {
                    Console.WriteLine("Tento vagón neobsahuje sedadlo číslo {0}", numberOfChair);
                }
            }
            else
            {
                Console.WriteLine("Vagón s tímto číslem je nákladní, nejde zarezerovat místo");
            }

        }
        else
        {
            Console.WriteLine("Vlak nemá takovýto počet vagónů");
        }
    }


    public void ListReservedChairs()
    {
        for (int i = 0; i < Wagons.Count; i++)
        {
            if (Wagons[i] is PersonalWagon personalWagon)
            {
                Console.WriteLine($"Rezervovaná sedadla ve vagónu {i}:");
                for (int j = 0; j < personalWagon.Sits.Count; j++)
                {
                    if (personalWagon.Sits[j].Reserved)
                    {
                        Console.WriteLine($"Sedačka {j} je rezervována.");
                    }
                }
            }
        }
    }


    
    public override string ToString()
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine($"Vlak s lokomotivou: {Locomotive}");
    sb.AppendLine("Vagony:");
    for (int i = 0; i < Wagons.Count; i++)
    {
        sb.AppendLine($"Vagón {i}: {Wagons[i]}");
    }
    return sb.ToString();
}
}


    
        class Program
    {
    static void Main(string[] args)
    {
        // Vytvoření lokomotivy
        Locomotive dieselLocomotive = new Locomotive(new Person("Karel", "Novák"), new Engine("diesel"));
        Locomotive steamLocomotive = new Locomotive(new Person("Pavel", "Svoboda"), new Engine("parní"));

        // Vytvoření osobních vagónů
        Business businessWagon = new Business(new Person("Lenka", "Kozáková"), 30);
        Economy economyWagon = new Economy(40);
        NightWagon nightWagon = new NightWagon(20, 15);

        // Vytvoření vagónu pro náklad
        Hopper hopperWagon = new Hopper(50.5);

        // Vytvoření vlaků
        Train dieselTrain = new Train(dieselLocomotive);
        Train steamTrain = new Train(steamLocomotive);

        // Přidání osobních vagónů k dieselovému vlaku
        dieselTrain.AddWagon(businessWagon);
        dieselTrain.AddWagon(economyWagon);
        dieselTrain.AddWagon(nightWagon);

        // Přidání nákladního vagónu k parnímu vlaku
        steamTrain.AddWagon(hopperWagon);

        // Zkouška přidání dalšího vagónu k vlakům
        Economy additionalEconomyWagon = new Economy(30);
        Business additionalBusinessWagon = new Business(new Person("Jan", "Novotný"), 20);
        NightWagon additionalNightWagon = new NightWagon(15, 10);
        Hopper additionalHopperWagon = new Hopper(40.0);

        // Připojení dalších vagónů k dieselovému vlaku
        dieselTrain.AddWagon(additionalEconomyWagon);
        dieselTrain.AddWagon(additionalBusinessWagon);
        dieselTrain.AddWagon(additionalNightWagon);

        // Připojení dalšího vagónu k parnímu vlaku
        steamTrain.AddWagon(additionalHopperWagon);

        // Zkouška rezervace sedadla v osobním vagónu
        
        dieselTrain.ReserveChair(1,5);

        // Zkouška opětovné rezervace již rezervovaného sedadla
        //dieselTrain.ReserveChair(1, 5); // Tohle by nemělo fungovat

        // Výpis rezervovaných sedadel
        Console.WriteLine("Rezervovaná sedadla v dieselovém vlaku:");
        dieselTrain.ListReservedChairs();

        Console.WriteLine("Rezervovaná sedadla v parním vlaku:");
        steamTrain.ListReservedChairs();

        // Výpis informací o vlacích
        Console.WriteLine("Informace o dieselovém vlaku:");
        Console.WriteLine(dieselTrain);

        Console.WriteLine("Informace o parním vlaku:");
        Console.WriteLine(steamTrain);
    }
}
    

